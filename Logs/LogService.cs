using Heron.Utility;

namespace Heron.Logs
{
    public class LogService
    {
        Monitor _monitor;

        public LogService(string code)
        {
            _monitor = new Monitor(code);
        }

        public bool Execute(Signal signal)
        {
            if ((signal.actInfo == Actions.OpenLong) || (signal.actInfo == Actions.OpenShort))
            {
                _monitor.Process(signal);
                return true;
            }

            var status = _monitor.Retrieve();
            var profit = _monitor.Process(signal);

            if (signal.actInfo == Actions.CloseLong)
                Trace.Record(
                    status.code,
                    signal.name,
                    Common.Translate(Direction.Long),
                    status.longTime,
                    status.longCost,
                    signal.time,
                    signal.price,
                    signal.lots,
                    profit);

            if (signal.actInfo == Actions.CloseShort)
                Trace.Record(
                    status.code,
                    signal.name,
                    Common.Translate(Direction.Short),
                    status.shortTime,
                    status.shortCost,
                    signal.time,
                    signal.price,
                    signal.lots,
                    profit);

            return true;
        }

        public PosStatus Query()
        {
            return _monitor.Retrieve();
        }
    }
}
