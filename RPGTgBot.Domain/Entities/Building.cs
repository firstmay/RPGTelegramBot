using RPGTgBot.Domain.ValueObjects;

namespace RPGTgBot.Domain.Entities
{
    public class Building : Entity
    {
        public string Name { get; private set; }
        public BuildingType BuildingType { get; private set; }
        public int Level { get; private set; }

        private Building(int id, BuildingType buildingType, int level)
        {
            SetId(id);
            BuildingType = buildingType;
            Level = level;
        }

        public static Result<Building> Create(int id, BuildingType buildingType, int level = 1)
        {
            if(level < 1)
            {
                return Result<Building>.Fail("Уровень не может быть меньше 1");
            }
            if (buildingType  == null)
            {
                return Result<Building>.Fail("Тип здания не определен.");
            }

            var building = new Building(id, buildingType, level);
            return Result<Building>.Ok(building);
        }
    }
}
