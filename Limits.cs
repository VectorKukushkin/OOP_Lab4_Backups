using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Lab4_Backups
{
    class Limits
    {
        public abstract class Limit
        {
            public abstract bool Achieved(int count, DateTime date, long size);
        }

        public class CountLimit : Limit
        {
            public CountLimit(int count)
            {
                Count = count;
            }

            public readonly int Count;

            public override bool Achieved(int count, DateTime date, long size)
            {
                if (count > Count)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public class DateLimit : Limit
        {
            public DateLimit(DateTime date)
            {
                Date = date;
            }

            public readonly DateTime Date;

            public override bool Achieved(int count, DateTime date, long size)
            {
                if (date > Date)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public class SizeLimit : Limit
        {
            public SizeLimit(long size)
            {
                Size = size;
            }

            public readonly long Size;

            public override bool Achieved(int count, DateTime date, long size)
            {
                if (size > Size)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public class HybridLimit : Limit
        {
            public HybridLimit(bool condition, Limit limit1, Limit limit2)
            {
                Condition = condition;
                Limit1 = limit1;
                Limit2 = limit2;
            }

            // true = and
            public readonly bool Condition;
            public readonly Limit Limit1;
            public readonly Limit Limit2;

            public override bool Achieved(int count, DateTime date, long size)
            {
                if (Condition)
                {
                    return Limit1.Achieved(count, date, size) && Limit2.Achieved(count, date, size);
                }
                else
                {
                    return Limit1.Achieved(count, date, size) || Limit2.Achieved(count, date, size);
                }
            }
        }
    }
}
