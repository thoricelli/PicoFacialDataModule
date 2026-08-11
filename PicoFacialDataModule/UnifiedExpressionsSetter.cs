using VRCFaceTracking;
using VRCFaceTracking.Core.Params.Expressions;

namespace PicoFacialDataModule
{
    public class UnifiedExpressionsSetter
    {
        public float this[UnifiedExpressions index]
        {
            set
            {
                UnifiedTracking.Data.Shapes[(int)index].Weight = value;
            }
        }
    }
}
