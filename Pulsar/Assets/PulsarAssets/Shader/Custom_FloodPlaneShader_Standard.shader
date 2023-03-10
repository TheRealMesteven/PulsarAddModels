Shader "Custom/FloodPlaneShader_Standard" {
	Properties {
		_MainTex ("Main Texture", 2D) = "white" {}
		_BumpMap ("Main Normals", 2D) = "white" {}
		_BumpMap2 ("Main Normals 2", 2D) = "white" {}
		_Smoothness ("Smoothness", Range(0, 1)) = 0
		_Metallic ("Metallic", Range(0, 1)) = 0
		_RegularColor ("Main Color", Vector) = (1,1,1,0.5)
		_HighlightColor ("Highlight Color", Vector) = (1,1,1,0.5)
		_HighlightThresholdMax ("Highlight Threshold Max", Float) = 1
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
}