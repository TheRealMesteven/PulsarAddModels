Shader "HardSurface/Hardsurface Free/Opaque Specular" {
	Properties {
		_Color ("Main Color", Vector) = (1,1,1,1)
		_SpecColor ("Specular Color", Vector) = (1,1,1,1)
		_Shininess ("Shininess", Range(0.01, 3)) = 1.5
		_Gloss ("Gloss", Range(0, 1)) = 0.5
		_Reflection ("Reflection", Range(0, 1)) = 0.5
		_Cube ("Reflection Cubemap", Cube) = "Black" {}
		_FrezPow ("Fresnel Reflection", Range(0, 2)) = 0.25
		_FrezFalloff ("Fresnal/EdgeAlpha Falloff", Range(0, 10)) = 4
		_EdgeAlpha ("Edge Alpha", Range(0, 1)) = 0
		_Metalics ("Metalics", Range(0, 1)) = 0.5
		_MainTex ("Diffuse(RGB) Alpha(A)", 2D) = "White" {}
		_BumpMap ("Normalmap", 2D) = "Bump" {}
		_Spec_Gloss_Reflec_Masks ("Spec(R) Gloss(G) Reflec(B)", 2D) = "White" {}
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