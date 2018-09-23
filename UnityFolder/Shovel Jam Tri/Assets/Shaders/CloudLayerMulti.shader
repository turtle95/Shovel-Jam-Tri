Shader "Custom/CloudLayerMulti"
{
	Properties
	{
		_TintColor ("Tint", Color) = (1, 1, 1, 1)
		_StarColor ("Star Color", Color) = (1, 1, 1, 1)
		_MainTex ("Main Texture", 2D) = "white" {}
		_MaskTex1 ("Mask Texture 1", 2D) = "white" {}
		_MaskTex2 ("Mask Texture 2", 2D) = "white" {}
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
			// make fog work
			#pragma multi_compile_fog
			
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
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			half4 _TintColor;
			half4 _StarColor;

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
				UNITY_TRANSFER_FOG(o,o.vertex);
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
				// sample the masks
				fixed4 mask1 = tex2D(_MaskTex1, i.uv_1);
				fixed4 mask2 = tex2D(_MaskTex2, i.uv_2);
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv) * _TintColor;

				half starIntensity = saturate(
						mask2.g * pow((1.0 - mask1.b) * (1.0 - col.g), 5.0) * 15.0
					);

				col = lerp(col, _StarColor, starIntensity);
				
				col.a = saturate(mask1.b * mask2.b +
						starIntensity
					);
				
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
