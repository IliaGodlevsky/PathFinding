using System;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Benchmarks.Data
{
    internal static class SerializationDataProvider
    {
        public static Dictionary<string, object> Data { get; }

        static SerializationDataProvider()
        {
            var extensiveData = new Dictionary<string, object>
            {
                { "EventId", Guid.NewGuid() },
                { "EventName", "Annual Conference" },
                { "Participants", new List<Dictionary<string, object>>() },
                { "Location", new Dictionary<string, object>
                             {
                                { "City", "New York" },
                                { "Venue", "Convention Center" },
                                { "Coordinates", new Dictionary<string, double>
                                                {
                                                    { "Latitude", 40.7128 },
                                                    { "Longitude", -74.0060 }
                                                }
                                }
                            }
                },
                { "Tags", new List<string>() },
                { "Schedule", new List<Dictionary<string, object>>() }
            };

            for (int i = 1; i <= 10000; i++)
            {
                ((List<Dictionary<string, object>>)extensiveData["Participants"]).Add(new Dictionary<string, object>
                {
                    { "ParticipantId", i },
                    { "Name", $"Participant {i}" },
                    { "RegisteredDate", DateTime.Now.AddDays(-i) }
                });
            }

            for (int i = 1; i <= 1000; i++)
            {
                ((List<string>)extensiveData["Tags"]).Add($"Tag {i}");
            }

            var daysOfWeek = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

            for (int i = 1; i <= 30; i++)
            {
                var daySchedule = new Dictionary<string, object>
                {
                    { "Day", $"Week {((i-1)/5) + 1}, {daysOfWeek[(i-1) % 5]}" },
                    { "Events", new List<string>() }
                };

                for (int j = 1; j <= 20; j++)
                {
                    ((List<string>)daySchedule["Events"]).Add($"Event {i}-{j}");
                }

                ((List<Dictionary<string, object>>)extensiveData["Schedule"]).Add(daySchedule);
            }
            Data = extensiveData;
        }
    }
}
