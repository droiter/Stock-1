using System;
using System.Drawing;
using Heron.Utility;

namespace Heron.Demo
{
    class Painter
    {
        const decimal Bar = 0.2m;
        const int Width = 960;
        const int HeightP = 480;
        const int HeightV = 240;
        const int GroupInterval = 30;
        const int SignalSize = 9;

        Bitmap _image;
        Graphics _graphics;

        decimal _basePrice;
        int _baseVol;

        int _groupId;
        int _volSum;
        int _lastVol;

        public Painter()
        {
            _image = new Bitmap(Width, HeightP + HeightV);
            _graphics = Graphics.FromImage(_image);
        }

        float pos(TimeSpan period)
        {
            float xsize = Width / 14400f;

            if (period < TimeSpan.FromHours(12))
            {
                return (int)(period - TimeSpan.FromHours(9.5)).TotalSeconds * xsize;
            }
            else
            {
                return (int)(period - TimeSpan.FromHours(13)).TotalSeconds * xsize + Width / 2;
            }
        }

        void drawPrice(decimal price, DateTime time)
        {
            decimal unit = HeightP / (_basePrice * Bar);

            float x = pos(time.TimeOfDay);
            float y = HeightP / 2f - (float)((price - _basePrice) * unit);

            _graphics.DrawLine(Pens.Black, x, y - 1, x, y + 1);
        }

        void drawVolume(int vol, DateTime time)
        {
            float xsize = Width / 14400f;
            float ysize = HeightV / (_baseVol / 14400f * GroupInterval) / 10;

            int s = (int)time.TimeOfDay.TotalSeconds / GroupInterval;

            if (_lastVol == 0)
            {
                _groupId = s;
                _volSum = 0;
                _lastVol = vol;
                return;
            }

            if (s > _groupId)
            {
                _graphics.DrawRectangle(Pens.Black,
                    pos(TimeSpan.FromSeconds(_groupId * GroupInterval)),
                    HeightP + HeightV - _volSum * ysize,
                    GroupInterval * xsize,
                    _volSum * ysize);

                _groupId = s;
                _volSum = vol - _lastVol;
            }
            else
            {
                _volSum += vol - _lastVol;
            }

            _lastVol = vol;
        }

        public void Init(decimal price, int volume)
        {
            _graphics.Clear(Color.White);

            _graphics.DrawLine(Pens.Blue, 0, HeightP, Width, HeightP);
            _graphics.DrawLine(Pens.Yellow, 0, HeightP / 2, Width, HeightP / 2);
            _graphics.DrawLine(Pens.Blue, Width / 2, 0, Width / 2, HeightP + HeightV);
            _graphics.DrawLine(Pens.Yellow, 0, HeightP + HeightV * 9 / 10, Width, HeightP + HeightV * 9 / 10);

            _basePrice = price;
            _baseVol = volume;

            _lastVol = 0;
        }

        public void DrawItem(DataNode item)
        {
            drawPrice(item.price, item.time);
            drawVolume(item.volume, item.time);
        }

        public void DrawSignal(Signal s)
        {
            decimal unit = HeightP / (_basePrice * Bar);

            float x = pos(s.time.TimeOfDay);
            float y = HeightP / 2f - (float)((s.price - _basePrice) * unit);

            switch (s.actInfo)
            {
                case Actions.OpenLong:
                    _graphics.DrawLine(Pens.Red,
                        x, y - SignalSize, x, y + SignalSize);
                    _graphics.DrawLine(Pens.Red,
                        x - SignalSize, y, x + SignalSize, y);
                    break;
                case Actions.CloseLong:
                    _graphics.DrawLine(Pens.Red,
                        x - SignalSize, y - SignalSize, x + SignalSize, y + SignalSize);
                    _graphics.DrawLine(Pens.Red,
                        x - SignalSize, y + SignalSize, x + SignalSize, y - SignalSize);
                    break;
                case Actions.OpenShort:
                    _graphics.DrawLine(Pens.Green,
                        x, y - SignalSize, x, y + SignalSize);
                    _graphics.DrawLine(Pens.Green,
                        x - SignalSize, y, x + SignalSize, y);
                    break;
                case Actions.CloseShort:
                    _graphics.DrawLine(Pens.Green,
                        x - SignalSize, y - SignalSize, x + SignalSize, y + SignalSize);
                    _graphics.DrawLine(Pens.Green,
                        x - SignalSize, y + SignalSize, x + SignalSize, y - SignalSize);
                    break;
            }
        }

        public Bitmap GetImage()
        {
            return _image;
        }
    }
}
