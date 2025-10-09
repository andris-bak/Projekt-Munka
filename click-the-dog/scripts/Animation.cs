using Godot;
using System;

public partial class Animation : AnimatedSprite2D
{
	[Export]
	public AnimatedSprite2D EnemyAnimator;
	
	public override void _Ready()
	{
		if (EnemyAnimator != null)
		{
	   
			EnemyAnimator.Play("idle"); 
   		}
	
	}	
}
