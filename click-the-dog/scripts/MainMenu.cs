using Godot;
using System;

public partial class MainMenu : Node2D
{
	private const string SAVE_PATH = "user://clicker_save.json"; 
	private AudioStreamPlayer zene; 
	
	[Export] public TextureButton loadButton; 
	
	public override void _Ready()
	{
		CheckSaveFile(); 
		zene = GetNode<AudioStreamPlayer>("Zene");
		if(zene != null)
		{
			zene.Play();
			zene.Finished += OnMusicFinished; 
		}
	}
	
	private void CheckSaveFile()
	{
		bool saveExists = Godot.FileAccess.FileExists(SAVE_PATH);
		
		if (loadButton != null)
		{
			loadButton.Disabled = !saveExists;
		}
	}
	
	private void OnLoadGameButtonPressed()
	{
		GD.Print("Mentett játék betöltése...");

		var mainScene = GD.Load<PackedScene>("res://scenes/za_warudo.tscn");

		GetTree().ChangeSceneToPacked(mainScene);
	}
	
	public async void OnStartButtonPressed()
	{
		FadeController fade = GetNode<FadeController>("/root/FadeController");
		await fade.FadeToScene("res://scenes/za_warudo.tscn");
		// GetTree().ChangeSceneToFile("res://scenes/za_warudo.tscn");
	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
	
	private void OnMusicFinished()
	{
		zene.Play();
	}
}
