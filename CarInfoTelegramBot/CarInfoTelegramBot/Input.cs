using System;

namespace CarInfoTelegramBot
{
    public class Input
    {
        private int _mileage;
        private DateTime _dateTime;

        public void SetMileage(int mileage)
        {
            _mileage = mileage;
            _dateTime = DateTime.Now;
        }
    }
}