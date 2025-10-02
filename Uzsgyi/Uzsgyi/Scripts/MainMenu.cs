using Godot;
using System;

public partial class MainMenu : Node2D
{
	private const string SAVE_PATH = "user://clicker_save.json"; 
	
	[Export]
	public TextureButton loadButton; 
	
	public override void _Ready()
	{
		 CheckSaveFile(); 
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

		var mainScene = GD.Load<PackedScene>("res://main.tscn");

		GetTree().ChangeSceneToPacked(mainScene);
	}
	
	public void OnStartButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://main.tscn");
	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
