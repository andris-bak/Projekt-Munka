using Godot;
using System;

public partial class Methods : Node
{
	public GameManager GM;
	
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
	
	public void LevelUp(Label lvl)
	{
		if (lvl != null)
		{
			lvl.Text = "Level: " + GM.Level.ToString();
		}
	}
}
