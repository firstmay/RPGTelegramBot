namespace RPGTgBot.Domain.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private Player(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Result<Player> Create(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<Player>.Fail("Нименование некорректно");
            }
            var player = new Player(id, name);
            return Result<Player>.Ok(player);
        }
    }
}
