Shader "Custom/AOEEffectShader_NoZTest" {
	Properties {
		_RegularColor ("Main Color", Vector) = (1,1,1,0.5)
		_HighlightColor ("Highlight Color", Vector) = (1,1,1,0.5)
		_HighlightThresholdMax ("Highlight Threshold Max", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "VertexLit"
}