Shader "Hidden/VLB_BuiltIn_SinglePass" {
	Properties {
		_ConeSlopeCosSin ("Cone Slope Cos Sin", Vector) = (0,0,0,0)
		_ConeRadius ("Cone Radius", Vector) = (0,0,0,0)
		_ConeApexOffsetZ ("Cone Apex Offset Z", Float) = 0
		_ColorFlat ("Color", Vector) = (1,1,1,1)
		_AlphaInside ("Alpha Inside", Range(0, 1)) = 1
		_AlphaOutside ("Alpha Outside", Range(0, 1)) = 1
		_DistanceFallOff ("Distance Fall Off", Vector) = (0,1,1,0)
		_DistanceCamClipping ("Camera Clipping Distance", Float) = 0.5
		_FadeOutFactor ("FadeOutFactor", Float) = 1
		_AttenuationLerpLinearQuad ("Lerp between attenuation linear and quad", Float) = 0.5
		_DepthBlendDistance ("Depth Blend Distance", Float) = 2
		_FresnelPow ("Fresnel Pow", Range(0, 15)) = 1
		_GlareFrontal ("Glare Frontal", Range(0, 1)) = 0.5
		_GlareBehind ("Glare from Behind", Range(0, 1)) = 0.5
		_DrawCap ("Draw Cap", Float) = 1
		_NoiseVelocityAndScale ("Noise Velocity And Scale", Vector) = (0,0,0,0)
		_NoiseParam ("Noise Param", Vector) = (0,0,0,0)
		_CameraParams ("Camera Params", Vector) = (0,0,0,0)
		_BlendSrcFactor ("BlendSrcFactor", Float) = 1
		_BlendDstFactor ("BlendDstFactor", Float) = 1
		_DynamicOcclusionClippingPlaneWS ("Dynamic Occlusion Clipping Plane WS", Vector) = (0,0,0,0)
		_DynamicOcclusionClippingPlaneProps ("Dynamic Occlusion Clipping Plane Props", Float) = 0.25
		_DynamicOcclusionDepthTexture ("DynamicOcclusionDepthTexture", 2D) = "white" {}
		_DynamicOcclusionDepthProps ("DynamicOcclusionDepthProps", Float) = 0.25
		_LocalForwardDirection ("LocalForwardDirection", Vector) = (0,0,1,1)
		_TiltVector ("TiltVector", Vector) = (0,0,0,0)
		_AdditionalClippingPlaneWS ("AdditionalClippingPlaneWS", Vector) = (0,0,0,0)
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