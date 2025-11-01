using Godot;

public partial class Cursor : TextureButton
{
	// --- HOVER KURZOR (amit mutasson, ha felette van) ---
	// Az [ExportGroup] szebben rendezi az Inspectorban
	[ExportGroup("Hover Kurzor")]
	[Export]
	private Texture2D _hoverCursorTexture;
	
	[Export]
	private Vector2 _hoverHotspot = Vector2.Zero;


	// --- ALAPÉRTELMEZETT KURZOR (amire váltson vissza) ---
	// Ide húzd be az alapértelmezett, "normál" játék-kurzorodat
	[ExportGroup("Alapértelmezett Kurzor")]
	[Export]
	private Texture2D _defaultCursorTexture;
	
	[Export]
	private Vector2 _defaultHotspot = Vector2.Zero;


	public override void _Ready()
	{
		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
	}

	private void OnMouseEntered()
	{
		if (_hoverCursorTexture != null)
		{
			// Beállítjuk a "hover" (pl. kard) kurzort
			Input.SetCustomMouseCursor(_hoverCursorTexture, Input.CursorShape.Arrow, _hoverHotspot);
		}
	}

	private void OnMouseExited()
	{
		// ▼▼▼ EZ A LÉNYEGI VÁLTOZÁS ▼▼▼
		
		// 'null' helyett a te alapértelmezett játék-kurzorodat állítjuk be.
		if (_defaultCursorTexture != null)
		{
			Input.SetCustomMouseCursor(_defaultCursorTexture, Input.CursorShape.Arrow, _defaultHotspot);
		}
		else
		{
			// Vészmegoldás: ha nem adtál meg alapértelmezettet,
			// akkor töröljük (ez a régi, OS nyílra visszaállító viselkedés)
			Input.SetCustomMouseCursor(null);
		}
	}
}
