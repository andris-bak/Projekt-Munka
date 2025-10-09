using Godot;
using System;

public partial class Enemy : Node
{
	public int Level {get; private set;}
	public int Health {get; private set;}
	Random rnd = new Random();
	
	public Enemy()
	{
		Level = 1;
		
		Health = rnd.Next(10,21);
		
	}
	
}
