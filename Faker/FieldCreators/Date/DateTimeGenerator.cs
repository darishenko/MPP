using System;

namespace FieldCreators.Date
{
    internal class DateTimeCreator : IPrimitiveTypeCreator
    {
        public Type CurType { get; }

        public DateTimeCreator()
        {
            CurType = typeof(DateTime);
        }

        public object Create()
        {
            var random = new Random();
            var hour = random.Next(0, 24);
            var minute = random.Next(0, 60);
            var second = random.Next(0, 60);
            var millisecond = random.Next(0, 1000);
            var year = random.Next(DateTime.MinValue.Year, DateTime.MaxValue.Year);
            var month = random.Next(1, 13);
            var day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);

            return new DateTime(year, month, day, hour, minute, second, millisecond);
        }
    }
}