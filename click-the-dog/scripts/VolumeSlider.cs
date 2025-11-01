using Godot;

// Ez a szkript közvetlenül az HSlider vagy VSlider node-on legyen.
public partial class VolumeSlider : HSlider
{

	[Export] public string BusName { get; set; } = "Master";
	private int _busIndex;

	public override void _Ready()
	{
		_busIndex = AudioServer.GetBusIndex(BusName);

		MinValue = -50.0;
		MaxValue = 0.0;
		Step = 0.5; 

		float currentDb = AudioServer.GetBusVolumeDb(_busIndex);
		Value = Mathf.Clamp(currentDb, MinValue, MaxValue);

		this.ValueChanged += OnValueChanged;
	}

	private void OnValueChanged(double value)
	{
		float dbValue = (float)value;

		if (dbValue < MinValue + 0.1)
		{
			AudioServer.SetBusVolumeDb(_busIndex, -80.0f);
		}
		else
		{
			AudioServer.SetBusVolumeDb(_busIndex, dbValue);
		}
	}
}
