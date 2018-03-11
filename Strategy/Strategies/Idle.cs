using System;
using Heron.Utility;

namespace Heron.Strategy
{
    class Idle : StrategyModel
    {
        public override void Feedback(PosStatus status)
        {
        }

        public override Signal[] Update(DataNode item)
        {
            return null;
        }
    }
}
