using RPGTgBot.Infrastructure.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RPGTgBot.Infrastructure.TelegramBot.Services
{
    public class MessageHandler : IMessageHandler
    {
        public Task HandleAsync(ITelegramBotClient client, Message update, CancellationToken token)
        {
            /// Обработка сообщений происходит:
            /// - Проверяем зарегистрирован ли пользователь
            /// - нет? запускаем процесс регистрации.
            /// - зареган? обрабатываем действие. 
            /// - Действие обрабатывается в соответствии с состоянием пользователя.
            return Task.CompletedTask;
        }
    }
}
