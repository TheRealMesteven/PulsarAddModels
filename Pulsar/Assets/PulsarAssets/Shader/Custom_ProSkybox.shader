Shader "Custom/ProSkybox" {
	Properties {
		_SkyTint ("Sky Tint", Vector) = (0.5,0.5,0.5,1)
		_SunTint ("Sun Tint", Vector) = (0.5,0.5,0.5,1)
		_GalaxyCoreWorldPos ("Core Position", Vector) = (0.5,0.5,0.5,1)
		_Exposure ("Exposure", Range(0, 8)) = 1.3
		_BGExposure ("BGExposure", Range(0, 8)) = 1.3
		_Tint ("Tint Color", Vector) = (0.5,0.5,0.5,0.5)
		[NoScaleOffset] _Tex ("Cubemap   (HDR)", Cube) = "grey" {}
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
}