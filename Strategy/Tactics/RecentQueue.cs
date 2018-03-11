using System;
using System.Collections.Generic;
using Heron.Utility;

namespace Heron.Strategy
{
    class RecentQueue
    {
        Queue<DataNode> _queue;
        int _period;

        public RecentQueue(int seconds)
        {
            _queue = new Queue<DataNode>();
            _period = seconds;
        }

        public void Update(DataNode item)
        {
            _queue.Enqueue(item);
            while (item.time - _queue.Peek().time > TimeSpan.FromSeconds(_period))
                _queue.Dequeue();
        }

        public int GetTotalVol(DateTime start, DateTime end)
        {
            int p = _queue.Peek().volume;
            int q = 0;

            foreach (var i in _queue)
            {
                if (i.time <= start)
                    p = i.volume;
                if ((i.time > start) && (i.time <= end))
                    q = i.volume;
                if (i.time > end)
                    break;
            }

            return q - p;
        }

        public decimal GetHighest(DateTime start, DateTime end)
        {
            decimal high = 0;

            foreach (var i in _queue)
                if ((i.time > start) && (i.time <= end))
                    if (i.price > high)
                        high = i.price;

            return high;
        }

        public decimal GetLowest(DateTime start, DateTime end)
        {
            decimal low = 1000;

            foreach (var i in _queue)
                if ((i.time > start) && (i.time <= end))
                    if (i.price < low)
                        low = i.price;

            return low;
        }

        public Direction CheckTend(DateTime start, DateTime end, decimal range, decimal bar)
        {
            decimal downSum = 0;
            decimal upSum = 0;
            decimal last = 0;

            foreach (var i in _queue)
                if ((i.time > start) && (i.time <= end))
                {
                    if (last != 0)
                    {
                        if (i.price > last)
                            upSum += i.price - last;
                        if (i.price < last)
                            downSum += last - i.price;
                    }

                    last = i.price;
                }

            if ((upSum - downSum >= range) && (upSum >= downSum * bar))
                return Direction.Long;

            if ((downSum - upSum >= range) && (downSum >= upSum * bar))
                return Direction.Short;

            return Direction.Null;
        }
    }
}
