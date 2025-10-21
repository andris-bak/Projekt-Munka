using Godot;
using System;

public partial class Main : Node2D
{
	private GameManager GM;
	
	// Jelenethez kötött Node hivatkozások
	private AudioStreamPlayer bgmPlayer; 
	private AudioStreamPlayer hitSound; 
	private PackedScene[] enemyScenes; 
	private PackedScene[] shieldScenes; 
	private Node2D currentEnemy; 
	private Node2D currentShield; // <- Kezeljük a pajzsot is Node-ként
	private AnimatedSprite2D maincat; 

	
	// Exportált Node hivatkozások...
	[Export] public Label ScoreLabel;
	[Export] public Label CoinLabel;
	[Export] public Label HPLabel;
	[Export] public Label bossLabel; // Ez a BOSS IDŐZÍTŐ címkéje
	[Export] public Label LevelLabel;
	[Export] public Label LevelPrice;
	[Export] public AnimatedSprite2D Enemy;
	[Export] public Sprite2D Shield;
	[Export] public ProgressBar HPBar;
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
		UpdateTimerLabel(); // Frissítés a kezdeti állapotra (rejtett)

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
		
		shieldScenes = new PackedScene[]
		{
			GD.Load<PackedScene>("res://scenes/shield_bal.tscn"), 
			GD.Load<PackedScene>("res://scenes/shield_jobb.tscn"), 
		};
		
		if (PlayerSprite != null)
		{
			PlayerSprite.Play("Idle"); 
		}
		
		ChangeEnemyScene();
		ChangeShield();
	}
	
	public override void _Process(double delta)
	{
		ChangePosition(); 
		
		// --- IDŐZÍTŐ LOGIKA ---
		if (GM.IsBossFight)
		{
			GM.BossTimeLeft -= delta;
			
			if (GM.BossTimeLeft <= 0)
			{
				GM.BossTimeLeft = 0;
				BossWins(); // FIX: Meghívjuk az idő lejárását kezelő metódust
			}
			UpdateTimerLabel();
		}
	}
	
	public void OnClickButton()
	{
		GM.ClickCounter++;
		if(GM.ClickCounter == 3)
		{
			GM.currentShieldIndex = GM.Rnd.Next(0, shieldScenes.Length);
			ChangeShield();
			GM.ClickCounter = 0;
		}
		int actualDamage = GM.PlayerData.Damage;
		Vector2 playerPos = PlayerSprite.Position;
		
		// Player pozíciók (ahol a PlayerSprite áll)
		Vector2 leftPlayerPos = new Vector2(205, 173); // Bal oldal
		Vector2 rightPlayerPos = new Vector2(435, 173); // Jobb oldal

		// Ellenőrizzük, hogy aktív-e a pajzs, és a játékos a megfelelő oldalon van-e
		if (currentShield != null)
		{
			// Pajzs a bal oldalon (Index 0)
			bool shieldIsLeft = GM.currentShieldIndex == 0;
			// Pajzs a jobb oldalon (Index 1)
			bool shieldIsRight = GM.currentShieldIndex == 1;

			// Játékos a bal oldalon ÉS a pajzs is ott van
			bool blockedByLeftShield = playerPos == leftPlayerPos && shieldIsLeft;
			
			// Játékos a jobb oldalon ÉS a pajzs is ott van
			bool blockedByRightShield = playerPos == rightPlayerPos && shieldIsRight;

			if (blockedByLeftShield || blockedByRightShield)
			{
				// A pajzs blokkolja a támadást
				actualDamage = 0;
				GD.Print("TÁMADÁS BLOKKOLVA! Pajzs oldala: " + (GM.currentShieldIndex == 0 ? "BAL" : "JOBB"));
			}
		}
		// GM.Score, GM.HP és GM.PlayerData használata
		GM.Score++;
		GM.HP -= actualDamage;
		
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
			GM.ClickCounter = 0;
			// FIX: Ha a boss-t vertük meg, kapcsoljuk ki az időzítőt
			if (GM.IsBossFight)
			{
				GM.IsBossFight = false;
				GD.Print("BOSS LEGYŐZVE!");
			}
			
			// GM.Coin, GM.Counter, GM.Rnd, GM.MinHP, GM.MaxHP használata
			GM.Coin += 1000;//GM.Counter;
			GM.Score = 0;
			
			GM.HP = GM.Rnd.Next(GM.MinHP, GM.MaxHP);
			HPBar.MaxValue = GM.HP;
			
			UpdateScoreLabel();
			UpdateCoinLabel();
			UpdateHP();
			ChangeShield(); // Pajzs frissítése/cseréje
			ChangeEnemyScene(); 
			
		}
	}
	
	// --- METÓDUS: Boss győzelem (Idő lejárt) ---
	private void BossWins()
	{
		GD.Print("AZ IDŐ LEJÁRT! A BOSS MEGNYERTE A HARCOT!");
		GM.IsBossFight = false;
		
		// Veszteség büntetés: elveszíti az aktuális aranyat, de megtartja a szintjét
		// Mivel GM.Counter nincs a LoadGame-ben, a 1000-et használom a példa kedvéért.
		GM.Coin = Math.Max(0, GM.Coin - 1000); 
		GM.Score = 0; // Pontok elvesztése
		
		// Új, normál ellenség betöltése
		GM.HP = GM.Rnd.Next(GM.MinHP, GM.MaxHP); 
		HPBar.MaxValue = GM.HP;
		
		UpdateScoreLabel();
		UpdateCoinLabel();
		UpdateHP();
		ChangeShield(); // Pajzs frissítése/cseréje
		ChangeEnemyScene(); 
	}
	
	public void OnLevelClickButton()
	{
		// GM.Coin, GM.LevelPrice és GM.Level használata
		if (GM.Coin >= GM.LevelPrice)
		{
			if(GM.Level == 12)
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
		// SaveGame();
		GetTree().Quit();
	}
	
	private void ChangeEnemyScene()
	{
		// GM.Rnd és GM.Level használata
		GM.currentEnemyIndex = GM.Rnd.Next(0, enemyScenes.Length); 
		int bossIndex = enemyScenes.Length - 1; 

		if (GM.currentEnemyIndex == bossIndex)
		{
			if (GM.Level < 10) 
			{
				GM.currentEnemyIndex = GM.Rnd.Next(0, bossIndex); 
				GD.Print("Boss kihagyva: Először el kell érned a(z) 5. szintet!");
				GM.IsBossFight = false; 
			}
			else
			{
				GD.Print("10. szint elérve! BOSS BETÖLTVE!");
				GM.HP = 170000;
				HPBar.MaxValue = GM.HP;
				
				// --- BOSS IDŐZÍTŐ INDÍTÁSA ---
				GM.IsBossFight = true;
				GM.BossTimeLeft = GameManager.BOSS_TIME_LIMIT; // Beállítjuk a max időt
				
				UpdateTimerLabel();
				UpdateHP();
			}
		} 
		
		else 
		{
			// Normál ellenség betöltése esetén mindig kapcsoljuk ki a boss módot
			GM.IsBossFight = false;
		}
		
		if (currentEnemy != null)
		{
		 	 currentEnemy.QueueFree();
			 currentEnemy = null;
		}

		PackedScene newEnemyScene = enemyScenes[GM.currentEnemyIndex]; 
		currentEnemy = newEnemyScene.Instantiate<Node2D>();
		

		AddChild(currentEnemy); 
		currentEnemy.Position = new Vector2(19, 11); 
		
		GD.Print($"Új ellenség betöltve: {currentEnemy.Name}");
		
		AnimatedSprite2D sprite = currentEnemy.GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D"); 
		if (sprite != null)
		{
			// A korábbi hibaforrás javítva: "idle" helyett "Idle" (Nagybetűs I)
			sprite.Play("Idle"); 
		}
		else
		{
			GD.PrintErr("HIBA: Nem található AnimatedSprite2D nevű gyermek Node az új ellenségen!");
		}
	}
	
	private void ChangeShield()
	{
		if(GM.Level >= 3)
		{
				// 1. ELŐZŐ PAJZS TÖRLÉSE (ha volt)
			if (currentShield != null)
			{
			 	 currentShield.QueueFree();
				 currentShield = null; // Biztos, ami biztos
			}
			
			// 2. RANDOM VÁLASZTÁS ÉS INSTANTIALIZÁLÁS
			// currentShieldIndex = 0 (shield_bal) vagy 1 (shield_jobb)
			GM.currentShieldIndex = GM.Rnd.Next(0, shieldScenes.Length); 
			
			PackedScene newShieldScene = shieldScenes[GM.currentShieldIndex]; 
			Node2D instantiatedShield = newShieldScene.Instantiate<Node2D>();
			
			// 3. POZÍCIÓ BEÁLLÍTÁSA
			if (instantiatedShield != null)
			{
				int currentDamage = GM.PlayerData.Damage;
				if (GM.currentShieldIndex == 0)
				{
					// Ez a BAL pajzs pozíciója
					instantiatedShield.Position = new Vector2(250, 160); 
					
				} 
				else // currentShieldIndex == 1
				{
					// Ez a JOBB pajzs pozíciója
					instantiatedShield.Position = new Vector2(0, 0); 
				}
				
				// 4. ÚJ PAJZS HOZZÁADÁSA
				currentShield = instantiatedShield; // Eltároljuk a hivatkozást
				AddChild(currentShield); 
				GD.Print($"Új pajzs betöltve: {currentShield.Name} a(z) {currentShield.Position} pozícióra.");
			}
			else
			{
				GD.PrintErr("HIBA: Nem sikerült a pajzs jelenetet létrehozni/példányosítani.");
			}
		}
		else
		{
			GD.Print("Mákos fasz");
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
		if(GM.Level == 12)
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
	
	private void UpdateTimerLabel()
		{
			if (bossLabel == null) return;
			
			if (GM.IsBossFight)
			{
				// Az időt két tizedesjegyre kerekítjük és megjelenítjük
				bossLabel.Text = $"IDŐ: {GM.BossTimeLeft:0.00} mp";
				bossLabel.Show();
				
				// Szín változtatása, ha már csak kevés idő van hátra
				if (GM.BossTimeLeft <= 5.0)
				{
					bossLabel.Modulate = new Color(1, 0.2f, 0.2f); // Pirosas, vészjelzés
				}
				else
				{
					bossLabel.Modulate = new Color(1, 1, 1); // Fehér
				}
			}
			else
			{
				bossLabel.Text = "";
				bossLabel.Hide();
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
