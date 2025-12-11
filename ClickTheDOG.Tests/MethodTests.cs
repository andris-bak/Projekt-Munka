using Xunit;

public class MethodsTests
{
    [Fact]
    public void BuildLevelPriceText_ShouldShowPrice_WhenLevelIsNotMax()
    {
        // Arrange
        int level = 5;
        int levelPrice = 200;

        // Act
        string text = Methods.BuildLevelPriceText(level, levelPrice);

        // Assert
        Assert.Equal("Price: 200", text);
    }

    [Fact]
    public void BuildLevelPriceText_ShouldShowMaxText_WhenLevelIs12()
    {
        // Arrange
        int level = 12;
        int levelPrice = 9999;

        // Act
        string text = Methods.BuildLevelPriceText(level, levelPrice);

        // Assert
        Assert.Equal("Elérted a maximális szintet!", text);
    }
}
