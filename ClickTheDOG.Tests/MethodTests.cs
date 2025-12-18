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

    [Fact]
    public void BuildLevelPriceText_NormalLevel_ReturnsPrice()
    {
        var result = Methods.BuildLevelPriceText(5, 300);
        Assert.Equal("Price: 300", result);
    }

    [Fact]
    public void BuildLevelPriceText_MaxLevel_ReturnsMaxText()
    {
        var result = Methods.BuildLevelPriceText(12, 999);
        Assert.Equal("Elérted a maximális szintet!", result);
    }

    [Fact]
    public void BuildBossTimeText_WhenBossFight_ReturnsFormattedText()
    {
        var text = Methods.BuildBossTimeText(true, 10.5f);
        Assert.Equal("IDŐ: 10.50 mp", text);
    }

    [Fact]
    public void BuildBossTimeText_WhenNotBossFight_ReturnsEmpty()
    {
        var text = Methods.BuildBossTimeText(false, 10f);
        Assert.Equal("", text);
    }

    [Fact]
    public void GetBossTimeColorCode_WhenTimeBelowFive_IsRedCode()
    {
        var code = Methods.GetBossTimeColorCode(true, 3f);
        Assert.Equal(1, code); // 1 = piros
    }

    [Fact]
    public void GetBossTimeColorCode_WhenTimeBetweenFiveAndFifteen_IsWhiteCode()
    {
        var code = Methods.GetBossTimeColorCode(true, 10f);
        Assert.Equal(0, code); // 0 = fehér
    }

    [Fact]
    public void CalculateBossLossRemainingCoin_CannotGoBelowZero()
    {
        var remaining = Methods.CalculateBossLossRemainingCoin(500);
        Assert.Equal(0, remaining);
    }

    [Fact]
    public void CalculateBossLossRemainingCoin_ReducesCorrectly()
    {
        var remaining = Methods.CalculateBossLossRemainingCoin(2000);
        Assert.Equal(1000, remaining);
    }

}
