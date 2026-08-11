using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static VRCFaceTracking.Core.Params.Expressions.UnifiedExpressions;

namespace PicoFacialDataModule
{
    using static PicoBlendshapes;
    public enum PicoBlendshapes
    {
        EyeLookDownL = 0,
        NoseSneerL = 1,
        EyeLookInL = 2,
        BrowInnerUp = 3,
        BrowDownR = 4,
        MouthClose = 5,
        MouthLowerDownR = 6,
        JawShapeOpen = 7,
        MouthUpperUpR = 8,
        MouthShrugUpper = 9,
        MouthFunnel = 10,
        EyeLookInR = 11,
        EyeLookDownR = 12,
        NoseSneerR = 13,
        MouthRollUpper = 14,
        JawShapeRight = 15,
        BrowDownL = 16,
        MouthShrugLower = 17,
        MouthRollLower = 18,
        MouthSmileL = 19,
        MouthPressL = 20,
        MouthSmileR = 21,
        MouthPressR = 22,
        MouthDimpleR = 23,
        MouthLeft = 24,
        JawShapeForward = 25,
        EyeSquintL = 26,
        MouthFrownL = 27,
        EyeBlinkL = 28,
        CheekSquintL = 29,
        BrowOuterUpL = 30,
        EyeLookUpL = 31,
        JawShapeLeft = 32,
        MouthStretchL = 33,
        MouthPucker = 34,
        EyeLookUpR = 35,
        BrowOuterUpR = 36,
        CheekSquintR = 37,
        EyeBlinkR = 38,
        MouthUpperUpL = 39,
        MouthFrownR = 40,
        EyeSquintR = 41,
        MouthStretchR = 42,
        CheekPuff = 43,
        EyeLookOutL = 44,
        EyeLookOutR = 45,
        EyeWideR = 46,
        EyeWideL = 47,
        MouthRight = 48,
        MouthDimpleL = 49,
        MouthLowerDownL = 50,
        TongueShapeOut = 51
    };

    [InlineArray(72)]
    public struct BlendShapes
    {
        private float _element0;

        public float this[PicoBlendshapes shape]
        {
            get => this[(int)shape];
        }
    }

    [InlineArray(10)]
    public struct Float10
    {
        private float _element0;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PicoFTInfo
    {
        public ulong Timestamp;

        public BlendShapes BlendshapeWeight;
        
        public Float10 VideoInputValid;
        public float LaughingProb;
        public Float10 EmotionProb;
    }

    public class FaceTrackingParser
    {
        public void Parse(byte[] data)
        {
            if (!MemoryMarshal.TryRead<PicoFTInfo>(data, out var picoFTInfo))
                return;

            if (picoFTInfo.VideoInputValid[1] != 1)
                return;

            var blendshapes = picoFTInfo.BlendshapeWeight;
            var face = new UnifiedExpressionsSetter();

            // Taken from ALVR.

            #region Eyebrow Expressions

            face[BrowPinchRight] = blendshapes[BrowDownR];
            face[BrowPinchLeft] = blendshapes[BrowDownL];
            face[BrowLowererRight] = blendshapes[BrowDownR];
            face[BrowLowererLeft] = blendshapes[BrowDownL];
            face[BrowInnerUpRight] = blendshapes[BrowInnerUp];
            face[BrowInnerUpLeft] = blendshapes[BrowInnerUp];
            face[BrowOuterUpRight] = blendshapes[BrowOuterUpR];
            face[BrowOuterUpLeft] = blendshapes[BrowOuterUpL];

            #endregion

            #region Cheek Expressions

            face[CheekSquintRight] = blendshapes[CheekSquintR];
            face[CheekSquintLeft] = blendshapes[CheekSquintL];
            face[CheekPuffRight] = blendshapes[CheekPuff];
            face[CheekPuffLeft] = blendshapes[CheekPuff];

            #endregion

            #region Jaw Exclusive Expressions

            face[JawOpen] = blendshapes[JawShapeOpen];
            face[MouthClosed] = blendshapes[MouthClose];

            face[JawRight] = blendshapes[JawShapeRight];
            face[JawLeft] = blendshapes[JawShapeLeft];
            face[JawForward] = blendshapes[JawShapeForward];

            #endregion

            #region Lip Expressions

            face[LipSuckUpperRight] = blendshapes[MouthRollUpper];
            face[LipSuckUpperLeft] = blendshapes[MouthRollUpper];
            face[LipSuckLowerRight] = blendshapes[MouthRollLower];
            face[LipSuckLowerLeft] = blendshapes[MouthRollLower];

            face[LipFunnelUpperRight] = blendshapes[MouthFunnel];
            face[LipFunnelUpperLeft] = blendshapes[MouthFunnel];
            face[LipFunnelLowerRight] = blendshapes[MouthFunnel];
            face[LipFunnelLowerLeft] = blendshapes[MouthFunnel];

            face[LipPuckerUpperRight] = blendshapes[MouthPucker];
            face[LipPuckerUpperLeft] = blendshapes[MouthPucker];
            face[LipPuckerLowerRight] = blendshapes[MouthPucker];
            face[LipPuckerLowerLeft] = blendshapes[MouthPucker];

            face[MouthUpperUpRight] = blendshapes[MouthUpperUpR];
            face[MouthUpperUpLeft] = blendshapes[MouthUpperUpL];
            face[MouthUpperDeepenRight] = blendshapes[MouthUpperUpR];
            face[MouthUpperDeepenLeft] = blendshapes[MouthUpperUpL];

            face[NoseSneerRight] = blendshapes[NoseSneerR];
            face[NoseSneerLeft] = blendshapes[NoseSneerL];

            face[MouthLowerDownRight] = blendshapes[MouthLowerDownR];
            face[MouthLowerDownLeft] = blendshapes[MouthLowerDownL];

            face[MouthUpperRight] = blendshapes[MouthRight];
            face[MouthUpperLeft] = blendshapes[MouthLeft];
            face[MouthLowerRight] = blendshapes[MouthRight];
            face[MouthLowerLeft] = blendshapes[MouthLeft];

            face[MouthCornerPullRight] = blendshapes[MouthSmileR];
            face[MouthCornerPullLeft] = blendshapes[MouthSmileL];
            face[MouthCornerSlantRight] = blendshapes[MouthSmileR];
            face[MouthCornerSlantLeft] = blendshapes[MouthSmileL];

            face[MouthFrownRight] = blendshapes[MouthFrownR];
            face[MouthFrownLeft] = blendshapes[MouthFrownL];
            face[MouthStretchRight] = blendshapes[MouthStretchR];
            face[MouthStretchLeft] = blendshapes[MouthStretchL];

            face[MouthDimpleRight] = blendshapes[MouthDimpleR];
            face[MouthDimpleLeft] = blendshapes[MouthDimpleL];

            face[MouthRaiserUpper] = blendshapes[MouthShrugUpper];
            face[MouthRaiserLower] = blendshapes[MouthShrugLower];
            face[MouthPressRight] = blendshapes[MouthPressR];
            face[MouthPressLeft] = blendshapes[MouthPressL];

            #endregion

            #region Tongue Expressions

            face[TongueOut] = blendshapes[TongueShapeOut];

            #endregion
        }
    }
}
