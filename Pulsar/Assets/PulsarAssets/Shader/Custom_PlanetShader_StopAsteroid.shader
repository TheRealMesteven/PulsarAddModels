Shader "Custom/PlanetShader_StopAsteroid" {
	Properties {
		_MainColor ("Main Color", Vector) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_CloudTex ("Base (RGB)", 2D) = "white" {}
		_DiffCloudTex ("Base (RGB)", 2D) = "white" {}
		_RimColor ("Rim Color", Vector) = (1,1,1,1)
		_RimPower ("Rim Power", Float) = 0
		_AmbientColor ("AmbientColor", Vector) = (0,0,0,0)
		_LavaTex ("Base (RGB)", 2D) = "white" {}
		_LavaTexFill ("Base (RGB)", 2D) = "white" {}
		_BurnedAmount ("Burned Amount", Float) = 0
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