﻿using CarInfoCommon.Models;

namespace CarInfoTelegramBot.Services
{
    public interface IReceiver
    {
        bool Message(CarInfo carInfo);
    }
}