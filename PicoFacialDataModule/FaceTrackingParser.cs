using PicoFacialDataModule.Models;
using VRCFaceTracking.Core.Params.Expressions;

namespace PicoFacialDataModule
{
    using static PicoBlendshapes;
    using static UnifiedExpressions;

    public class FaceTrackingParser
    {
        public void Parse(PicoFTInfo picoFTInfo)
        {
            if (picoFTInfo.VideoInputValid[1] != 1)
                return;

            var blendshapes = picoFTInfo.BlendshapeWeight;
            var face = new UnifiedExpressionsSetter();

            // Taken from ALVR.

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
