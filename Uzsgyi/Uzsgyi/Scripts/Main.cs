using Godot;
using System;
using Godot.Collections;

public partial class Main : Node2D
{
	Random rnd = new Random();
	private int score = 0;
	private int coin = 0;
	private int hp = 10;
	private int level = 1;
	private int levelprice = 2;
	private int min = 10;
	private int max = 20;
	private int counter = 1;
	private Player player;
	private Enemy enemy;
	
	private Texture2D[] enemyTextures;
	private int currentEnemyIndex;
	
	[Export]
	public Label ScoreLabel;
	[Export]
	public Label CoinLabel;
	[Export]
	public Label HPLabel;
	[Export]
	public ProgressBar HPBar;
	[Export]
	public Label LevelLabel;
	[Export]
	public Label LevelPrice;
	[Export]
	public Sprite2D Enemy;
	
	public override void _Ready()
	{
		player = new Player();
		enemy = new Enemy();
		hp = enemy.Health;
		HPBar.MaxValue = enemy.Health;
		HPBar.Value = enemy.Health;
		HPBar.Step = player.Damage;
		UpdateScoreLabel();
		UpdateCoinLabel();
		UpdateHP();
		UpdateLevel();
		UpdateLevelPrice();
		LoadGame();
		
		enemyTextures = new Texture2D[]
		{
				GD.Load<Texture2D>("res://Assets/images.png"),
				GD.Load<Texture2D>("res://Assets/cokkie.jpg"),
				GD.Load<Texture2D>("res://Assets/catboy.png")
		};
		
		if (enemyTextures.Length == 0)
		{	
			GD.PrintErr("Nincsenek ellenség textúrák betöltve!");
   	 	}
		
		ChangeEnemySprite();
	}
	
	public void OnClickButton()
	{
		score++;
		hp -= player.Damage;
		UpdateHP();
		UpdateScoreLabel();
		if(hp <= 0)
		{
			coin += counter;
			score = 0;
			hp = rnd.Next(min,max);
			HPBar.MaxValue = hp;
			
			UpdateScoreLabel();
			UpdateCoinLabel();
			UpdateHP();
			
			currentEnemyIndex = rnd.Next(0,3);

			ChangeEnemySprite();
		}
	}
	
	public void OnLevelClickButton()
	{
		if (coin >= levelprice)
		{
			if(level == 5)
			{
				GD.Print("Elérted a maximális szintet te termesz");
			}
			else
			{
				min = min * 2;
				max = max * 2;
				level++;
				player.LevelUp();
				coin = coin-levelprice;
				levelprice = levelprice * 2;
				counter++;
				hp = rnd.Next(min, max);
				HPBar.MaxValue = hp;

				UpdateCoinLabel();
				UpdateLevel();
				UpdateLevelPrice();
				UpdateHP();
				
			}
			
		}
		
	}
	
	private void ChangeEnemySprite()
	{	
		Enemy.Texture  = enemyTextures[currentEnemyIndex];
	}
	
	private void UpdateScoreLabel()
	{
		if (ScoreLabel != null)
		{
			ScoreLabel.Text = "Pontok: " + score.ToString();
		}
		
	}
	
	private void UpdateLevel()
	{
		if (LevelLabel != null)
		{
			LevelLabel.Text = "Level: " + level.ToString();
		}
		
	}
	
	private void UpdateLevelPrice()
	{
		if (LevelPrice != null)
		{
			LevelPrice.Text = "Price: " + levelprice.ToString();
		}
		
	}
	
	private void UpdateCoinLabel()
	{
		if(CoinLabel != null)
		{
			CoinLabel.Text = "Coins: " + coin.ToString();
		}
	}
	
	private void UpdateHP()
	{
		if (HPBar == null)
		{
			GD.PrintErr("HIBA: A HPBar null az UpdateHP-ben!");
			return; 
   	 	}
		if(hp <= HPBar.MaxValue && hp >= 0)
		{
			if (HPLabel != null)
			{
				HPLabel.Text = "Health: " + hp.ToString();
			}
		}
		if (HPBar != null)
		{
			HPBar.Value = Math.Max(0, hp);
		}
	}
	
	private const string SAVE_PATH = "user://clicker_save.json";
	
	public void SaveGame()
	{
		Godot.Collections.Dictionary dataDict = new Godot.Collections.Dictionary 
		{
			{"Score", score},
			{"Coin", coin},
			{"Level", level},
			{"LevelPrice", levelprice},
			{"MinHP", min},
			{"MaxHP", max},
			{"Counter", counter},

		
			{"PlayerLevel", player.Level},
			{"PlayerDamage", player.Damage}
		};
		
		string jsonString = Json.Stringify(dataDict);
		
		using var file = Godot.FileAccess.Open(SAVE_PATH, Godot.FileAccess.ModeFlags.Write);
		if (file != null)
		{
			file.StoreString(jsonString);
	   	 	GD.Print("Játék elmentve! " + SAVE_PATH + " útvonalra");
   	 	}
		else
		{
			GD.PrintErr("Hiba a mentéskor: Nem lehet megnyitni a fájlt.");
		}
	}
	
	public void LoadGame()
	{
	// 1. Ellenőrizd, hogy a mentési fájl létezik-e
		if (!Godot.FileAccess.FileExists(SAVE_PATH))
		{
	   	 	GD.Print("Mentési fájl nem található, új játék indul.");
			return; // Ha nincs mentés, fejezd be a betöltést
		}	

	// 2. Olvasd be a JSON stringet a fájlból
		using var file = Godot.FileAccess.Open(SAVE_PATH, Godot.FileAccess.ModeFlags.Read);
		if (file == null)
		{
			GD.PrintErr("Hiba a betöltéskor: Nem lehet megnyitni a fájlt olvasásra.");
			return;
		}
		string jsonString = file.GetAsText();

	// 3. Konvertáld a JSON stringet Godot Variant/Dictionary-vé
		Variant dataVariant = Json.ParseString(jsonString);
	
		if (dataVariant.VariantType != Variant.Type.Dictionary)
		{
			GD.PrintErr("Hiba a betöltéskor: Sérült mentési fájl formátum.");
		// Mivel sérült, elindítjuk a játékot alapértelmezett értékekkel (nem térünk vissza, hanem hagyjuk a _Ready()-t futni)
			return;
   		}
	
		Godot.Collections.Dictionary dataDict = dataVariant.As<Godot.Collections.Dictionary>();

	// 4. Töltsd vissza az adatokat a Main osztály változóiba
	// FIGYELEM: Mivel a JSON-ban minden szám System.Int64 (long) típusú, 
	// explicit módon int-re kell konvertálni, hogy ne legyen hiba.
		score = (int)(long)dataDict["Score"];
		coin = (int)(long)dataDict["Coin"];
   		level = (int)(long)dataDict["Level"];
		levelprice = (int)(long)dataDict["LevelPrice"];
		min = (int)(long)dataDict["MinHP"];
		max = (int)(long)dataDict["MaxHP"];
		counter = (int)(long)dataDict["Counter"];

	// 5. Frissítsd a Player objektumot (és az Enemy-t, ha szükséges)
	// Megjegyzés: A Player objektumot a _Ready()-ben már létrehoztuk, most csak az adatait frissítjük!
		player.Level = (int)(long)dataDict["PlayerLevel"];
	
	// A Player.cs osztályban a PlayerDamage property-nek publikus settert kell adnod, 
	// hogy itt írni tudd: public int Damage { get; set; }
		player.Damage = (int)(long)dataDict["PlayerDamage"]; 

	// Frissítsd az Enemy HP-t a mentett max/min értékek alapján
		hp = rnd.Next(min, max); // Az új ellenségnek az új, mentett min/max alapján adunk HP-t

	// Frissítsd a UI elemeket
		UpdateScoreLabel();
		UpdateCoinLabel();
		UpdateLevel();
		UpdateLevelPrice();
	
	// Frissítsd a ProgressBar-t
		if (HPBar != null)
		{
			 HPBar.MaxValue = max; // Mivel a mentés után új ellenség jön, a MaxValue-t a mentett max-ra állítjuk
	   		 HPBar.Step = player.Damage;
   		}
		UpdateHP();

		GD.Print("Játék sikeresen betöltve a mentésből.");
	}
}
