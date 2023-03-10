Shader "Custom/StopAsteroidCustomShader" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_EmmColor ("EmmColor", Vector) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex2 ("Albedo 2 (RGB)", 2D) = "white" {}
		_NormalMap ("NormalMap", 2D) = "bump" {}
		_NormalMapAmt ("_NormalMapAmt", Range(0, 100)) = 0
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0
		_DistanceOffset ("Distance Offset", Float) = 0
		_DistanceAlphaMultiplier ("Distance Alpha Multiplier", Float) = 0.01
		_SequenceAlpha ("Sequence Alpha", Float) = 0
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