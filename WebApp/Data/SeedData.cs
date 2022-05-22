using DAL;
using Entities;
using Entities.Files;
using Entities.Hotels;
using Entities.Transports;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context
                = new ManagementDbContext(serviceProvider
                .GetRequiredService<DbContextOptions<ManagementDbContext>>()))
            {
                // Look for any movies.
                if (context.TourTemplates.Any())
                {
                    return;   // DB has been seeded
                }
                string BeachTourType = "Beach tours";
                string VacationWithCheldrenType = "Vacation with children";
                string FamilyVacationType = "Family vacation";

                var room1 = new HotelRoom() {
                    Number = 100,
                    SleepingPlaces = 4,
                    Price = 99.90m
                };

                var room2 = new HotelRoom() {
                        Number = 101,
                        SleepingPlaces = 4,
                        Price = 99.90m
                    };

                var room3 = new HotelRoom() {
                        Number = 102,
                        SleepingPlaces = 4,
                        Price = 99.90m
                    };

                var room4 = new HotelRoom() {
                        Number = 103,
                        SleepingPlaces = 4,
                        Price = 99.90m
                    };
                var room5 = new HotelRoom()
                {
                    Number = 104,
                    SleepingPlaces = 4,
                    Price = 99.90m
                }; 
                var room6 = new HotelRoom()
                {
                    Number = 105,
                    SleepingPlaces = 4,
                    Price = 199.90m
                };
                var DefaultHotelRooms = new List<HotelRoom>()
                {
                    room1, room2, room3, room4
                };

                var image1 = new Image()
                {
                    Path = "/Files/1.jpg"
                };
                var image2 = new Image()
                {
                    Path = "/Files/2.jpg"
                };
                var image3 = new Image()
                {
                    Path = "/Files/3.jpg"
                };
                var image4 = new Image()
                {
                    Path = "/Files/4.jpg"
                };
                var image5 = new Image()
                {
                    Path = "/Files/5.jpg"
                };

                var hotel1 = new Hotel()
                {
                    Name = "Mercure Hurghada Hotel",
                    Address = "Some Address",
                    Rooms = DefaultHotelRooms,
                    Stars = 4,
                    Images = new List<Image>() { image1 }
                };
                var hotel2 = new Hotel()
                {
                    Name = "Albatros Aqua Blu Resort",
                    Address = "Some Address",
                    Rooms = DefaultHotelRooms,
                    Stars = 4,
                    Images = new List<Image>() { image2 }
                };
                var hotel3 = new Hotel()
                {
                    Name = "hotel3",
                    Address = "Some Address",
                    Rooms = DefaultHotelRooms,
                    Stars = 4,
                    Images = new List<Image>() { image3 }
                };
                var hotel4 = new Hotel()
                {
                    Name = "hotel4",
                    Address = "Some Address",
                    Rooms = DefaultHotelRooms,
                    Stars = 4,
                    Images = new List<Image>() { image4 }
                };
                var hotel5 = new Hotel()
                {
                    Name = "hotel5",
                    Address = "Some Address",
                    Rooms = DefaultHotelRooms,
                    Stars = 4,
                    Images = new List<Image>() { image5 }
                };

                string airType = "Plane";
                string busType = "Bus";

                var transportPlaces1 = new TransportPlace()
                {
                    Number = 1,
                    Price = 15.49M
                };
                var transportPlaces2 = new TransportPlace()
                {
                    Number = 1,
                    Price = 64.09M
                };

                var transport1 = new Transport()
                {
                    Type = airType + " " + busType,
                    DeparturePoint = "Ukraine, Kiev",
                    ArrivalPoint = "Egypt, Hurghada",
                    DepartureTime = new DateTime(2022, 02, 24, 15, 54, 00),
                    ArrivalTime = new DateTime(2022, 02, 25, 03, 22, 00),
                    TransportPlaces = new List<TransportPlace>() { transportPlaces1, transportPlaces2 }
                }; var transport2 = new Transport()
                {
                    Type = airType + " " + busType,
                    DeparturePoint = "Ukraine, Kiev",
                    ArrivalPoint = "Egypt, Hurghada",
                    DepartureTime = new DateTime(2022, 03, 01, 15, 54, 00),
                    ArrivalTime = new DateTime(2022, 03, 02, 03, 22, 00),
                    TransportPlaces = new List<TransportPlace>() { transportPlaces1, transportPlaces2 }
                };
                var transport3 = new Transport()
                {
                    Type = airType + " " + busType,
                    DeparturePoint = "Ukraine, Kiev",
                    ArrivalPoint = "Egypt, Sharm El Sheikh",
                    DepartureTime = new DateTime(2022, 10, 27, 15, 54, 00),
                    ArrivalTime = new DateTime(2022, 10, 28, 03, 22, 00),
                    TransportPlaces = new List<TransportPlace>() { transportPlaces1, transportPlaces2 }
                }; 
                var transport4 = new Transport()
                {
                    Type = airType + " " + busType,
                    DeparturePoint = "Ukraine, Kiev",
                    ArrivalPoint = "Egypt, Sharm El Sheikh",
                    DepartureTime = new DateTime(2022, 11, 02, 15, 54, 00),
                    ArrivalTime = new DateTime(2022, 11, 03, 03, 22, 00),
                    TransportPlaces = new List<TransportPlace>() { transportPlaces1, transportPlaces2 }
                };
                var transport5 = new Transport()
                {
                    Type = airType,
                    DeparturePoint = "somewhere",
                    ArrivalPoint = "somewhere",
                    DepartureTime = new DateTime(2023, 10, 24, 15, 54, 00),
                    ArrivalTime = new DateTime(2023, 10, 25, 03, 22, 00),
                    TransportPlaces = new List<TransportPlace>() { transportPlaces1 }
                }; var transport6 = new Transport()
                {
                    Type = airType,
                    DeparturePoint = "somewhere",
                    ArrivalPoint = "somewhere",
                    DepartureTime = new DateTime(2023, 10, 30, 15, 54, 00),
                    ArrivalTime = new DateTime(2023, 10, 31, 03, 22, 00),
                    TransportPlaces = new List<TransportPlace>() { transportPlaces1 }
                };

                string description = "some description";

                context.TourTemplates.AddRange(
                    new Tour
                    {
                        Title = "Mercure Hurghada Hotel with great views",
                        Type = VacationWithCheldrenType, 
                        Country = "Egypt",
                        City = "Hurghada",
                        Hotel = hotel1,
                        IsHotOffer = true,
                        Price = 599.90m,
                        TransportIn = transport1,
                        TransportOut = transport2,
                        Duration = (transport2.ArrivalTime - transport1.DepartureTime).Days,
                        Description = description,
                    },

                    new Tour
                    {
                        Title = "Albatros Aqua Blu Resort Sharm El Sheikh",
                        Type = BeachTourType,
                        Country = "Egypt",
                        City = "Sharm El Sheikh",
                        Hotel = hotel2,
                        Price = 2000m,
                        TransportIn = transport3,
                        TransportOut = transport4,
                        Duration = (transport4.ArrivalTime - transport3.DepartureTime).Days,
                        Description = description,
                    },

                    new Tour
                    {
                        Title = "Tour",
                        Type = FamilyVacationType,
                        Country = "Germany",
                        City = "Dusseldorf",
                        Hotel = hotel3,
                        IsHotOffer = true,
                        Price = 950.59m,
                        TransportIn = transport1,
                        TransportOut = transport2,
                        Duration = (transport2.ArrivalTime - transport1.DepartureTime).Days,
                        Description = description,
                    },

                    new Tour
                    {
                        Title = "Another tour",
                        Type = VacationWithCheldrenType,
                        Country = "Italy",
                        City = "Pisa",
                        Hotel = hotel4,
                        IsHotOffer = true,
                        Price = 1300m,
                        TransportIn = transport5,
                        TransportOut = transport6,
                        Duration = (transport6.ArrivalTime - transport5.DepartureTime).Days,
                        Description = description,
                    },
                    new Tour
                    {
                        Title = "Some tour",
                        Type = BeachTourType,
                        Country = "Turkey",
                        City = "Antalya",
                        Hotel = hotel5,
                        Price = 1500m,
                        TransportIn = transport5,
                        TransportOut = transport6,
                        Duration = (transport6.ArrivalTime - transport5.DepartureTime).Days,
                        Description = description,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
