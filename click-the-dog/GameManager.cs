using Godot;
using System;

public partial class GameManager : Node
{
	// --- Globális Állapot (Public Properties) ---
	
	// A privát mezők helyett publikus tulajdonságok kellenek a Main.cs-ből való eléréshez
	public int Score { get; set; } = 0;
	public int Coin { get; set; } = 0;
	public int HP { get; set; } = 10;
	public int Level { get; set; } = 1;
	public int LevelPrice { get; set; } = 2;
	public int MinHP { get; set; } = 10;
	public int MaxHP { get; set; } = 20;
	public int Counter { get; set; } = 1;
	public int ClickCounter {get; set;} = 0;
	public int currentEnemyIndex {get; set;} = 0; 
	public int currentShieldIndex {get; set;} = 0;
	public int Tick {get; set;} = 3;
	public float regenTimer {get; set;} = 0.0f;
	public int element {get; set;} = -1;
	public bool BossDefeated {get; set;} = false;
	public int CurrentScene {get; set;} = 0;
	public bool Hired {get;set;} = false;	
	
	public Player PlayerData { get; set; }
	public Enemy EnemyData { get; set; }
	
	// A Random generátor is a GM-ben lakik, hogy egységes legyen a véletlenszerűség
	public Random Rnd { get; private set; } = new Random();
	
	// Ez a változó a Main Node-hoz tartozik, de ha itt akarod tárolni:
	// public int CurrentEnemyIndex { get; set; } = 0; 
	public bool IsBossFight { get; set; } = false;
	public double BossTimeLeft { get; set; } = 0.0;
	public const double BOSS_TIME_LIMIT = 40.0; // 15 másodperces időlimit

	public override void _Ready()
	{
		// Inicializálás:
		PlayerData = new Player();
		EnemyData = new Enemy();
		HP = EnemyData.Health;
	}
}
