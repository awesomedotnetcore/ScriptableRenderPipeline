using System;
using UnityEngine;

namespace UnityEngine.Experimental.Rendering.HDPipeline
{
    // The settings here are per reflection settings.
    // Each probe must have its own per reflection settings
    [Serializable]
    public class ReflectionSettings : FrameSettings
    {
        [Flags]
        public enum ReflectionSettingsOverrides : long
        {
            //lighting settings
            EnableShadow = 1 << 1,
            EnableContactShadow = 1 << 2,
            EnableShadowMask = 1 << 3,
            EnableSSR = 1 << 4,
            EnableSSAO = 1 << 5,
            EnableSubsurfaceScattering = 1 << 6,
            EnableTransmission = 1 << 7,
            EnableAtmosphericScaterring = 1 << 8,
            EnableVolumetrics = 1 << 9,
            EnableLightLayers = 1 << 10,

            //rendering pass
            EnableTransparentPrepass = 1 << 11,
            EnableTransparentPostpass = 1 << 12,
            EnableMotionVectors = 1 << 13,
            EnableObjectMotionVectors = 1 << 14,
            EnableDecals = 1 << 15,
            EnableRoughRefraction = 1 << 16,
            EnableDistortion = 1 << 17,
            EnablePostprocess = 1 << 18,

            //rendering settings
            EnableForwardRenderingOnly = 1 << 19,
            EnableDepthPrepassWithDeferredRendering = 1 << 20,
            EnableAsyncCompute = 1 << 21,
            EnableOpaqueObjects = 1 << 22,
            EnableTransparentObjects = 1 << 23,

            //other FrameSettings
            EnableStereo = 1 << 24,
            EnableMSAA = 1 << 25,
            MSAASampleCount = 1 << 26,

            //light loop settings
            EnableFptlForForwardOpaque = 1 << 27,
            EnableBigTilePrepass = 1 << 28,
            EnableComputeLightEvaluation = 1 << 29,
            EnableComputeLightVariants = 1 << 30,
            EnableComputeMaterialVariants = 1 << 31,

            //other light loop settings
            enableTileAndCluster = 1 << 32,
            isFptlEnabled = 1 << 33,

            //capture settings
            CubeResolution = 1 << 34,
            PlanarResolution = 1 << 35,
            ClearColorMode = 1 << 36,
            BackgroundColorHDR = 1 << 37,
            ShadowDistance = 1 << 38,
            CullingMask = 1 << 39,
            UseOcclusionCulling = 1 << 40,
            NearClip = 1 << 41,
            FarClip = 1 << 42,
        }

        public ReflectionSettingsOverrides overrides;

        //additional acquisition camera settings (capture)
        public CubeReflectionResolution cubeResolution = CubeReflectionResolution.CubeReflectionResolution128;
        public PlanarReflectionResolution planarResolution = PlanarReflectionResolution.PlanarReflectionResolution64;

        public HDAdditionalCameraData.ClearColorMode clearColorMode = HDAdditionalCameraData.ClearColorMode.Sky;
        public Color backgroundColorHDR = new Color(0.025f, 0.07f, 0.19f, 0.0f);

        public float shadowDistance;

        public int cullingMask = -1; //= 0xFFFFFFFF which is c++ default
        public bool useOcclusionCulling = true;

        public float nearClip = 0.3f;
        public float farClip = 1000f;
    }
}
