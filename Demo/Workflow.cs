using System;
using System.Drawing;
using Heron.Utility;
using Heron.Data;
using Heron.Logs;
using Heron.Strategy;


namespace Heron.Demo
{
    class Workflow
    {
        Action<Image, string> _callback;
        DateTime _date;

        string _code;
        Painter _painter;

        DataService _source;
        LogService _target;
        StrategyModel _method;

        public Workflow(DateTime date, string code, Painter painter)
        {
            _date = date;
            init(code, painter, _date);
        }

        public Workflow(Action<Image, string> callback, string code, Painter painter)
        {
            _callback = callback;
            init(code, painter, DateTime.Now);
        }

        void init(string code, Painter painter, DateTime day)
        {
            _code = code;
            _painter = painter;

            _source = new DataService(_code);
            _target = new LogService(_code);
            _method = StrategyFactory.Create("Inertance", _source);

            _painter.Init(_source.GetPreClose(day), _source.GetRecentAvgVol(day));
        }

        void handler(DataNode item)
        {
            string result = "";
            result = item.time.ToString() + "    "
                + item.price.ToString() + "    "
                + item.volume.ToString() + "\r\n";

            var signals = _method.Update(item);
            _painter.DrawItem(item);

            foreach (var signal in signals)
                if (signal.actInfo != Actions.Null)
                {
                    result += Common.Translate(signal.actInfo)
                        + "    " + signal.time
                        + "    " + signal.price + "\r\n";

                    _painter.DrawSignal(signal);
                    _target.Execute(signal);
                    _method.Feedback(_target.Query());
                }

            _callback(_painter.GetImage(), result);
        }

        public void Start()
        {
            _source.Start(this.handler);
        }

        public void Stop()
        {
            _source.Stop();
        }

        public string Process()
        {
            var data = _source.DailyData(_date);

            if (data == null)
            {
                data = _source.DailyDataK(_date);

                if (data == null)
                    return "No Content";
            }

            string result = "";
            foreach (var item in data)
            {
                var signals = _method.Update(item);
                _painter.DrawItem(item);

                foreach (var signal in signals)
                    if (signal.actInfo != Actions.Null)
                    {
                        result += Common.Translate(signal.actInfo)
                            + "    " + signal.time
                            + "    " + signal.price + "\r\n";

                        _painter.DrawSignal(signal);
                        _target.Execute(signal);
                        _method.Feedback(_target.Query());
                    }
            }

            return result;
        }
    }
}
