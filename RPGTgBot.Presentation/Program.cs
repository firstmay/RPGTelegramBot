
using Microsoft.EntityFrameworkCore;
using RPGTgBot.Infrastructure.DataBaseContext;
using RPGTgBot.Infrastructure.TelegramBot.Interfaces;
using RPGTgBot.Infrastructure.TelegramBot.Services;
using Telegram.Bot;

namespace RPGTgBot.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<RPGDbContext>(options => options.UseNpgsql(connectionString));

            var tgToken = builder.Configuration["TgToken"];
            if(tgToken == null)
            {
                throw new Exception("Отсутствует токен для бота");
            }

            builder.Services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(tgToken));
            builder.Services.AddHostedService<TelegramBotService>();
            builder.Services.AddSingleton<IHandlerUpdate, HandlerUpdate>();
            
            // Add services to the container.


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
