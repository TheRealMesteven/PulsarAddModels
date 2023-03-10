Shader "Custom/WarpFieldBattleShader" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_RimColor ("Rim Color", Vector) = (1,1,1,1)
		_RimPower ("Rim Power", Range(0, 100)) = 1
		_HighlightThresholdMax ("Highlight Threshold", Range(0, 100)) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}