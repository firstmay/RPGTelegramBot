using FluentAssertions;
using RPGTgBot.Domain.Entities;
using RPGTgBot.Domain.ValueObjects;

namespace Domain.Tests
{
    public class BuildingTests
    {
        [Fact]
        public void Create_ShouldBeTrue_WhereCorrectedData()
        {
            var typeResult = BuildingType.Create("Ратуша");
            if (!typeResult.IsSuccessAndNotNull())
            {
                return;
            }

            var buildingResult = Building.Create(0, typeResult.Value);

            buildingResult.IsSuccess.Should().BeTrue();
            buildingResult.Should().NotBeNull();
            buildingResult.Value.Should().NotBeNull();
        }

        [Fact]
        public void Create_ShouldBeFalse_WhereLevelLessThanOne()
        {
            var typeResult = BuildingType.Create("Ратуша");
            if (!typeResult.IsSuccessAndNotNull())
            {
                return;
            }

            var buildingResult = Building.Create(0, typeResult.Value, 0);

            buildingResult.IsSuccess.Should().BeFalse();
            buildingResult.Should().NotBeNull();
            buildingResult.Value.Should().BeNull();
            buildingResult.Error.Should().NotBeNull();
        }

        [Fact]
        public void Create_ShouldBeFalse_WhereBuildingTypeIsNull()
        {
            var buildingResult = Building.Create(0, null);

            buildingResult.IsSuccess.Should().BeFalse();
            buildingResult.Should().NotBeNull();
            buildingResult.Value.Should().BeNull();
            buildingResult.Error.Should().NotBeNull();
        }
    }
}
