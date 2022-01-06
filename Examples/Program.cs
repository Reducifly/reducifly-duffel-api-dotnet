﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Duffel.ApiClient;
using Duffel.ApiClient.Interfaces.Models;
using Duffel.ApiClient.Interfaces.Models.Requests;

namespace Examples
{
    public class Examples
    {

        public static async Task Main(string[] args)
        {
            var client = new DuffelApiClient(args[0]);
            var offers = await client.CreateOffersRequest(new OffersRequest
            {
                Passengers = new List<Passenger> { new Passenger { PassengerType = PassengerType.Adult } },
                RequestedSources = new List<string> { "duffel_airways" },
                Slices = new List<Slice>
                {
                    new Slice
                    {
                        Origin = "SFO",
                        Destination = "JFK",
                        DepartureDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd")
                    }
                }
            });

            offers.Slices.ToList()
                .ForEach(slice => Console.WriteLine($"Slice: {slice.Origin.Id} -> {slice.Destination.Id}"));
        }

    }
    
}

