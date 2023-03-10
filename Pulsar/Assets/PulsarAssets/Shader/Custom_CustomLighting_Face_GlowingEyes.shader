Shader "Custom/CustomLighting_Face_GlowingEyes" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_EyeColor ("EyeColor", Vector) = (1,1,1,1)
		_LipColor ("LipColor", Vector) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_SkinID ("SkinID", Float) = 0
		_AmbientBoost ("AmbientBoost", Float) = 0.5
		_GlowAmt ("GlowAmt", Float) = 60
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