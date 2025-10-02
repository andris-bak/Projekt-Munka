using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
	public int Score { get; set; }
	public int Coin { get; set; }
	public int Level { get; set; }
	public int LevelPrice { get; set; }
	public int MinHP { get; set; }
	public int MaxHP { get; set; }
	public int Counter { get; set; }

	public int PlayerLevel { get; set; } 
	public int PlayerDamage { get; set; }
}
