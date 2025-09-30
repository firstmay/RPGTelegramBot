using RPGTgBot.Domain;
using Telegram.Bot;

namespace RPGTgBot.Infrastructure.TelegramBot.Interfaces
{
    public interface IMessageHandledMenu
    {
        Task<Result> HandleMessage(long chatId, string message, ITelegramBotClient _bot);
    }
}
