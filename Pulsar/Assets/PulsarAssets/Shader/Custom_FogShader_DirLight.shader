Shader "Custom/FogShader_DirLight" {
	Properties {
		_MainTex ("", 2D) = "white" {}
		_FogColor ("_FogColor", Vector) = (0,0,0,0)
		_FogColor2 ("_FogColor2", Vector) = (0,0,0,0)
		_FogMaxAmt ("_FogMaxAmt", Float) = 0
		_FogAmt ("_FogAmt", Float) = 0
		_MinDepthValue ("_MinDepthValue", Float) = 0
		_MaxDepthValue ("_MaxDepthValue", Float) = 0
		_FogColor1Scale ("_DirFogColor1Scale", Float) = 1
		_FogColor1Pow ("_DirFogColor1Pow", Float) = 1
		_FogColor2Scale ("_DirFogColor2Scale", Float) = 1
		_FogColor2DepthMin ("_FogColor2DepthMin", Float) = 1
		_FogColorAmbientBrightness ("_FogColorAmbientBrightness", Float) = 1
		_LightDir ("_LightDir", Vector) = (0,0,0,0)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}