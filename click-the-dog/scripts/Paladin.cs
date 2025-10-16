using Godot;
using System;

public partial class Paladin : Sprite2D
{
	[Export] public Label DialogueLabel;
	public AudioStreamPlayer hang;
	
	public override void _Ready()
	{
		if (DialogueLabel != null)
		{
			DialogueLabel.Hide();
		}
		hang = GetNode<AudioStreamPlayer>("Hang");
	}

	public void OnPaladinPressed()
	{
		StartDialogue("MEOW!"); 
		hang.Play();
	}

	public void StartDialogue(string text)
	{
		if (DialogueLabel != null)
		{
			DialogueLabel.Text = text; 
			DialogueLabel.Show();
			GetTree().CreateTimer(2.0f).Timeout += () => DialogueLabel.Hide();
		}
	}
}
