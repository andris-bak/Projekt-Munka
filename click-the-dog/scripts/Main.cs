using Godot;
using System;

public partial class Main : Node2D
{
	private GameManager GM;

	private AudioStreamPlayer bgmPlayer; 
	private AudioStreamPlayer hitSound; 
	private PackedScene[] enemyScenes; 
	private Node2D currentEnemy; 
	private AnimatedSprite2D maincat; 
	private int currentEnemyIndex = 0; 
	
	[Export] public Label ScoreLabel;
	[Export] public Label CoinLabel;
	[Export] public Label HPLabel;
	[Export] public ProgressBar HPBar;
	[Export] public Label LevelLabel;
	[Export] public Label LevelPrice;
	[Export] public Sprite2D Enemy;
	[Export] public AnimatedSprite2D PlayerSprite;
	
	public override void _Ready()
	{
		GM = GetNode<GameManager>("/root/GameManager");
		GM.HP = GM.EnemyData.Health;
		LoadGame();

		HPBar.MaxValue = GM.MaxHP;
		HPBar.Value = GM.HP;
		HPBar.Step = GM.PlayerData.Damage; 
		
		UpdateScoreLabel();
		UpdateCoinLabel();
		UpdateHP();
		UpdateLevel();
		UpdateLevelPrice();

		bgmPlayer = GetNode<AudioStreamPlayer>("BGMPlayer");
		hitSound = GetNode<AudioStreamPlayer>("hitfx");
		
		if(bgmPlayer != null)
		{
			bgmPlayer.Play();
			bgmPlayer.Finished += OnMusicFinished; 
		}
		 
		
		enemyScenes = new PackedScene[]
		{
			 GD.Load<PackedScene>("res://scenes/slime.tscn"), 
			 GD.Load<PackedScene>("res://scenes/slime_2.tscn"), 
			 GD.Load<PackedScene>("res://scenes/slime_3.tscn"),
			
			//Boss:
			 GD.Load<PackedScene>("res://scenes/bonedog.tscn"), 

		};
		
		ChangeEnemyScene();
	}
	
	public override void _Process(double delta)
	{
		ChangePosition(); 
	}
	
	public void OnClickButton()
	{
		// GM.Score, GM.HP és GM.PlayerData használata
		GM.Score++;
		GM.HP -= GM.PlayerData.Damage;
		
		UpdateHP();
		UpdateScoreLabel();

		// Animációk
		if(PlayerSprite != null)
		{
			if (PlayerSprite.Position == new Vector2(435, 173))
			{
				PlayerSprite.Play("attack"); 
			}
			else if (PlayerSprite.Position == new Vector2(205, 173))
			{
				PlayerSprite.Play("attack2");
			}
		}
		
		hitSound.Play();
		
		if(GM.HP <= 0)
		{
			// GM.Coin, GM.Counter, GM.Rnd, GM.MinHP, GM.MaxHP használata
			GM.Coin += GM.Counter;
			GM.Score = 0;
			
			GM.HP = GM.Rnd.Next(GM.MinHP, GM.MaxHP);
			HPBar.MaxValue = GM.HP;
			
			UpdateScoreLabel();
			UpdateCoinLabel();
			UpdateHP();
			ChangeEnemyScene(); 
			
		}
	}
	
	public void OnLevelClickButton()
	{
		// GM.Coin, GM.LevelPrice és GM.Level használata
		if (GM.Coin >= GM.LevelPrice)
		{
			if(GM.Level == 5)
			{
				GD.Print("Elérted a maximális szintet te termesz");
			}
			else
			{
				// GM.MinHP és GM.MaxHP növelése
				GM.MinHP = GM.MinHP * 2;
				GM.MaxHP = GM.MaxHP * 2;
				GM.Level++;
				GM.PlayerData.LevelUp(); 
				GM.Coin = GM.Coin - GM.LevelPrice;
				GM.LevelPrice = GM.LevelPrice * 2;
				GM.Counter++;
				
				// GM.Rnd használata
				GM.HP = GM.Rnd.Next(GM.MinHP, GM.MaxHP);
				HPBar.MaxValue = GM.HP;

				UpdateCoinLabel();
				UpdateLevel();
				UpdateLevelPrice();
				UpdateHP();
			}
		}
	}
	
	public void OnQuitButtonPressed()
	{
		SaveGame();
		GetTree().Quit();
	}
	
	private void ChangeEnemyScene()
	{
		// GM.Rnd és GM.Level használata
		currentEnemyIndex = GM.Rnd.Next(0, enemyScenes.Length); 
		int bossIndex = enemyScenes.Length - 1; 

		if (currentEnemyIndex == bossIndex)
		{
			if (GM.Level < 5) 
			{
				currentEnemyIndex = GM.Rnd.Next(0, bossIndex); 
				GD.Print("Boss kihagyva: Először el kell érned a(z) 5. szintet!");
			}
			else
			{
				GD.Print("5. szint elérve! BOSS BETÖLTVE!");
				GM.HP = 2000;
				HPBar.MaxValue = GM.HP;
				UpdateHP();
			}
		}
		
		if (currentEnemy != null)
		{
		 	 currentEnemy.QueueFree();
			 currentEnemy = null;
		}

		PackedScene newEnemyScene = enemyScenes[currentEnemyIndex]; 
		currentEnemy = newEnemyScene.Instantiate<Node2D>();
		

		AddChild(currentEnemy); 
		currentEnemy.Position = new Vector2(19, 11); 
		
		GD.Print($"Új ellenség betöltve: {currentEnemy.Name}");
		
		AnimatedSprite2D sprite = currentEnemy.GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D"); 
		if (sprite != null)
		{
			sprite.Play("idle"); 
		}
		else
		{
			GD.PrintErr("HIBA: Nem található AnimatedSprite2D nevű gyermek Node az új ellenségen!");
		}
	}
		
		
	private void OnPlayerAnimationFinished()
	{
		if (PlayerSprite != null)
		{
			if (PlayerSprite.Position == new Vector2(435, 173) && PlayerSprite.Animation != "Idle")
			{
				PlayerSprite.Play("Idle"); 
			}
			else if (PlayerSprite.Position == new Vector2(205, 173) && PlayerSprite.Animation != "Idle2")
			{
				PlayerSprite.Play("Idle2");
			}
		}
	}
	
	private void OnMusicFinished()
	{
		bgmPlayer.Play();
	}
		
	// --- UI Frissítő Metódusok (GM. adatok alapján) ---
		
	private void UpdateScoreLabel()
	{
		if (ScoreLabel != null)
		{
			ScoreLabel.Text = "Pontok: " + GM.Score.ToString();
		}
		
	}
	
	private void UpdateLevel()
	{
		if (LevelLabel != null)
		{
			LevelLabel.Text = "Level: " + GM.Level.ToString();
		}
		
	}
	
	private void UpdateLevelPrice()
	{
		if (LevelPrice != null)
		{
			LevelPrice.Text = "Price: " + GM.LevelPrice.ToString();
		}
		if(GM.Level == 5)
		{
			LevelPrice.Text = "Elérted a maximális szintet! ";
		}
		
	}
	
	private void UpdateCoinLabel()
	{
		if(CoinLabel != null)
		{
			CoinLabel.Text = GM.Coin.ToString();
		}
	}
	
	private void UpdateHP()
	{
		if (HPBar == null)
		{
			GD.PrintErr("HIBA: A HPBar null az UpdateHP-ben!");
			return; 
   	 	}
		if(GM.HP <= HPBar.MaxValue && GM.HP >= 0)
		{
			if (HPLabel != null)
			{
				HPLabel.Text = "Health: " + GM.HP.ToString();
			}
		}
		if (HPBar != null)
		{
			HPBar.Value = Math.Max(0, GM.HP);
		}
	}
	
	// --- Mentés / Betöltés Metódusok (GM. adatok kezelése) ---

	private const string SAVE_PATH = "user://clicker_save.json";
	
	public void SaveGame()
	{
		// GM adatok mentése
		Godot.Collections.Dictionary dataDict = new Godot.Collections.Dictionary 
		{
			{"Score", GM.Score},
			{"Coin", GM.Coin},
			{"Level", GM.Level},
			{"LevelPrice", GM.LevelPrice},
			{"MinHP", GM.MinHP},
			{"MaxHP", GM.MaxHP},
			{"Counter", GM.Counter},

		
			{"PlayerLevel", GM.PlayerData.Level},
			{"PlayerDamage", GM.PlayerData.Damage}
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
		if (!Godot.FileAccess.FileExists(SAVE_PATH))
		{
	   	 	GD.Print("Mentési fájl nem található, új játék indul.");
			return; 
		}	

		using var file = Godot.FileAccess.Open(SAVE_PATH, Godot.FileAccess.ModeFlags.Read);
		if (file == null)
		{
			GD.PrintErr("Hiba a betöltéskor: Nem lehet megnyitni a fájlt olvasásra.");
			return;
		}
		string jsonString = file.GetAsText();

		Variant dataVariant = Json.ParseString(jsonString);
	
		if (dataVariant.VariantType != Variant.Type.Dictionary)
		{
			GD.PrintErr("Hiba a betöltéskor: Sérült mentési fájl formátum.");
			return;
   		}
	
		Godot.Collections.Dictionary dataDict = dataVariant.As<Godot.Collections.Dictionary>();

		// GM adatok betöltése
		GM.Score = (int)(long)dataDict["Score"];
		GM.Coin = (int)(long)dataDict["Coin"];
   		GM.Level = (int)(long)dataDict["Level"];
		GM.LevelPrice = (int)(long)dataDict["LevelPrice"];
		GM.MinHP = (int)(long)dataDict["MinHP"];
		GM.MaxHP = (int)(long)dataDict["MaxHP"];
		GM.Counter = (int)(long)dataDict["Counter"];

		// GM.PlayerData adatok betöltése
		GM.PlayerData.Level = (int)(long)dataDict["PlayerLevel"];
		GM.PlayerData.Damage = (int)(long)dataDict["PlayerDamage"]; 

		// GM.Rnd és GM.Min/MaxHP használata
		GM.HP = GM.Rnd.Next(GM.MinHP, GM.MaxHP); 

		UpdateScoreLabel();
		UpdateCoinLabel();
		UpdateLevel();
		UpdateLevelPrice();
	
		if (HPBar != null)
		{
			 // Itt a MaxHP-ra frissítjük a MaxValue-t, ahogy a _Ready-ben is volt
			 HPBar.MaxValue = GM.MaxHP; 
   		}
		UpdateHP();

		GD.Print("Játék sikeresen betöltve a mentésből.");
	}
	
	public void DeleteSave()
	{
		if (Godot.FileAccess.FileExists(SAVE_PATH))
		{
			var result = Godot.DirAccess.RemoveAbsolute(SAVE_PATH);
			
			if (result == Godot.Error.Ok)
			{
				GD.Print("Mentési fájl sikeresen törölve.");
			}
			else
			{
				GD.PrintErr($"Hiba a mentési fájl törlésekor: {result}");
			}
		}
		else
		{
			GD.Print("Nincs mentési fájl a törléshez.");
		}
	}
	
	
	
	public void ChangePosition()
	{
		if (PlayerSprite == null) return;
		
		if (Input.IsActionJustPressed("switchLeft"))
		{
			PlayerSprite.Position = new Vector2(205,173);
			PlayerSprite.Play("Idle2");
		}
		if (Input.IsActionJustPressed("switchRight"))
		{
			PlayerSprite.Position = new Vector2(435,173);
			PlayerSprite.Play("Idle");
		}
	}
}
