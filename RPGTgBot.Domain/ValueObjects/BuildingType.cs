namespace RPGTgBot.Domain.ValueObjects
{
    public class BuildingType
    {
        public string Type { get; private set; }

        private BuildingType(string type) 
        {
            Type = type;
        }

        public static Result<BuildingType> Create(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return Result<BuildingType>.Fail("Тип не может быть пустым.");
            
            var createdType = new BuildingType(type);
            return Result<BuildingType>.Ok(createdType);
        }
    }
}
