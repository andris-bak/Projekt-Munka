using Godot;
using System;

public partial class Main : Node2D
{
	public GameManager GM;
	public Methods Method;
	private int CurrentScene;

	// Jelenethez kötött Node hivatkozások
	public AudioStreamPlayer bgmPlayer; 
	public AudioStreamPlayer hitSound; 
	public PackedScene[] enemyScenes; 
	public PackedScene[] shieldScenes; 
	public PackedScene[] characterScenes;
	public Node2D currentEnemy; 
	public Node2D currentShield; 
	public AnimatedSprite2D maincat; 
	public Player.DamageType tes = Player.DamageType.NONE;

	// Exportált Node hivatkozások...
	// [Export] public Label ScoreLabel;
	// [Export] public AnimatedSprite2D Enemy;
	// [Export] public Timer HealthRegen;
	[Export] public Label CoinLabel;
	[Export] public Label HPLabel;
	[Export] public Label bossLabel;
	[Export] public Label LevelLabel;
	[Export] public Label LevelPrice;
	[Export] public CanvasLayer Sign;
	[Export] public CanvasLayer MENU;
	[Export] public Sprite2D Shield;
	[Export] public Sprite2D Fire;
	[Export] public Sprite2D Earth; 
	[Export] public Sprite2D Air;
	[Export] public Sprite2D Water;
	[Export] public ProgressBar HPBar;
	[Export] public AnimatedSprite2D PlayerSprite;
	[Export] public AnimatedSprite2D PaladinSprite;
	[Export] public CanvasLayer optionsMenuLayer;
	[Export] public Sprite2D GoNext;
	[Export] public ColorRect PauseOverLay;
	
	
	public override void _Ready()
	{
		GM = GetNode<GameManager>("/root/GameManager");
		Method = GetNode<Methods>("/root/Methods");
		Method.BindUI(LevelLabel, LevelPrice, CoinLabel, HPLabel, HPBar, bossLabel,
		optionsMenuLayer, GoNext, Sign, MENU, PaladinSprite);  //enemyScenes, currentEnemy
		
		bgmPlayer = GetNode<AudioStreamPlayer>("BGMPlayer");
		hitSound = GetNode<AudioStreamPlayer>("hitfx");
		
		CurrentScene = 1;
		
		GM.HP = GM.EnemyData.Health;
		LoadGame();
		HPBar.MaxValue = GM.HP;
		HPBar.Value = GM.HP;
		HPBar.Step = GM.PlayerData.Damage; 
		UpdateCoinLabel();
		UpdateHP();
		UpdateLevel();
		UpdateLevelPrice();
		UpdateTimerLabel();

		if(bgmPlayer != null)
		{
			bgmPlayer.Play();
			bgmPlayer.Finished += () => Method.MusicFinished(bgmPlayer);
		}
		 
		shieldScenes = new PackedScene[]
		{
			GD.Load<PackedScene>("res://scenes/shield_bal.tscn"), 
			GD.Load<PackedScene>("res://scenes/shield_jobb.tscn"), 
		};
		
		characterScenes = new PackedScene[]
		{
			GD.Load<PackedScene>("res://scenes/paladin.tscn"),
		};
		
		if (PlayerSprite != null)
		{
 		 	PlayerSprite.Position = new Vector2(429, 173); 
		 	PlayerSprite.Play("Idle");
		}
		
		ChangeEnemyScene();
		ChangeShield();
	}
	
	public override void _Process(double delta)
	{
	//	if (GetTree().Paused)
	//	return;
		
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
		
		if(GM.Tick > 0 && GM.HP < GM.MaxHP && GM.HP > 0)
		{
			GM.regenTimer += GM.Tick * (float)delta;
			if(GM.regenTimer > 1.0f)
			{
				int regenMennyiseg = Mathf.FloorToInt(GM.regenTimer);
				
				// Kivonjuk a hozzáadott értéket az akkumulátorból
				GM.regenTimer -= regenMennyiseg;
				
				// Hozzáadjuk a HP-hoz
				GM.HP += regenMennyiseg;

				// FONTOS: Soha ne engedjük MaxHp fölé menni
				GM.HP = Mathf.Min(GM.HP, GM.MaxHP);

				UpdateHP();
			}
		}
	}
	
	public override void _Input(InputEvent @event)
	{
		// Cél: Bármely gombnyomás, amit a Godot-ban "attack_action" néven beállítunk (pl. Space, vagy egy controller gomb).
		if (@event.IsActionPressed("attack_action"))
		{
			// Meghívjuk a már létező OnClickButton metódust, ami elindítja a CombatHandler.HandleClick()-et.
			OnClickButton();
			// Jelzi a Godot-nak, hogy feldolgoztuk az eseményt.
			GetViewport().SetInputAsHandled(); 
			if(GM.PlayerData.PlayerDamageType == Player.DamageType.NONE)
			{
				GD.Print($"Enum működik");
			}
			if(GM.EnemyData.EnemyResistance == Enemy.DefenseType.FIRE)
			{
				GD.Print($"Enemy Enum működik");
			}
		}
		
		if (@event.IsActionPressed("openMenu"))
		{
			if (MENU != null)
			{
				MENU.Visible = !MENU.Visible;
				GD.Print($" menü váltva: {(MENU.Visible ? "MEGNYITVA" : "BEZÁRVA")}");
			}
			if(MENU.Visible == false)
			{
				optionsMenuLayer.Visible = false;
			}
			
			GetViewport().SetInputAsHandled(); 
		}
		
		if (@event.IsActionPressed("switchToFire"))
		{
			tes = Player.DamageType.FIRE;
			GD.Print($"Fire type  működik");
			GetViewport().SetInputAsHandled(); 
		}
		
		if (@event.IsActionPressed("switchToWater"))
		{
			tes = Player.DamageType.WATER;
			GD.Print($"Water type  működik");
			GetViewport().SetInputAsHandled(); 
		}
		
		if (@event.IsActionPressed("switchToAir"))
		{
			tes = Player.DamageType.AIR;
			GD.Print($"AIR type  működik");
			GetViewport().SetInputAsHandled(); 
		}
		
		if (@event.IsActionPressed("switchToEarth"))
		{
			tes = Player.DamageType.EARTH;
			GD.Print($"EARTH type  működik");
			GetViewport().SetInputAsHandled(); 
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
		Vector2 rightPlayerPos = new Vector2(429, 173); // Jobb oldal

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
		
		switch(GM.element)
			{
				case 0 :
					GM.EnemyData.EnemyResistance = Enemy.DefenseType.FIRE;
					break;
				case 1 :
					GM.EnemyData.EnemyResistance = Enemy.DefenseType.WATER;
					break;
				case 2 :
					GM.EnemyData.EnemyResistance = Enemy.DefenseType.AIR;
					break;
				case 3 :
					GM.EnemyData.EnemyResistance = Enemy.DefenseType.EARTH;
					break;
				default:
					break;
			}
			
		if(tes == Player.DamageType.FIRE && GM.EnemyData.EnemyResistance == Enemy.DefenseType.EARTH)
		{
			GD.Print($"Fire type  működik");
			actualDamage *= 5;
		}
		
		if(tes == Player.DamageType.WATER && GM.EnemyData.EnemyResistance == Enemy.DefenseType.FIRE)
		{
			GD.Print($"Water type  működik");
			actualDamage *= 5;
		}
		
		
		else
		{
			//actualDamage = GM.PlayerData.Damage;
		}
		// GM.Score, GM.HP és GM.PlayerData használata
		GM.Score++;
		GM.HP -= actualDamage;
		
		UpdateHP();
		// Animációk
		if(PlayerSprite != null)
		{
			if (PlayerSprite.Position == new Vector2(429, 173))
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
			if (GM.IsBossFight &&  GM.HP <= 0)
			{
				GM.IsBossFight = false;
				GM.BossDefeated = true;
				GD.Print("BOSS LEGYŐZVE!");
				UpdateTimerLabel();
				GoNextVisible();
			}
			
			// GM.Coin, GM.Counter, GM.Rnd, GM.MinHP, GM.MaxHP használata
			GM.Coin += 1000;//GM.Counter;
			GM.Score = 0;
			
			GM.HP = GM.Rnd.Next(GM.MinHP, GM.MaxHP);
			HPBar.MaxValue = GM.HP;
			//HPBar.Value = GM.HP;
			
			GM.element = GM.Rnd.Next(0,4);
			
			switch(GM.element)
			{
				case 0 :
					Water.Visible = false;
					Earth.Visible = false;
					Air.Visible = false;
					Fire.Visible = true;
					GM.EnemyData.EnemyResistance = Enemy.DefenseType.FIRE;
					break;
				case 1 :
					Earth.Visible = false;
					Air.Visible = false;
					Fire.Visible = false;
					Water.Visible = true;
					GM.EnemyData.EnemyResistance = Enemy.DefenseType.WATER;
					break;
				case 2 :
					Earth.Visible = false;
					Fire.Visible = false;
					Water.Visible = false;
					Air.Visible = true;
					GM.EnemyData.EnemyResistance = Enemy.DefenseType.AIR;
					break;
				case 3 :
					Fire.Visible = false;
					Water.Visible = false;
					Air.Visible = false;
					Earth.Visible = true;
					GM.EnemyData.EnemyResistance = Enemy.DefenseType.EARTH;
					break;
				default:
					break;
			}
			UpdateCoinLabel();
			UpdateHP();
			ChangeShield(); // Pajzs frissítése/cseréje
			ChangeEnemyScene(); 
		} // Bezárja az if(GM.HP <= 0) blokkot

	} 
	
	public async void OnQuitButtonPressed()
	{
		// SaveGame();
		FadeController fade = GetNode<FadeController>("/root/FadeController");
		await fade.FadeOut();
		if(CurrentScene == 2)
		{
			Sign.Visible = false;
		}
		if(MENU.Visible == true)
		{
			MENU.Visible = false;
		}
		Method.Quit();
	}
	
	public void UpdateLevel()
	{
		Method.LevelUp();
	}
	
	public void UpdateLevelPrice()
	{
		Method.LevelPrice();
	}
	
	public void UpdateCoinLabel()
	{
		Method.Coin();
	}
	
	public void UpdateHP()
	{
		Method.Health();
	}
	
	public void UpdateTimerLabel()
	{
		Method.BossTime();
	}
	
	public void OnOptionsMenuOpened()
	{
		Method.OptionOpen();
	}
	
	public void OnMenuClosed()
	{
		Method.OptionClose();
	}
	
	public void GoNextVisible()
	{
		Method.GetNext();
		CurrentScene++;
	}
	
	public void HeroHire()
	{
		Method.Hire();
		PackedScene newCharacterScene = characterScenes[0];
		Node2D newCharacter = newCharacterScene.Instantiate<Node2D>();
		AnimatedSprite2D sprite = newCharacter.GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
		AddChild(newCharacter);
		if(PlayerSprite.Position == new Vector2(429, 173))
		{
			sprite.Position = new Vector2(205, 173);
			sprite.Play("Idle2");
		}
		if(PlayerSprite.Position == new Vector2(205, 173))
		{
			sprite.Position = new Vector2(429, 173);
			sprite.Play("Idle");
		}
	}
	
	public async void OnChangeMapPressed()
	{
		FadeController fade = GetNode<FadeController>("/root/FadeController");
		await fade.FadeToScene("res://scenes/za_warudo_2.tscn");
		//GetTree().ChangeSceneToFile("res://scenes/za_warudo_2.tscn");
	}
	
	// --- METÓDUS: Boss győzelem (Idő lejárt) ---
	public void BossWins()
	{
		Method.bossWins();
		UpdateCoinLabel();
		UpdateHP();
		ChangeShield(); 
		ChangeEnemyScene(); 
	}
	
	public void OnLevelClickButton()
	{
		Method.LevelClick();
	}
	
	public void ChangeEnemyScene()
	{
		if (currentEnemy != null)
		{
			 currentEnemy.QueueFree();
			 currentEnemy = null;
		}

		// 2. SEGÉD: Kérünk egy új ellenséget a Methodstól
		Node2D newEnemy = Method.ChangeEnemy(); 
		
		// 3. TULAJDONOS: Befogadjuk az új gyereket
		currentEnemy = newEnemy;
		AddChild(currentEnemy); 
		
		// 4. TULAJDONOS: Elhelyezzük a jelenetben
		currentEnemy.Position = new Vector2(19, 11);
	}
	
	public void ChangeShield()
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
				// int currentDamage = GM.PlayerData.Damage; // Nem használt változó, eltávolítható
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
			
		}
	}
		
	public void OnPlayerAnimationFinished()
	{
		if (PlayerSprite != null)
		{
			if (PlayerSprite.Position == new Vector2(429, 173) && PlayerSprite.Animation != "Idle")
			{
				PlayerSprite.Play("Idle"); 
			}
			else if (PlayerSprite.Position == new Vector2(205, 173) && PlayerSprite.Animation != "Idle2")
			{
				PlayerSprite.Play("Idle2");
			}
		}
	}

	// --- Mentés / Betöltés Metódusok (GM. adatok kezelése) ---
	public const string SAVE_PATH = "user://clicker_save.json";
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
			PlayerSprite.Position = new Vector2(429,173);
			PlayerSprite.Play("Idle");
		}
	}
}
