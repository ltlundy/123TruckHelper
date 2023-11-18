﻿using _123TruckHelper.Models.Data;
using _123TruckHelper.Models.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace _123TruckHelper.Services
{
    public class TruckService : ITruckService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public TruckService(IServiceScopeFactory serviceScopeFactory) { 
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task CreateOrUpdateTruckAsync(TruckData truckData)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            var existing_truck = await dbContext.Trucks.Where(t => t.TruckId == truckData.TruckId).SingleOrDefaultAsync();

            try
            {
                if (existing_truck != null)
                {
                    if (!existing_truck.Busy)
                    {
                        existing_truck.PositionLatitude = truckData.PositionLatitude;
                        existing_truck.PositionLongitude = truckData.PositionLongitude;
                        existing_truck.NextTripLengthPreference = truckData.NextTripLengthPreference;
                    }
                }
                else
                {
                    var truck = new Truck
                    {
                        TruckId = truckData.TruckId,
                        EquipType = truckData.EquipType,
                        PositionLatitude = truckData.PositionLatitude,
                        PositionLongitude = truckData.PositionLongitude,
                        NextTripLengthPreference = truckData.NextTripLengthPreference,
                        Busy = truckData.Busy
                    };
                    await dbContext.Trucks.AddAsync(truck);
                }
                await dbContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}