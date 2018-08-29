using System;
using UnityEngine;

namespace UnityEngine.Experimental.Rendering.HDPipeline
{
    // The settings here are per reflection settings.
    // Each probe must have its own per reflection settings
    [Serializable]
    public class ReflectionSettings : FrameSettings
    {
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
