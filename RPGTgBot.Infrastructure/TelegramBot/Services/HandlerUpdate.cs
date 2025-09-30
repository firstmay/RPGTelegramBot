using RPGTgBot.Application.Queries;
using RPGTgBot.Infrastructure.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RPGTgBot.Infrastructure.TelegramBot.Services
{
    
    public class HandlerUpdate(
        IMessageHandler messageHandler,
        CheckPlayerRegistrationHandler checkPlayerRegistration
        ) : IHandlerUpdate
    {
        private readonly IMessageHandler _MessageHandler = messageHandler;
        private readonly CheckPlayerRegistrationHandler _checkPlayerRegistration = checkPlayerRegistration;

        public Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            var error = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(error);
            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            switch (update.Type)
            {
                case UpdateType.Message:
                    if(update.Message != null)
                    {
                        var result = await _checkPlayerRegistration.HandleAsync(new(update.Message.Chat.Id));
                        if (!result.IsSuccess)
                        {
                            await botClient.SendMessage(update.Message.Chat.Id, result.Error);
                            return;
                        }
                        await _MessageHandler.HandleAsync(botClient, update.Message, cancellationToken);
                    }
                    //await botClient.SendMessage(update.Message!.Chat.Id, update.Message.Text!);
                    break;
            }
        }

    }
}
