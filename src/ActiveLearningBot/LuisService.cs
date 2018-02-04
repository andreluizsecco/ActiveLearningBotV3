using Cognitive.LUIS.Programmatic;
using Cognitive.LUIS.Programmatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveLearningBot
{
    [Serializable]
    public class LuisService
    {
        public async Task LearnLatestMessage(string intent)
        {
            var message = DB.GetLatestMessage();
            if (!string.IsNullOrEmpty(message))
                await Learn(message, intent);
        }

        public async Task Learn(string message, string intent)
        {
            IEnumerable<Training> trainingList;
            var client = new LuisProgClient("{YourSubscriptionKey}", Location.WestUS);
            var app = await client.GetAppByNameAsync("Hotel");
            await client.AddExampleAsync(app.Id, app.Endpoints.Production.VersionId, new Example
            {
                Text = message,
                IntentName = intent
            });

            await client.TrainAsync(app.Id, app.Endpoints.Production.VersionId);
            do
            {
                trainingList = await client.GetTrainingStatusListAsync(app.Id, app.Endpoints.Production.VersionId);
            }
            while (!trainingList.All(x => x.Details.Status.Equals("Success")));

            await client.PublishAsync(app.Id, app.Endpoints.Production.VersionId, false, "westus");
            await client.PublishAsync(app.Id, app.Endpoints.Production.VersionId, false, "westcentralus");
        }
    }
}