using RPGTgBot.Infrastructure.TelegramBot.Interfaces;
using RPGTgBot.Infrastructure.TelegramBot.Menu;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RPGTgBot.Infrastructure.TelegramBot.Services
{
    public class MessageHandler(IMenuManager menuManager) : IMessageHandler
    {
        private readonly IMenuManager _menuManager = menuManager;

        public async Task HandleAsync(ITelegramBotClient client, Message update, CancellationToken token)
        {
            /// Обработка сообщений происходит:
            /// - Проверяем зарегистрирован ли пользователь
            /// - нет? запускаем процесс регистрации.
            /// - зареган? обрабатываем действие. 
            /// - Действие обрабатывается в соответствии с состоянием пользователя.

            if(update.Text == string.Empty || update.Text is null) return;

            string message = update.Text;
            switch (message)
            {
                case var m when message.StartsWith("/"):
                    await HandleCommandAsync(update.Chat.Id, m, token);
                    break;
                default:
                    await _menuManager.HandleMessage(message, update.Chat.Id);
                    break;
            }

        }

        private async Task HandleCommandAsync(long chatId, string command, CancellationToken token)
        {
            switch (command)
            {
                case "/start":
                    await _menuManager.SwitchToMenuAsync<MainMenu>(chatId);
                    break;
            }
        }
    }
}
