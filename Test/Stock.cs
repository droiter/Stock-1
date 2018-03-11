using System;
using Heron.Utility;
using Heron.Data;
using Heron.Strategy;
using Heron.Logs;

namespace Heron.Test
{
    class Stock
    {
        string[] _codes;
        DateTime _start;
        DateTime _end;

        public Stock(string[] codes, DateTime start, DateTime end)
        {
            _codes = codes;
            _start = start;
            _end = end;
        }

        public void Process()
        {
            foreach (var code in _codes)
            {
                if (code == null || code.Trim() == "")
                    continue;

                var source = new DataService(code);
                var target = new LogService(code);
                var method = StrategyFactory.Create("Inertance", source);

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
}
