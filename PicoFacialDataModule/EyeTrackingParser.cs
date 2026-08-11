using PicoFacialDataModule.Models;
using VRCFaceTracking;
using VRCFaceTracking.Core.Params.Expressions;

namespace PicoFacialDataModule
{
    using static PicoBlendshapes;
    using static UnifiedExpressions;

    public class EyeTrackingParser
    {
        const float EYE_PUPIL_DILATION_EYE_OPENNESS_THRESHOLD = 0.8f;
        public void Parse(PxrEyePoseDataV2 eyeData, PicoFTInfo picoFTInfo)
        {
            var eye = UnifiedTracking.Data.Eye;
            var blendshapes = picoFTInfo.BlendshapeWeight;
            var face = new UnifiedExpressionsSetter();

            if (picoFTInfo.VideoInputValid[0] != 1)
                return;

            eye.Left.Gaze.x = eyeData.LeftEyeGazeVectorX;
            eye.Left.Gaze.y = eyeData.LeftEyeGazeVectorY;

            eye.Left.Openness = eyeData.LeftEyeOpenness;

            if (eyeData.LeftEyePupilDilation == 0)
                eyeData.LeftEyePupilDilation = 50f;

            if (eyeData.LeftEyeOpenness > EYE_PUPIL_DILATION_EYE_OPENNESS_THRESHOLD)
                eye.Left.PupilDiameter_MM = eyeData.LeftEyePupilDilation / 10;

            eye.Right.Gaze.x = eyeData.RightEyeGazeVectorX;
            eye.Right.Gaze.y = eyeData.RightEyeGazeVectorY;

            eye.Right.Openness = eyeData.RightEyeOpenness;

            if (eyeData.RightEyePupilDilation == 0)
                eyeData.RightEyePupilDilation = 50f;

            if (eyeData.RightEyeOpenness > EYE_PUPIL_DILATION_EYE_OPENNESS_THRESHOLD)
                eye.Right.PupilDiameter_MM = eyeData.RightEyePupilDilation / 10;


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
        }
    }
}
