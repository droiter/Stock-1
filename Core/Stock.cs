using System;
using System.Threading;
using Heron.Utility;
using Heron.Data;
using Heron.Strategy;
using Heron.Logs;
using Heron.DBAccess;

namespace Heron.Core
{
    class WorkItem
    {
        string _code;
        DataService _source;
        LogService _target;
        StrategyModel _method;

        void handler(DataNode item)
        {
            var signals = _method.Update(item);

            foreach (var signal in signals)
                if (signal.actInfo != Actions.Null)
                {
                    _target.Execute(signal);
                    _method.Feedback(_target.Query());

                    Console.WriteLine(
                        signal.code
                        + "    " + _source.Name
                        + "    " + Common.Translate(signal.actInfo)
                        + "    " + signal.time
                        + "    " + signal.price);

                    Console.Beep(800, 1000);

                    DBService service = new DBService();
                    service.Add(signal);
                }
        }

        public WorkItem(string code)
        {
            _code = code;
            _source = new DataService(code);
            _target = new LogService(code);
            _method = StrategyFactory.Create("Inertance", _source);

            _source.Start(this.handler);
        }
    }

    class Stock
    {
        string[] _codes;

        public Stock(string[] codes)
        {
            _codes = codes;
        }

        public void Process()
        {
            foreach (var code in _codes)
            {
                if (code != null && code.Trim() != "")
                    new WorkItem(code.Trim());
            }

            while (true)
                Thread.Sleep(5000);
        }
    }
}
