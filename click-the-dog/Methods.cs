using Godot;
using System;

public partial class Methods : Node
{
	public GameManager GM;
	private Label _levelLabel; 
	private Label _levelPrice;
	private Label _coinLabel;
	
	public override void _Ready()
	{
		// Autoloadként ez létrejön még a fő jelenet előtt:
		GM = GetNode<GameManager>("/root/GameManager");
	}
	
	public void BindUI(Label LevelLabel, Label LevelPrice, Label CoinLabel)
	{
		_levelLabel = LevelLabel;
		_levelPrice = LevelPrice;
		_coinLabel = CoinLabel;
	}
	
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
	
	public void LevelUp(Label _levelLabel)
	{
		if (_levelLabel != null && GM != null)
			_levelLabel.Text = $"Level: {GM.Level}";
	}
	
	public void LevelPrice(Label _levelPrice)
	{
		if (_levelPrice != null)
		{
			_levelPrice.Text = $"Price: {GM.LevelPrice}";//"Price: " + GM.LevelPrice.ToString();
		}
		if(GM.Level == 12)
		{
			_levelPrice.Text = "Elérted a maximális szintet!";
		}
	}
	
	public void Coin(Label _coinLabel)
	{
		if(_coinLabel != null)
		{
			_coinLabel.Text = $"{GM.Coin}";//GM.Coin.ToString();
		}
	}
}
