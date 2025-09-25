using FluentAssertions;
using RPGTgBot.Domain.Entities;

namespace DomainTests
{
    public class PlayerTests
    {
        [Fact]
        public void CreatePlayer_ShouldBeFalse_WhereNameIsEmpty()
        {
            var playerResult = Player.Create(1,1, string.Empty, DateTime.Now);

            playerResult.IsSuccess.Should().BeFalse();
            playerResult.Value.Should().BeNull();
        }

        [Fact]
        public void CreatePlayer_ShouldBeFalse_WhereNameIsSpaces()
        {
            var playerResult = Player.Create(1, 1, " ", DateTime.Now);

            playerResult.IsSuccess.Should().BeFalse();
            playerResult.Value.Should().BeNull();
        }

        [Fact]
        public void CreatePlayer_ShouldBeTrue_WhereNameIsCorrect()
        {
            string name = "MyName";
            var playerResult = Player.Create(1, 1, name, DateTime.Now);

            playerResult.IsSuccess.Should().BeTrue();
            playerResult.Value.Should().NotBeNull();
            playerResult.Value.Name.Should().Be(name);
        }
    }
}