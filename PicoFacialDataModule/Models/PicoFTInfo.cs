using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PicoFacialDataModule.Models
{
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
}
