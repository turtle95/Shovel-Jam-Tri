Shader "Custom/MobileDefault"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_ColorEmit ("Emit Color", Color) = (1,1,1,1)
		_EmitPower ("Emit Power", Range(0.1, 5)) = 1.0
		_MainTex ("Texture", 2D) = "white" {}
		_Emit ("Emit Key", 2D) = "black" {} 
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			uniform fixed4 _Color;
			uniform fixed4 _ColorEmit;
			uniform float _EmitPower;

			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D _Emit;
			float4 _Emit_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 emit = tex2D(_Emit, i.uv);

				UNITY_APPLY_FOG(i.fogCoord, col);
				return (col * _Color) + (pow(emit.b, _EmitPower) * _ColorEmit);
			}
			ENDCG
		}
	}
}
