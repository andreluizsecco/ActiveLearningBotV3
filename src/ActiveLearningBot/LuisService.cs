using Cognitive.LUIS.Programmatic;
using Cognitive.LUIS.Programmatic.Models;
using System;
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
            using (var client = new LuisProgClient("{YourSubscriptionKey}", Regions.WestUS))
            {
                var app = await client.GetAppByNameAsync("Hotel");
                await client.AddExampleAsync(app.Id, app.Endpoints.Production.VersionId, new Example
                {
                    Text = message,
                    IntentName = intent
                });

                var trainingDetails = await client.TrainAndGetFinalStatusAsync(app.Id, app.Endpoints.Production.VersionId);
                if (trainingDetails.Status.Equals("Success"))
                {
                    await client.PublishAsync(app.Id, app.Endpoints.Production.VersionId, false, "westus");
                    await client.PublishAsync(app.Id, app.Endpoints.Production.VersionId, false, "westcentralus");
                }
            }
        }
    }
}