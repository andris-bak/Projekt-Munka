#  Unit tesztek

**Projekt neve:** Click the Dog (CTD)

**Készítette:** M.A.K.E Kft.

**Tagok:** Venyige Márk, Bak András Mátyás, Kovács Krisztián, Jabur Emil

---
# Unit Tesztelési Dokumentáció - Click the Dog

Ez a dokumentum a `ClickTheDOG.Tests` projektben implementált automatizált teszteket részletezi. A tesztek célja a játékmenet alapvető logikai elemeinek (matematikai számítások, korlátozások, szöveggenerálás) validálása.

---

## 1. Tesztelési Összefoglaló Táblázat

| Teszt ID | Metódus neve | Cél | Prioritás |
| :--- | :--- | :--- | :--- |
| **UT01** | `CalculateNewHP_Test` | Sebzés levonásának pontossága. | Kritikus |
| **UT02** | `MaxLevelText_Test` | Maximális szint elérésekor a helyes üzenet megjelenítése. | Alacsony |
| **UT03** | `GoldReward_Test` | Szint alapú pénzjutalom kiszámítása. | Magas |
| **UT04** | `RegenerationLimit_Test` | HP regeneráció megállítása a maximum értéknél. | Magas |
| **UT05** | `ElementalDamage_Test` | Típusok közötti ellenállás/sebzés szorzók ellenőrzése. | Közepes |
| **UT06** | `DataReset_Test` | Mentés törlésekor az adatok alaphelyzetbe állítása. | Magas |

---

## 2. Implementált Teszt Kód (xUnit)

A tesztek a `.NET 8` környezetben futtathatóak az xUnit keretrendszer segítségével.

```csharp
using Xunit;
using ClickTheDOG.Logic;

namespace ClickTheDOG.Tests
{
    public class GameLogicTests
    {
        [Fact]
        public void UT01_CalculateNewHP_ShouldDecreaseHP_WhenDamageIsApplied()
        { 
            int currentHP = 100;
            int damage = 25;
            int expected = 75;
           
            int result = Methods.CalculateNewHP(currentHP, damage);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void UT02_BuildLevelPriceText_ShouldReturnMaxLevelMessage_WhenLevelIs12()
        {
            int level = 12;
            int price = 1000;
            string expected = "Elérted a maximális szintet!";

            string result = Methods.BuildLevelPriceText(level, price);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 10)]
        [InlineData(10, 100)]
        public void UT03_CalculateGoldReward_ShouldReturnCorrectAmountBasedOnLevel(int level, int expected)
        {
            int result = Methods.CalculateGoldReward(level);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void UT04_ApplyRegeneration_ShouldNotExceedMaxHP()
        {
            int currentHP = 98;
            int regen = 5;
            int maxHP = 100;
            int expected = 100;

            int result = Methods.ApplyRegeneration(currentHP, regen, maxHP);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void UT05_CalculateElementalDamage_ShouldReduceDamageOnResistance()
        {
            float baseDmg = 100f;
            string attacker = "Fire";
            string defender = "Water"; // A víz ellenáll a tűznek
            float expected = 50f; 

            float result = Methods.CalculateElementalDamage(baseDmg, attacker, defender);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void UT06_ResetData_ShouldSetDefaultsCorrectly()
        {
            var player = new PlayerData { Level = 5, Gold = 1500 };

            Methods.ResetData(player);

            Assert.Equal(1, player.Level);
            Assert.Equal(0, player.Gold);
        }
    }
}
