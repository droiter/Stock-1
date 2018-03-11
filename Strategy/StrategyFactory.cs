using Heron.Data;

namespace Heron.Strategy
{
    public static class StrategyFactory
    {
        public static StrategyModel Create(string strategy, DataService service)
        {
            StrategyModel model;

            switch (strategy)
            {
                case "Unilateral": model = new Unilateral(service); break;
                case "Inertance": model = new Inertance(service); break;
                default: model = new Idle(); break;
            }

            return model;
        }
    }
}
