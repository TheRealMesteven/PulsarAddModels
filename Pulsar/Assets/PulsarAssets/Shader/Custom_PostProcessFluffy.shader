Shader "Custom/PostProcessFluffy" {
	Properties {
		_MainTex ("", 2D) = "white" {}
		_FogColorTexture ("_FogColorTexture", 2D) = "white" {}
		_FogMaxAmt ("_FogMaxAmt", Float) = 0
		_FogAmt ("_FogAmt", Float) = 0
		_FogMultiplier ("_FogMultiplier", Float) = 0
		_MinDepthValue ("_MinDepthValue", Float) = 0
		_MaxDepthValue ("_MaxDepthValue", Float) = 0
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