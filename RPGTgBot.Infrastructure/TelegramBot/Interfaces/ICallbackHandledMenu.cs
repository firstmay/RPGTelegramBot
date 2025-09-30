using Telegram.Bot;
using Telegram.Bot.Types;

namespace RPGTgBot.Infrastructure.TelegramBot.Interfaces
{
    public interface ICallbackHandledMenu
    {
        Task HandleCallback(long chatId, string callbackData, ITelegramBotClient _bot);
    }
}
