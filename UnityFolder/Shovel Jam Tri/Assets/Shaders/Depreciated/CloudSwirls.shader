Shader "Custom/CloudSwirls"
{
	Properties
	{
		_SwirlIntensity ("Swirl Intensity", Range(0, 5)) = 5.0
		_TintColor ("Tint", Color) = (1, 1, 1, 1)
		_MainTex ("Main Texture", 2D) = "white" {}
		_MaskTex ("Mask Texture ", 2D) = "white" {}
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
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 uv_1 : TEXCOORD1;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			half _SwirlIntensity;
			half4 _TintColor;

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _MaskTex;
			float4 _MaskTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv_1 = TRANSFORM_TEX(v.uv_1, _MaskTex);
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
				// sample the mask
				fixed4 mask = tex2D(_MaskTex, i.uv_1);
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv + half2(mask.r, mask.b) * _SwirlIntensity);
				
				col.a = saturate(mask.b * col.r);

				// Apply tint
				col *= _TintColor;
				
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
