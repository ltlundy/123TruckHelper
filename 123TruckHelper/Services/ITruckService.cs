﻿using _123TruckHelper.Models.Data;

namespace _123TruckHelper.Services
{
    public interface ITruckService
    {
        Task CreateOrUpdateTruckAsync(TruckData truckData);
    }
}