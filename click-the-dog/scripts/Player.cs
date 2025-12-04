using Godot;
using System;

public partial class Player 
{
	public int Level {get;  set;}
	public int Damage {get;  set;}
	
	public enum DamageType
	{
		NONE,
		FIRE,
		WATER,
		AIR,
		EARTH
	}
	
	public DamageType PlayerDamageType {get; set;}
	
	public Player()
	{
		Level = 1;
		Damage = 1;
		PlayerDamageType = DamageType.NONE;
	}
	
	public void LevelUp()
	{
		Level++;
		Damage = Damage * 2;
	}
}
