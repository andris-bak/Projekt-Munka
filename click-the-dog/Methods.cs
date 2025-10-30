using Godot;
using System;

public partial class Methods : Node
{
	public GameManager GM;
	private Main _mainScene;
	public void HealthRegenHandler(Main mainScene)
	{
		_mainScene = mainScene;
		GD.Print("HealthRegenHandler sikeresen példányosítva.");
	}

	public void RegenerateHealth()
	{
		GameManager GM = _mainScene.GM;

		if (GM.HP < GM.MaxHP)
		{
			GM.HP = GM.HP + GM.Tick;
		   
			if (GM.HP > GM.MaxHP)
			{
				GM.HP = GM.MaxHP;
			}
			
			_mainScene.UpdateHP();
		}
	}
}
