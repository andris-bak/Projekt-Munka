using Godot;
using System;

public partial class Methods : Node
{
	public GameManager GM;
	private Label _levelLabel; 
	private Label _levelPrice;
	private Label _coinLabel;
	private Label _hpLabel;
	private ProgressBar _hpBar;
	private Label _bossLabel;
	private CanvasLayer _optionsMenuLayer;
	private PackedScene[] _enemyScenes;
	private Node2D _currentEnemy;
	
	public override void _Ready()
	{
		// Autoloadként ez létrejön még a fő jelenet előtt:
		GM = GetNode<GameManager>("/root/GameManager");
		_enemyScenes = new PackedScene[]
		{
			 GD.Load<PackedScene>("res://scenes/slime.tscn"), 
			 GD.Load<PackedScene>("res://scenes/slime_2.tscn"), 
			 GD.Load<PackedScene>("res://scenes/slime_3.tscn"),
			 GD.Load<PackedScene>("res://scenes/clowndog.tscn"),
			
			//Boss:
			 GD.Load<PackedScene>("res://scenes/bonedog.tscn"), 
		};
	}
	
	public void BindUI(Label LevelLabel, Label LevelPrice, Label CoinLabel, 
	Label HPLabel, ProgressBar HPBar, Label bossLabel, CanvasLayer optionsMenuLayer)
	{
		_levelLabel = LevelLabel;
		_levelPrice = LevelPrice;
		_coinLabel = CoinLabel;
		_hpLabel = HPLabel;
		_hpBar = HPBar;
		_bossLabel = bossLabel;
		_optionsMenuLayer = optionsMenuLayer;

	}
	
	public void Quit()
	{
		GetTree().Quit();
	}
	
	public void MusicFinished(AudioStreamPlayer player)
	{
		if (player != null)
		{
			player.Play();
		}
	}
	
	public void LevelUp()
	{
		if (_levelLabel != null && GM != null)
			_levelLabel.Text = $"Level: {GM.Level}";
	}
	
	public void LevelPrice()
	{
		if (_levelPrice != null)
		{
			_levelPrice.Text = $"Price: {GM.LevelPrice}";//"Price: " + GM.LevelPrice.ToString();
		}
		if(GM.Level == 12)
		{
			_levelPrice.Text = "Elérted a maximális szintet!";
		}
	}
	
	public void Coin()
	{
		if(_coinLabel != null)
		{
			_coinLabel.Text = $"{GM.Coin}";//GM.Coin.ToString();
		}
	}
	
	public void Health()
	{
			if (_hpBar == null)
			{
				GD.PrintErr("HIBA: A HPBar null az UpdateHP-ben!");
				return; 
	    	}
			if(GM.HP <= _hpBar.MaxValue && GM.HP >= 0)
			{
				if (_hpLabel != null)
				{
					_hpLabel.Text = "Health: " + GM.HP.ToString();
				}
			}
			
			if (_hpBar != null)
			{
				_hpBar.Value = GM.HP; //Math.Max(0, GM.HP);
			}
	}
	
	public void BossTime()
	{
		if (_bossLabel == null) return;
		
		if (GM.IsBossFight)
		{
			// Az időt két tizedesjegyre kerekítjük és megjelenítjük
			_bossLabel.Text = $"IDŐ: {GM.BossTimeLeft:0.00} mp";
			_bossLabel.Show();
				
			if(GM.BossTimeLeft >= 5.0 && GM.BossTimeLeft <= 15.0)
			{
				_bossLabel.Modulate = new Color(1, 1, 1); // Fehér
			}
			// Szín változtatása, ha már csak kevés idő van hátra
			else if (GM.BossTimeLeft <= 5.0 && GM.BossTimeLeft >= 0.0)
			{
				_bossLabel.Modulate = new Color(1, 0.2f, 0.2f); // Piros vészjelzés
			}
		}
		else
		{
			_bossLabel.Text = "";
			_bossLabel.Hide();
		}
	}
	
	public void OptionOpen(CanvasLayer _optionsMenuLayer)
	{
		if (_optionsMenuLayer != null)
		{
			_optionsMenuLayer.Visible = true;
			GD.Print("Opciók menü megnyitva.");
		}
	}
	
	public void OptionClose(CanvasLayer _optionsMenuLayer)
	{
		if (_optionsMenuLayer != null)
		{
			_optionsMenuLayer.Visible = false;
			GD.Print("Opciók menü bezárva.");
		}
	}
	
	public void bossWins(ProgressBar _hpBar)
	{
		GD.Print("AZ IDŐ LEJÁRT! A BOSS MEGNYERTE A HARCOT!");
		GM.IsBossFight = false;
		
		// Veszteség büntetés: elveszíti az aktuális aranyat, de megtartja a szintjét
		// Mivel GM.Counter nincs a LoadGame-ben, a 1000-et használom a példa kedvéért.
		GM.Coin = Math.Max(0, GM.Coin - 1000); 
		GM.Score = 0; // Pontok elvesztése
		
		// Új, normál ellenség betöltése
		GM.HP = GM.Rnd.Next(GM.MinHP, GM.MaxHP); 
		_hpBar.MaxValue = GM.HP;
	}
	
	public void LevelClick(ProgressBar _hpBar)
	{
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
				_hpBar.MaxValue = GM.HP;

				Coin();//UpdateCoinLabel();
				LevelUp();//UpdateLevel();
				LevelPrice();//UpdateLevelPrice();
				Health();//UpdateHP();
			}
		}
	}
	
	public Node2D ChangeEnemy()
	{
			// Nincs paraméter, az osztályszintű _enemyScenes-t használjuk
		GM.currentEnemyIndex = GM.Rnd.Next(0, _enemyScenes.Length); 
		int bossIndex = _enemyScenes.Length - 1; 

		if (GM.currentEnemyIndex == bossIndex)
		{
			if (GM.Level < 10) 
			{
				GM.currentEnemyIndex = GM.Rnd.Next(0, bossIndex); 
				GD.Print("Boss kihagyva: Először el kell érned a(z) 10. szintet!");
				GM.IsBossFight = false; 
			}
			else
			{
				GD.Print("10. szint elérve! BOSS BETÖLTVE!");
				GM.HP = 170000;
				
				// Nincs paraméter, az osztályszintű _hpBar-t használjuk
				if (_hpBar != null)
					_hpBar.MaxValue = GM.HP;
				
				GM.IsBossFight = true;
				GM.BossTimeLeft = GameManager.BOSS_TIME_LIMIT; 
				
				// A "segéd" metódusok hívása (paraméter nélkül)
				BossTime();
				Health();
			}
		} 
		else
		{
			GM.IsBossFight = false;
		}
		
		// FIGYELEM: Nincs QueueFree()! Azt a Main végzi.

		PackedScene newEnemyScene = _enemyScenes[GM.currentEnemyIndex]; 
		Node2D newEnemy = newEnemyScene.Instantiate<Node2D>();
		
		// FIGYELEM: Nincs AddChild()! Azt a Main végzi.
		// FIGYELEM: Nincs Position beállítás! Azt a Main végzi.
		
		GD.Print($"Új ellenség létrehozva: {newEnemy.Name}");
		
		AnimatedSprite2D sprite = newEnemy.GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D"); 
		if (sprite != null)
		{
			sprite.Play("Idle"); 
		}
		else
		{
			GD.PrintErr("HIBA: Nem található AnimatedSprite2D nevű gyermek Node az új ellenségen!");
		}
		
		// Visszaadjuk az új ellenséget a hívónak (Main-nek)
		return newEnemy;
	}
}
