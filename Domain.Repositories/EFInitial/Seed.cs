using System.Security.Claims;
using Domain.Models.Catalogues;
using Domain.Models.Tables;
using Microsoft.AspNetCore.Identity;

namespace Domain.Repositories.EFInitial;

public static class Seed
{
    public static async Task SeedData(DataContext context, UserManager<ApplicationUser> userManager)
    {
        var eventCategories = new List<EventCategory>();
        var eventTables = new List<EventTable>();
        var eventTypes = new List<EventType>();
        var ticketDiscounts = new List<TicketDiscount>();
        var ticketTypes = new List<TicketType>();

        var applicationUsers = new List<ApplicationUser>();
        var events = new List<Event>();
        var orders = new List<Order>();
        var tickets = new List<Ticket>();
        var tableEvents = new List<TableEvent>();
        var ticketOrders = new List<TicketOrder>();

        var rnd = new Random();

        if (!userManager.Users.Any())
        {
            applicationUsers =
            [
                new()
                {
                    UserName = "seller1", Firstname = "Seller", Lastname = "Jack", Email = "seller1@test.com",
                    DOB = DateTime.Now, Phone = "123456789"
                },

                new()
                {
                    UserName = "seller2", Firstname = "Seller", Lastname = "Ron", Email = "seller2@test.com",
                    DOB = DateTime.Now, Phone = "223456789"
                },

                new()
                {
                    UserName = "customer", Firstname = "Customer", Lastname = "Wok", Email = "customer1@test.com",
                    DOB = DateTime.Now, Phone = "123456789"
                }
            ];

            foreach (var user in applicationUsers)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");

                var tempUser = await userManager.FindByEmailAsync(user.Email);

                if (tempUser.UserName.Contains("seller"))
                    await userManager.AddClaimAsync(tempUser, new Claim("SellerId", user.Id.ToString().ToUpper()));
                else
                    await userManager.AddClaimAsync(tempUser, new Claim("CustomerId", user.Id.ToString().ToUpper()));
            }
        }
        else
        {
            applicationUsers = context.Users.ToList();
        }

        if (!context.EventCategory.Any())
        {
            eventCategories =
            [
                new()
                {
                    Category = "Music Party"
                },

                new()
                {
                    Category = "Office meeting"
                },

                new()
                {
                    Category = "Artist meeting"
                }
            ];
            await context.EventCategory.AddRangeAsync(eventCategories);
        }
        else
        {
            eventCategories = context.EventCategory.ToList();
        }


        if (!context.EventTable.Any())
        {
            eventTables =
            [
                new()
                {
                    Number = "123",
                    Price = 100,
                    PeopleQuantity = 8
                },

                new()
                {
                    Number = "22",
                    Price = 50,
                    PeopleQuantity = 4
                },

                new()
                {
                    Number = "11",
                    Price = 25,
                    PeopleQuantity = 2
                },

                new()
                {
                    Number = "111",
                    Price = 100,
                    PeopleQuantity = 8
                }
            ];

            await context.EventTable.AddRangeAsync(eventTables);
        }
        else
        {
            eventTables = context.EventTable.ToList();
        }

        if (!context.EventType.Any())
        {
            eventTypes =
            [
                new()
                {
                    Type = "Online"
                },

                new()
                {
                    Type = "Offline"
                },

                new()
                {
                    Type = "On place"
                }
            ];
            await context.EventType.AddRangeAsync(eventTypes);
        }
        else
        {
            eventTypes = context.EventType.ToList();
        }

        if (!context.TicketDiscount.Any())
        {
            ticketDiscounts =
            [
                new()
                {
                    DiscountPercentage = 20
                },

                new()
                {
                    DiscountPercentage = 30
                },

                new()
                {
                    DiscountPercentage = 15
                },

                new()
                {
                    DiscountPercentage = 10
                }
            ];

            await context.TicketDiscount.AddRangeAsync(ticketDiscounts);
        }
        else
        {
            ticketDiscounts = context.TicketDiscount.ToList();
        }

        if (!context.TicketType.Any())
        {
            ticketTypes =
            [
                new()
                {
                    Type = "With place",
                    Price = 350
                },

                new()
                {
                    Type = "Table",
                    Price = 1000
                },

                new()
                {
                    Type = "Simple",
                    Price = 100
                }
            ];
            await context.TicketType.AddRangeAsync(ticketTypes);
        }
        else
        {
            ticketTypes = context.TicketType.ToList();
        }

        if (!context.Event.Any())
        {
            events =
            [
                new()
                {
                    User = applicationUsers.Last(x => x.UserName.Contains("seller")),
                    Title = "Event 1",
                    Category = eventCategories[rnd.Next(0, eventCategories.Count - 1)],
                    Description = "Event 1 description",
                    Place = "Event 1 place",
                    Date = DateTime.Today.AddDays(rnd.Next(-30, 30)),
                    Type = eventTypes[rnd.Next(0, eventTypes.Count - 1)],
                    Moderator = "Alex",
                    TotalPlaces = rnd.Next(30, 200)
                },

                new()
                {
                    User = applicationUsers.First(x => x.UserName.Contains("seller")),
                    Title = "Event 2",
                    Category = eventCategories[rnd.Next(0, eventCategories.Count - 1)],
                    Description = "Event 2 description",
                    Place = "Event 2 place",
                    Date = DateTime.Today.AddDays(rnd.Next(-30, 30)),
                    Type = eventTypes[rnd.Next(0, eventTypes.Count - 1)],
                    Moderator = "Alex",
                    TotalPlaces = rnd.Next(30, 200)
                },

                new()
                {
                    User = applicationUsers.Last(x => x.UserName.Contains("seller")),
                    Title = "Event 3",
                    Category = eventCategories[rnd.Next(0, eventCategories.Count - 1)],
                    Description = "Event 3 description",
                    Place = "Event 1 place",
                    Date = DateTime.Today.AddDays(rnd.Next(-30, 30)),
                    Type = eventTypes[rnd.Next(0, eventTypes.Count - 1)],
                    Moderator = "Alex",
                    TotalPlaces = rnd.Next(30, 200)
                },

                new()
                {
                    User = applicationUsers.First(x => x.UserName.Contains("seller")),
                    Title = "Event 4",
                    Category = eventCategories[rnd.Next(0, eventCategories.Count - 1)],
                    Description = "Event 4 description",
                    Place = "Event 4 place",
                    Date = DateTime.Today.AddDays(rnd.Next(-30, 30)),
                    Type = eventTypes[rnd.Next(0, eventTypes.Count - 1)],
                    Moderator = "Alex",
                    TotalPlaces = rnd.Next(30, 200)
                }
            ];
            await context.Event.AddRangeAsync(events);
        }
        else
        {
            events = context.Event.ToList();
        }

        if (!context.Order.Any())
        {
            orders =
            [
                new()
                {
                    User = applicationUsers.First(x => x.UserName.Contains("customer")),
                },

                new()
                {
                    User = applicationUsers.First(x => x.UserName.Contains("customer")),
                },

                new()
                {
                    User = applicationUsers.Last(x => x.UserName.Contains("customer")),
                },

                new()
                {
                    User = applicationUsers.Last(x => x.UserName.Contains("customer")),
                }
            ];
            await context.Order.AddRangeAsync(orders);
        }
        else
        {
            orders = context.Order.ToList();
        }

        if (!context.Ticket.Any())
        {
            tickets =
            [
                new()
                {
                    Number = 0,
                    Type = ticketTypes[rnd.Next(0, ticketTypes.Count - 1)],
                    Event = events[rnd.Next(0, events.Count - 1)]
                },

                new()
                {
                    Number = 0,
                    Type = ticketTypes[rnd.Next(0, ticketTypes.Count - 1)],
                    Event = events[rnd.Next(0, events.Count - 1)]
                },

                new()
                {
                    Number = 0,
                    Type = ticketTypes[rnd.Next(0, ticketTypes.Count - 1)],
                    Event = events[rnd.Next(0, events.Count - 1)]
                },

                new()
                {
                    Number = 0,
                    Type = ticketTypes[rnd.Next(0, ticketTypes.Count - 1)],
                    Event = events[rnd.Next(0, events.Count - 1)]
                },

                new()
                {
                    Number = 0,
                    Type = ticketTypes[rnd.Next(0, ticketTypes.Count - 1)],
                    Event = events[rnd.Next(0, events.Count - 1)]
                },

                new()
                {
                    Number = 0,
                    Type = ticketTypes[rnd.Next(0, ticketTypes.Count - 1)],
                    Event = events[rnd.Next(0, events.Count - 1)]
                }
            ];
            await context.Ticket.AddRangeAsync(tickets);
        }
        else
        {
            tickets = context.Ticket.ToList();
        }


        if (!context.TableEvent.Any())
        {
            tableEvents =
            [
                new TableEvent
                {
                    Event = events[0],
                    Table = eventTables[0]
                },

                new TableEvent
                {
                    Event = events[1],
                    Table = eventTables[1]
                },

                new TableEvent
                {
                    Event = events[2],
                    Table = eventTables[2]
                }
            ];
            await context.TableEvent.AddRangeAsync(tableEvents);
        }
        else
        {
            tableEvents = context.TableEvent.ToList();
        }

        if (!context.TicketOrder.Any())
        {
            ticketOrders =
            [
                new()
                {
                    Order = orders[0],
                    Ticket = tickets[0]
                },

                new()
                {
                    Order = orders[1],
                    Ticket = tickets[1]
                },

                new()
                {
                    Order = orders[2],
                    Ticket = tickets[2]
                }
            ];
            await context.TicketOrder.AddRangeAsync(ticketOrders);
        }
        else
        {
            ticketOrders = context.TicketOrder.ToList();
        }

        await context.SaveChangesAsync();
    }
}