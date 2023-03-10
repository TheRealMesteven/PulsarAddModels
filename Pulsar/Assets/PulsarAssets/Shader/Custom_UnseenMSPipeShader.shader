Shader "Custom/UnseenMSPipeShader" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap ("Normal Map", 2D) = "bump" {}
		_EmissiveColor ("Emissive Color", Vector) = (1,1,1,1)
		_EmissivePower ("Emissive Power", Range(0, 100)) = 1
		_EmissiveTex ("Emissive (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0
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