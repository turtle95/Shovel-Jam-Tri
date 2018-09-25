Shader "Custom/AlbertoWhale" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_SpecTint ("Spec Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SpecPow ("Spec Power", Range(0.05,1.0)) = 0.3
		_RimColor ("Rim Color", Color) = (1,1,1,1)
		_RimPow ("Rim Power", Range(0.05,10.0)) = 0.3
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf BlinnPhong

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		
		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
		};

		uniform float4 _Color;
		uniform float4 _RimColor;
		uniform float _RimPow;
		uniform float _SpecPow;
		uniform float4 _SpecTint;

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * _Color.rgb;

			/* RIM */
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			o.Emission = _RimColor.rgb * pow(rim, _RimPow) * _RimColor.a;
			
			/* SPECULAR */
			o.Specular = _SpecPow;
			_SpecColor.rgb = c.rgb * _SpecTint.rgb * _SpecTint.a;
			o.Gloss = _SpecTint.a;
			o.Alpha = _SpecTint.a;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
