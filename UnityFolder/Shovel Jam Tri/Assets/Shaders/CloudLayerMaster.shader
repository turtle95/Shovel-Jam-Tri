/*
	This combines the cloud and swirl shaders into one
	so that it runs faster on mobile
*/
Shader "Custom/CloudLayerMaster"
{
	Properties
	{
		_TintColor ("Cloud Tint", Color) = (1, 1, 1, 1)
		_SwirlTintColor ("Swirl Tint", Color) = (1, 1, 1, 1)
		_StarColor ("Star Color", Color) = (1, 1, 1, 1)
		_SwirlIntensity ("Swirl Intensity", Range(0, 5)) = 5.0
		_CloudReact ("Clouds Move With Camera", Range(0, 3)) = 0.2
		_SwirlReact ("Swirls Move With Camera", Range(0, 3)) = 0.4
		_MainTex ("Main Texture", 2D) = "white" {}
		_MaskTex1 ("Mask Texture 1", 2D) = "white" {}
		_MaskTex2 ("Mask Texture 2", 2D) = "white" {}
		[HideInInspector] _CameraOffset ("Camera Offset", Vector) = (0, 0, 0, 0)
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float2 uv_1 : TEXCOORD1;
				float2 uv_2 : TEXCOORD2;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 uv_1 : TEXCOORD1;
				float2 uv_2 : TEXCOORD2;
				float4 vertex : SV_POSITION;
			};

			half _SwirlIntensity;
			half _SwirlReact;
			half _CloudReact;
			half4 _TintColor;
			half4 _SwirlTintColor;
			half4 _StarColor;
			float4 _CameraOffset;

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _MaskTex1;
			float4 _MaskTex1_ST;
			sampler2D _MaskTex2;
			float4 _MaskTex2_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv_1 = TRANSFORM_TEX(v.uv_1, _MaskTex1);
				o.uv_2 = TRANSFORM_TEX(v.uv_2, _MaskTex2);
				return o;
			}
			
			/*
				MAIN:
					RGB = albedo
				MASK 1:
					Value = regular noise
				MASK 2:
					R = clumpy noise
					G = stars
					B = regular noise
			*/
			fixed4 frag (v2f i) : SV_Target
			{
				// Convert to 2D vector
				half2 cloudOffset = half2(_CameraOffset.x, _CameraOffset.y) * _CloudReact;
				half2 swirlOffset = half2(_CameraOffset.x, _CameraOffset.y) * _SwirlReact;

				// Sample the masks
				fixed3 mask1 = tex2D(_MaskTex1, i.uv_1 + cloudOffset * 0.5).rgb;
				fixed3 mask2 = tex2D(_MaskTex2, i.uv_2 + cloudOffset * 0.5).rgb;
				// Sample the texture
				fixed3 albedo = tex2D(_MainTex, i.uv + cloudOffset).rgb * _TintColor.rgb;

				half starIntensity = saturate(
						mask2.g * pow((1.0 - mask1.b) * (1.0 - albedo.g), 5.0) * 15.0
					);

				// Apply tint
				albedo *= _TintColor.rgb;

				// Let stars shine through sometimes
				albedo = lerp(albedo, _StarColor, starIntensity);
				
				// Set alpha based on masks
				half alpha = saturate(mask1.b * mask2.b + starIntensity) * _TintColor.a;

				// Re-sample for swirl layer
				fixed3 swirlCol = tex2D(_MainTex, i.uv + swirlOffset + half2(mask1.r, mask2.b) * _SwirlIntensity).rgb;

				// Calculate alpha for swirls
				half swirlAlpha = saturate(mask1.b * swirlCol.r) * _SwirlTintColor.a;
				
				// Lerp between albedoors
				albedo = lerp(albedo, swirlCol * _SwirlTintColor.rgb, swirlAlpha);

				// Combine alpha channels
				alpha = 1.0 - (1.0 - alpha) * (1.0 - swirlAlpha);
				
				return fixed4(albedo, alpha);
			}
			ENDCG
		}
	}
}
