using System.Runtime.InteropServices;

namespace PicoFacialDataModule.Models
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
}
