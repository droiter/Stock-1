using System;
using Heron.Utility;
using Heron.Data;
using Heron.Strategy;
using Heron.Logs;

namespace Heron.Test
{
    class Futures
    {
        string _code;
        DateTime _start;
        DateTime _end;

        public Futures(string code, DateTime start, DateTime end)
        {
            _code = code;
            _start = start;
            _end = end;
        }

        public void Process()
        {
            var source = new DataService(_code);
            var target = new LogService(_code);
            var method = StrategyFactory.Create("Unilateral", source);

            for (DateTime day = _start; day <= _end; day = day.AddDays(1))
            {
                var data = source.DailyData(day);
                if (data == null)
                    continue;

                foreach (var item in data)
                {
                    var signals = method.Update(item);

                    foreach (var signal in signals)
                        if (signal.actInfo != Actions.Null)
                        {
                            target.Execute(signal);
                            method.Feedback(target.Query());
                        }
                }
            }
        }
    }
}
