Shader "Unlit/SkyboxFade"
{
	Properties
	{
		_Rotation("Rotation", Range(0.000000,360.000000)) = 0.000000
		_Blend ("Blend", Range(0.0,1.0)) = 0.5
		_Skybox1 ("Skybox one", Cube) = ""
		_Skybox2 ("Skybox two", Cube) = ""
	}
	SubShader
	{
		Tags { "Queue"="Background" }
		Cull Off
		Fog{ Mode Off}
		Lighting Off


		Pass
		{
			SetTexture [_Skybox1] {combine texture}
			SetTexture [_Skybox2] {constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous}
			//SetTexture [_Skybox2] {combine previous +- primary, previous * primary}
		}
	}
	Fallback "Skybox/6 Sided", 1
}
