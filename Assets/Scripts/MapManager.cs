using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
	public Character Character;
	public Pin StartPin;
	public Text SelectedLevelText;
	
	/// <summary>
	/// Use this for initialization
	/// </summary>
	private void Start ()
	{
		// Pass a ref and default the player Starting Pin
		Character.Start(this, StartPin);
	}

    /// <summary>
    /// Update the GUI text
    /// </summary>
    public void UpdateGui()
	{
		SelectedLevelText.text = string.Format("Current Level: {0}", Character.CurrentPin.SceneToLoad);
	}
}
