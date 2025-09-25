using RPGTgBot.Domain;
using RPGTgBot.Domain.Entities;

namespace RPGTgBot.Infrastructure.Entities
{
    public class User
    {
        public int Id { get; set; }
        public long TelegramId { get; set; }
        public required string Name { get; set; }
        public DateTime RegistrationDate { get; set; }


        public static User ConvertFrom(Player player)
        {
            return new User()
            {
                Id = player.Id,
                Name = player.Name,
                RegistrationDate = player.RegistrationDate,
                TelegramId = player.TelegramId,
            };
        }

        public Result<Player> ConvertToPlayer()
        {
            var result = Player.Create(Id, TelegramId, Name, RegistrationDate);
            if (!result.IsSuccessAndNotNull())
            {
                return Result<Player>.Fail(result.Error);
            }
            return Result<Player>.Ok(result.Value);
        }
    }
}
