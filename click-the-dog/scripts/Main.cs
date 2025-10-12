using Godot;
using System;

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
	private AudioStreamPlayer bgmPlayer; 
	private AudioStreamPlayer hitSound; 
	private PackedScene[] enemyScenes; 
	private Node2D currentEnemy; 
	private AnimatedSprite2D maincat;
	private int currentEnemyIndex = 0;
	
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
	[Export]
	public AnimatedSprite2D PlayerSprite;
	
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
	
	public void OnClickButton()
	{
		score++;
		hp -= player.Damage;
		UpdateHP();
		UpdateScoreLabel();

		if (PlayerSprite != null)
		{
			PlayerSprite.Play("attack"); 
		}
		hitSound.Play();
		
		if(hp <= 0)
		{
			coin += counter;
			score = 0;
			hp = rnd.Next(min,max);
			HPBar.MaxValue = hp;
			
			UpdateScoreLabel();
			UpdateCoinLabel();
			UpdateHP();
			ChangeEnemyScene(); 
			
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
	
	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
	
	private void ChangeEnemyScene()
	{
		currentEnemyIndex = rnd.Next(0,4);
		int bossIndex = enemyScenes.Length - 1; 

		if (currentEnemyIndex == bossIndex)
		{
			if (level < 5) 
			{
				currentEnemyIndex = 0; 
				GD.Print("Boss kihagyva: Először el kell érned a(z) 5. szintet!");
			}
			else
			{
				GD.Print("5. szint elérve! BOSS BETÖLTVE!");
				hp = 2000;
				HPBar.MaxValue = hp;
				UpdateHP();
			}
		}
		
		// C. Előző Ellenség Törlése
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
		if (PlayerSprite != null && PlayerSprite.Animation != "Idle")
		{
			PlayerSprite.Play("Idle"); 
		}
	}
	
	private void OnMusicFinished()
	{
		bgmPlayer.Play();
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
		if(level == 5)
		{
			LevelPrice.Text = "Elérted a maximális szintet! ";
		}
		
	}
	
	private void UpdateCoinLabel()
	{
		if(CoinLabel != null)
		{
			CoinLabel.Text = coin.ToString();
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

		score = (int)(long)dataDict["Score"];
		coin = (int)(long)dataDict["Coin"];
   		level = (int)(long)dataDict["Level"];
		levelprice = (int)(long)dataDict["LevelPrice"];
		min = (int)(long)dataDict["MinHP"];
		max = (int)(long)dataDict["MaxHP"];
		counter = (int)(long)dataDict["Counter"];

		player.Level = (int)(long)dataDict["PlayerLevel"];
	

		player.Damage = (int)(long)dataDict["PlayerDamage"]; 

		hp = rnd.Next(min, max); 

		UpdateScoreLabel();
		UpdateCoinLabel();
		UpdateLevel();
		UpdateLevelPrice();
	
		if (HPBar != null)
		{
			 HPBar.MaxValue = max; 
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
}
