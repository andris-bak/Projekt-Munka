using Godot;
using System;

public partial class Player 
{
	public int Level {get;  set;}
	public int Damage {get;  set;}
	public int PalaLevel {get; set;}
	public int PalaDamage {get;set;}
	
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
		PalaLevel = 1;
		PalaDamage = 1;
	}
	
	public void LevelUp()
	{
		Level++;
		Damage = Damage * 2;
	}
}
