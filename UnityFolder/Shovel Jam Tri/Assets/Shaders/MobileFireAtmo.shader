Shader "Custom/MobileFireAtmo"
{
	Properties
	{
		_Color1 ("Color1", Color) = (1,1,1,1)
		_Color2 ("Color2", Color) = (1,1,1,1)
		_Distortion ("Distortion", Range(0, 2)) = 0.5
		_MainTex ("Texture", 2D) = "white" {}
		_Tex2 ("Texture 2", 2D) = "white" {}
		_DistortionTex ("Distortion Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "IgnoreProjector"="True" "Queue"="Transparent" }
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma alpha
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			uniform fixed4 _Color1;
			uniform fixed4 _Color2;
			uniform half _Distortion;

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _Tex2;
			sampler2D _DistortionTex;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// Distortion map
				half4 distort = tex2D(_DistortionTex, i.uv + _Time.x * half2(2.84, -1.19));

				// Color and alpha textures
				half4 col1 = tex2D(_MainTex, i.uv + _Time.x * half2(4.5, 3.42) + _Distortion * half2(distort.r - 0.5, distort.b - 0.5));
				half4 col2 = tex2D(_Tex2, i.uv + _Time.x * half2(-3.84, -2.0) + _Distortion * half2(distort.g - 0.5, distort.r - 0.5));

				// Lerp between 2 colors
				fixed4 col = lerp(_Color1, _Color2, col2.b * col2.b);

				// Set alpha
				col.a = saturate(pow(col1.b * col2.b * (0.5 + 0.5 * distort.b), 1.25) * 5.0) * 2.0;
				
				// Return wonderful color
				return col;

			}
			ENDCG
		}
	}
}
