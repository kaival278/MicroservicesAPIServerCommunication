using System;
using MassTransit;

namespace APIServerConsumer
{
    public class WeatherForcastConsumer : IConsumer<String>
    {
     
        public async Task Consume(ConsumeContext<String> context)
        {
            var data = context.Message;
            // Make A service call / Controller call from here to save message to database.

        }
    }
}

