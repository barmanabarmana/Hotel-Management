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
                = new UsageDbContext(serviceProvider
                .GetRequiredService<DbContextOptions<UsageDbContext>>()))
            {
                // Look for any movies.
                if (context.TourTemplates.Any())
                {
                    return;   // DB has been seeded
                }
                string BeachTourType = "Beach tours";
                string VacationWithCheldrenType = "Vacation with children";
                string FamilyVacationType = "Family vacation";
                string AllInclusive = "All inclusive";
                string WithoutMeal = "Without meal";
                var room1 = new HotelRoom() 
                {
                    Name = "SUPER ECO CLASS",
                    Number = 100,
                    SleepingPlaces = 4,
                    Price = 99.90m
                };

                var room2 = new HotelRoom()
                {
                    Name = "SUPER ECO CLASS",
                    Number = 101,
                        SleepingPlaces = 4,
                        Price = 99.90m
                    };

                var room3 = new HotelRoom()
                {
                    Name = "ULLTRA FIRST CLASS",
                    Number = 102,
                        SleepingPlaces = 4,
                        Price = 199.90m
                    };

                var room4 = new HotelRoom()
                {
                    Name = "MEGA STANDART",
                    Number = 103,
                        SleepingPlaces = 4,
                        Price = 199.90m
                    };
                var room5 = new HotelRoom()
                {
                    Name = "MEGA STANDART",
                    Number = 104,
                    SleepingPlaces = 4,
                    Price = 99.90m
                }; 
                var room6 = new HotelRoom()
                {
                    Name = "ULLTRA FIRST CLASS",
                    Number = 105,
                    SleepingPlaces = 4,
                    Price = 199.90m
                };
                var DefaultHotelRooms = new List<HotelRoom>()
                {
                    room1, room2, room3, room4, room5, room6
                };

                var image1 = new Image()
                {
                    Name = "1",
                    Path = "/Files/1.jpg"
                };
                var image2 = new Image()
                {
                    Name = "2",
                    Path = "/Files/2.jpg"
                };
                var image3 = new Image()
                {
                    Name = "3",
                    Path = "/Files/3.jpg"
                };
                var image4 = new Image()
                {
                    Name = "4",
                    Path = "/Files/4.jpg"
                };
                var image5 = new Image()
                {
                    Name = "5",
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

                var transport1 = new Transport()
                {
                    Type = busType,
                    DeparturePoint = "Ukraine, Kiev",
                    ArrivalPoint = "Egypt, Hurghada",
                    DepartureTime = new DateTime(2022, 02, 24, 15, 54, 00),
                    ArrivalTime = new DateTime(2022, 02, 25, 03, 22, 00),
                    TransportPlaces = new()
                    {
                        new TransportPlace()
                        {
                            Number = 1,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 2,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 3,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 4,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 5,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 6,
                            Price = 64.09M
                        },
                    }
                }; var transport2 = new Transport()
                {
                    Type = busType,
                    DeparturePoint = "Ukraine, Kiev",
                    ArrivalPoint = "Egypt, Hurghada",
                    DepartureTime = new DateTime(2022, 03, 01, 15, 54, 00),
                    ArrivalTime = new DateTime(2022, 03, 02, 03, 22, 00),
                    TransportPlaces = new()
                    {
                        new TransportPlace()
                        {
                            Number = 1,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 2,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 3,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 4,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 5,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 6,
                            Price = 64.09M
                        },
                    }
                };
                var transport3 = new Transport()
                {
                    Type = airType,
                    DeparturePoint = "Ukraine, Kiev",
                    ArrivalPoint = "Egypt, Sharm El Sheikh",
                    DepartureTime = new DateTime(2022, 10, 27, 15, 54, 00),
                    ArrivalTime = new DateTime(2022, 10, 28, 03, 22, 00),
                    TransportPlaces = new()
                    {
                        new TransportPlace()
                        {
                            Number = 1,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 2,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 3,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 4,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 5,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 6,
                            Price = 64.09M
                        },
                    }
                }; 
                var transport4 = new Transport()
                {
                    Type = airType,
                    DeparturePoint = "Ukraine, Kiev",
                    ArrivalPoint = "Egypt, Sharm El Sheikh",
                    DepartureTime = new DateTime(2022, 11, 02, 15, 54, 00),
                    ArrivalTime = new DateTime(2022, 11, 03, 03, 22, 00),
                    TransportPlaces = new()
                    {
                        new TransportPlace()
                        {
                            Number = 1,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 2,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 3,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 4,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 5,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 6,
                            Price = 64.09M
                        },
                    }
                };
                var transport5 = new Transport()
                {
                    Type = busType,
                    DeparturePoint = "somewhere",
                    ArrivalPoint = "somewhere",
                    DepartureTime = new DateTime(2023, 10, 24, 15, 54, 00),
                    ArrivalTime = new DateTime(2023, 10, 25, 03, 22, 00),
                    TransportPlaces = new()
                    {
                        new TransportPlace()
                        {
                            Number = 1,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 2,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 3,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 4,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 5,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 6,
                            Price = 64.09M
                        },
                    }
                }; var transport6 = new Transport()
                {
                    Type = busType,
                    DeparturePoint = "somewhere",
                    ArrivalPoint = "somewhere",
                    DepartureTime = new DateTime(2023, 10, 30, 15, 54, 00),
                    ArrivalTime = new DateTime(2023, 10, 31, 03, 22, 00),
                    TransportPlaces = new()
                    {
                        new TransportPlace()
                        {
                            Number = 1,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 2,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 3,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 4,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 5,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 6,
                            Price = 64.09M
                        },
                    }
                };
                var transport7 = new Transport()
                {
                    Type = airType,
                    DeparturePoint = "somewhere",
                    ArrivalPoint = "somewhere",
                    DepartureTime = new DateTime(2023, 10, 24, 15, 54, 00),
                    ArrivalTime = new DateTime(2023, 10, 25, 03, 22, 00),
                    TransportPlaces = new()
                    {
                        new TransportPlace()
                        {
                            Number = 1,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 2,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 3,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 4,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 5,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 6,
                            Price = 64.09M
                        },
                    }
                }; var transport8 = new Transport()
                {
                    Type = airType,
                    DeparturePoint = "somewhere",
                    ArrivalPoint = "somewhere",
                    DepartureTime = new DateTime(2023, 10, 30, 15, 54, 00),
                    ArrivalTime = new DateTime(2023, 10, 31, 03, 22, 00),
                    TransportPlaces = new()
                    {
                        new TransportPlace()
                        {
                            Number = 1,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 2,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 3,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 4,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 5,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 6,
                            Price = 64.09M
                        },
                    }
                };
                var transport9 = new Transport()
                {
                    Type = airType,
                    DeparturePoint = "somewhere",
                    ArrivalPoint = "somewhere",
                    DepartureTime = new DateTime(2023, 10, 24, 15, 54, 00),
                    ArrivalTime = new DateTime(2023, 10, 25, 03, 22, 00),
                    TransportPlaces = new()
                    {
                        new TransportPlace()
                        {
                            Number = 1,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 2,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 3,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 4,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 5,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 6,
                            Price = 64.09M
                        },
                    }
                }; var transport10 = new Transport()
                {
                    Type = airType,
                    DeparturePoint = "somewhere",
                    ArrivalPoint = "somewhere",
                    DepartureTime = new DateTime(2023, 10, 30, 15, 54, 00),
                    ArrivalTime = new DateTime(2023, 10, 31, 03, 22, 00),
                    TransportPlaces = new()
                    {
                        new TransportPlace()
                        {
                            Number = 1,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 2,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 3,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 4,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 5,
                            Price = 64.09M
                        },
                        new TransportPlace()
                        {
                            Number = 6,
                            Price = 64.09M
                        },
                    }
                };

                string description = "some description";

                context.TourTemplates.AddRange(
                    new Tour
                    {
                        Title = "Mercure Hurghada Hotel with great views",
                        Type = VacationWithCheldrenType, 
                        Country = "Egypt",
                        City = "Hurghada",
                        Meal = AllInclusive,
                        AviablePeopleCount = 1,
                        Hotel = hotel1,
                        IsHotOffer = true,
                        Price = 599.90m,
                        TransportIn = transport1,
                        TransportOut = transport2,
                        /*Transports = new()
                        {
                            transport1, transport2
                        },*/
                        Duration = (transport2.ArrivalTime - transport1.DepartureTime).Days,
                        Description = description,
                    },

                    new Tour
                    {
                        Title = "Albatros Aqua Blu Resort Sharm El Sheikh",
                        Type = BeachTourType,
                        Country = "Egypt",
                        City = "Sharm El Sheikh",
                        Meal = WithoutMeal,
                        AviablePeopleCount = 1,
                        Hotel = hotel2,
                        Price = 2000m,
                        TransportIn = transport3,
                        TransportOut = transport4,
                        /*Transports = new()
                        {
                            transport3,
                            transport4
                        },*/
                        Duration = (transport4.ArrivalTime - transport3.DepartureTime).Days,
                        Description = description,
                    },

                    new Tour
                    {
                        Title = "Tour",
                        Type = FamilyVacationType,
                        Country = "Germany",
                        City = "Dusseldorf",
                        Meal = AllInclusive,
                        AviablePeopleCount = 1,
                        Hotel = hotel3,
                        IsHotOffer = true,
                        Price = 950.59m,
                        TransportIn = transport5,
                        TransportOut = transport6,
                        /*Transports = new()
                        {
                            transport5,
                            transport6
                        },*/
                        Duration = (transport6.ArrivalTime - transport5.DepartureTime).Days,
                        Description = description,
                    },

                    new Tour
                    {
                        Title = "Another tour",
                        Type = VacationWithCheldrenType,
                        Country = "Italy",
                        City = "Pisa",
                        Meal = WithoutMeal,
                        AviablePeopleCount = 2,
                        Hotel = hotel4,
                        IsHotOffer = true,
                        Price = 1300m,
                        TransportIn = transport7,
                        TransportOut = transport8,
                        /*Transports = new()
                        {
                            transport7,
                            transport8
                        },*/
                        Duration = (transport6.ArrivalTime - transport7.DepartureTime).Days,
                        Description = description,
                    },
                    new Tour
                    {
                        Title = "Some tour",
                        Type = BeachTourType,
                        Country = "Turkey",
                        City = "Antalya",
                        Meal = AllInclusive,
                        AviablePeopleCount = 2,
                        Hotel = hotel5,
                        Price = 1500m,
                        TransportIn = transport9,
                        TransportOut = transport10,
                        /*Transports = new()
                        {
                            transport9,
                            transport10
                        },*/
                        Duration = (transport10.ArrivalTime - transport9.DepartureTime).Days,
                        Description = description,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
