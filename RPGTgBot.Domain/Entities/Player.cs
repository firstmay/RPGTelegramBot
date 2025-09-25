namespace RPGTgBot.Domain.Entities
{
    public class Player
    {
        public int Id { get; private set; }
        public long TelegramId { get; set; }

        public string Name { get; private set; }
        public DateTime RegistrationDate { get; private set; }


        private Player(int id,long telegramId, string name, DateTime registrationDate)
        {
            Id = id;
            TelegramId = telegramId;
            Name = name;
            RegistrationDate = registrationDate;
        }

        public static Result<Player> Create(int id, long telegramId, string name, DateTime registrationDate)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<Player>.Fail("Нименование некорректно");
            }
            var player = new Player(id, telegramId, name, registrationDate);
            return Result<Player>.Ok(player);
        }
    }
}
