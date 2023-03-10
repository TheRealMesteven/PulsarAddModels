Shader "Custom/MoonCustomLighting" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_EmmTex ("Emissive (RGB)", 2D) = "black" {}
		_BumpMap ("Bumpmap", 2D) = "bump" {}
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0
		_RimMultiplier ("RimMultiplier", Range(0, 1)) = 0
		_DirLightMultiplier ("DirLightMultiplier", Range(0, 1)) = 0
		_DirLightVector ("DirLightVector", Vector) = (0,1,0,0)
		_BumpScale ("Scale", Float) = 1
		_Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.5
		_Parallax ("Height Scale", Range(0.005, 0.08)) = 0.02
		_ParallaxMap ("Height Map", 2D) = "black" {}
		_OcclusionStrength ("Strength", Range(0, 1)) = 1
		_OcclusionMap ("Occlusion", 2D) = "white" {}
		_EmissionColor ("Color", Vector) = (0,0,0,1)
		_EmissionMap ("Emission", 2D) = "white" {}
		_DetailMask ("Detail Mask", 2D) = "white" {}
		_DetailAlbedoMap ("Detail Albedo x2", 2D) = "grey" {}
		_DetailNormalMapScale ("Scale", Float) = 1
		_DetailNormalMap ("Normal Map", 2D) = "bump" {}
		[Enum(UV0,0,UV1,1)] _UVSec ("UV Set for secondary textures", Float) = 0
		[HideInInspector] _Mode ("__mode", Float) = 0
		[HideInInspector] _SrcBlend ("__src", Float) = 1
		[HideInInspector] _DstBlend ("__dst", Float) = 0
		[HideInInspector] _ZWrite ("__zw", Float) = 1
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
	//CustomEditor "StandardShaderGUI"
}