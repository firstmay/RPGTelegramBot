using FluentAssertions;
using RPGTgBot.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests
{
    public class BuildingTypeTests
    {
        [Fact]
        public void Created_ShouldBeFalse_WhereNameIsEmpty()
        {
            var typeResult = BuildingType.Create(string.Empty);

            typeResult.IsSuccess.Should().BeFalse();
            typeResult.Value.Should().BeNull();
            typeResult.Error.Should().NotBeNull();
        }

        [Fact]
        public void Created_ShouldBeFalse_WhereNameIsSpaces()
        {
            var typeResult = BuildingType.Create(" ");

            typeResult.IsSuccess.Should().BeFalse();
            typeResult.Value.Should().BeNull();
            typeResult.Error.Should().NotBeNull();
        }

        [Fact]
        public void Created_ShouldBeTrue_WhereNameIsCorrected()
        {
            string name = "Ратуша";
            var typeResult = BuildingType.Create(name);

            typeResult.IsSuccess.Should().BeTrue();
            typeResult.Value.Should().NotBeNull();
            typeResult.Value.Type.Should().Be(name);
            typeResult.Error.Should().Be(string.Empty);
        }
    }
}
