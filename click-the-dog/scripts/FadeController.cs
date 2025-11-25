using Godot;
using System.Threading.Tasks;

public partial class FadeController : CanvasLayer
{
	private ColorRect _fadeRect;

	[Export] private float FadeTime = 0.5f;

	public override void _Ready()
	{
		_fadeRect = GetNode<ColorRect>("ColorRect");
		_fadeRect.Modulate = new Color(0f, 0f, 0f, 0f);
		_fadeRect.MouseFilter = Control.MouseFilterEnum.Ignore; // alapból ne blokkoljon
	}

	public async Task FadeToScene(string scenePath)
	{
		await FadeOut();
		GetTree().ChangeSceneToFile(scenePath);
		await FadeIn();
	}

	private async Task FadeOut()
	{
		_fadeRect.MouseFilter = Control.MouseFilterEnum.Stop; // most már blokkolhat
		var tween = CreateTween();
		tween.TweenProperty(_fadeRect, "modulate:a", 1f, FadeTime);
		await ToSignal(tween, Tween.SignalName.Finished);
	}

	private async Task FadeIn()
	{
		var tween = CreateTween();
		tween.TweenProperty(_fadeRect, "modulate:a", 0f, FadeTime);
		await ToSignal(tween, Tween.SignalName.Finished);
		_fadeRect.MouseFilter = Control.MouseFilterEnum.Ignore; // újra engedi a kattintást
	}
}
