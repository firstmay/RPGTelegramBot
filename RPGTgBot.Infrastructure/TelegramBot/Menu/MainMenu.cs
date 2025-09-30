using RPGTgBot.Domain;
using RPGTgBot.Infrastructure.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace RPGTgBot.Infrastructure.TelegramBot.Menu
{
    public class MainMenu : IMenu, IMessageHandledMenu
    {
        public async Task<Result> HandleMessage(long chatId, string message, ITelegramBotClient _bot)
        {
            await _bot.SendMessage(chatId, "Входящее сообщение обработано в MainMenu");
            return Result.Ok();
        }

        public async Task SendMenuAsync(long chatId, ITelegramBotClient bot)
        {
            await bot.SendMessage(chatId, GetMessage(),replyMarkup: GetKeyboard());
        }

        private string GetMessage()
        {
            return "Вы находитесь в главном меню";
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Профиль", "Profile")
                }
            });
        }
    }
}
