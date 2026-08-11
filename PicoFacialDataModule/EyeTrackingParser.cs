using System.Runtime.InteropServices;
using VRCFaceTracking;

namespace PicoFacialDataModule
{
    public class EyeTrackingParser
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PxrEyePoseDataV2
        {
            public uint Timestamp;

            public uint LeftEyePoseStatus;
            public uint RightEyePoseStatus;
            public uint CombinedEyePoseStatus;

            public float LeftEyeGazePointX;
            public float LeftEyeGazePointY;
            public float LeftEyeGazePointZ;

            public float RightEyeGazePointX;
            public float RightEyeGazePointY;
            public float RightEyeGazePointZ;

            public float CombinedEyeGazePointX;
            public float CombinedEyeGazePointY;
            public float CombinedEyeGazePointZ;

            public float LeftEyeGazeVectorX;
            public float LeftEyeGazeVectorY;
            public float LeftEyeGazeVectorZ;

            public float RightEyeGazeVectorX;
            public float RightEyeGazeVectorY;
            public float RightEyeGazeVectorZ;

            public float CombinedEyeGazeVectorX;
            public float CombinedEyeGazeVectorY;
            public float CombinedEyeGazeVectorZ;

            public float LeftEyeOpenness;
            public float RightEyeOpenness;

            public float LeftEyePupilDilation;
            public float RightEyePupilDilation;

            public float LeftEyePositionGuideX;
            public float LeftEyePositionGuideY;
            public float LeftEyePositionGuideZ;

            public float RightEyePositionGuideX;
            public float RightEyePositionGuideY;
            public float RightEyePositionGuideZ;

            public float FoveatedGazeDirectionX;
            public float FoveatedGazeDirectionY;
            public float FoveatedGazeDirectionZ;

            public uint FoveatedGazeTrackingState;
        }

        const float EYE_PUPIL_DILATION_EYE_OPENNESS_THRESHOLD = 0.8f;
        public void Parse(byte[] data)
        {
            if (!MemoryMarshal.TryRead<PxrEyePoseDataV2>(data, out var eyeData))
                return;

            var eye = UnifiedTracking.Data.Eye;

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
        }
    }
}
