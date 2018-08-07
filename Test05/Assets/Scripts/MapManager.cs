using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
	public Character Character;
	public Pin StartPin;
	public Text SelectedLevelText;

    public static Pin CurrentPosition;
    public static int IsStartPositionChanged = 0;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	private void Start ()
	{
		// Pass a ref and default the player Starting Pin
        if (IsStartPositionChanged == 0)
            Character.Initialize(this, StartPin);
        else
            Character.Initialize(this, CurrentPosition);
	}

    /// <summary>
    /// Update the GUI text
    /// </summary>
    public void UpdateGui()
	{
		SelectedLevelText.text = string.Format("Current Level: {0}", Character.CurrentPin.SceneToLoad);
	}
}
