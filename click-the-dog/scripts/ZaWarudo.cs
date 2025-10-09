using Godot;
using System;

public partial class ZaWarudo : Node2D
{
	private AudioStreamPlayer bgmPlayer; 
	
	public override void _Ready()
	{
		bgmPlayer = GetNode<AudioStreamPlayer>("BGMPlayer");
		
		if(bgmPlayer != null)
		{
			bgmPlayer.Play();
		}
	}
}
