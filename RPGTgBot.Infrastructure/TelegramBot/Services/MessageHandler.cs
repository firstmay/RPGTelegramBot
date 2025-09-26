using RPGTgBot.Infrastructure.TelegramBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RPGTgBot.Infrastructure.TelegramBot.Services
{
    public class MessageHandler : IMessageHandler
    {
        public Task HandleAsync(ITelegramBotClient client, Message update, CancellationToken token)
        {

        }
    }
}
