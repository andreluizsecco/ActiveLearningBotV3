using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace ActiveLearningBot.Dialogs
{
    [Serializable]
    [LuisModel("{ModelId}", "{SubscriptionKey}")]
    public class RootDialog : LuisDialog<object>
    {
        private readonly LuisService luisService;

        public RootDialog() =>
            luisService = new LuisService();

        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            DB.SaveMessage(result.Query);
            var cardActions = new List<CardAction>
            {
                new CardAction
                {
                    Title = "Fazer uma reserva",
                    Type = ActionTypes.ImBack,
                    Value = "Fazer uma reserva"
                },
                new CardAction
                {
                    Title = "Solicitar o serviço de quarto",
                    Type = ActionTypes.ImBack,
                    Value = "Solicitar o serviço de quarto"
                },
                new CardAction
                {
                    Title = "Solicitar o serviço de despertador",
                    Type = ActionTypes.ImBack,
                    Value = "Solicitar o serviço de despertador"
                },
                new CardAction
                {
                    Title = "Estou apenas te cumprimentando :)",
                    Type = ActionTypes.ImBack,
                    Value = "Estou apenas te cumprimentando"
                },
                new CardAction
                {
                    Title = "Nenhuma",
                    Type = ActionTypes.ImBack,
                    Value = "Nenhuma"
                }
            };

            var card = new HeroCard
            {
                Title = "Desculpe...",
                Text = "Ainda estou aprendendo, qual dessas opções representa o que você deseja?",
                Buttons = cardActions
            };

            var activity = context.MakeMessage();
            activity.Id = new Random().Next().ToString();
            activity.Attachments.Add(card.ToAttachment());

            await context.PostAsync(activity);
        }

        [LuisIntent("Saudacao")]
        public async Task Saudacao(IDialogContext context, LuisResult result)
        {
            LearnLatestMessage(result.TopScoringIntent.Intent);

            await context.PostAsync($"Bem-vindo(a) ao Hotel! Sou o seu atendente virtual, o que deseja?");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Reserva")]
        public async Task Reserva(IDialogContext context, LuisResult result)
        {
            LearnLatestMessage(result.TopScoringIntent.Intent);

            await context.PostAsync($"Ótimo! Esses são os quartos que estão disponíveis...");
            context.Wait(MessageReceived);
        }

        [LuisIntent("ServicoQuarto")]
        public async Task ServicoQuarto(IDialogContext context, LuisResult result)
        {
            LearnLatestMessage(result.TopScoringIntent.Intent);

            await context.PostAsync($"Entendi, iremos enviar nosso colaborador, qual é o número do seu quarto?");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Despertador")]
        public async Task Despertador(IDialogContext context, LuisResult result)
        {
            LearnLatestMessage(result.TopScoringIntent.Intent);

            await context.PostAsync($"Certo, pode contar comigo, não irei dormir. Que horário deseja ser acordado?");
            context.Wait(MessageReceived);
        }

        private void LearnLatestMessage(string intent)
        {
            HostingEnvironment.QueueBackgroundWorkItem(ct => 
                luisService.LearnLatestMessage(intent));
        }
    }
}