Shader "Custom/CrystalShader_FadeViaFragments" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalMap ("_NormalMap", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0
		_RimColor ("Rim Color", Vector) = (0.26,0.19,0.16,0)
		_RimPower ("Rim Power", Range(0.5, 8)) = 3
		_Cube ("Cubemap", Cube) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}