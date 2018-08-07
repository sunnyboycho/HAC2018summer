using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Direction
{
    Next,
    Before
}

public class Pin : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.root);
    }

    public Character Character;

    [Header("Options")] //
	public bool IsAutomatic;
	public bool HideIcon;
	public string SceneToLoad;
	
	[Header("Pins")] //
	public Pin NextPin;
	public Pin BeforePin;

	private Dictionary<Direction, Pin> _pinDirections; 
	
	
	/// <summary>
	/// Use this for initialisation
	/// </summary>
	private void Start()
	{
		// Load the directions into a dictionary for easy access
		_pinDirections = new Dictionary<Direction, Pin>
		{
			{ Direction.Next, NextPin },
			{ Direction.Before, BeforePin }
		};
		
		// Hide the icon if needed
		if (HideIcon)
		{
			GetComponent<SpriteRenderer>().enabled = false;
		}
	}
	
	
	/// <summary>
	/// Get the pin in a selected direction
	/// Using a switch statement rather than linq so this can run in the editor
	/// </summary>
	/// <param name="direction"></param>
	/// <returns></returns>
	public Pin GetPinInDirection(Direction direction)
	{
		switch (direction)
		{
			case Direction.Next:
				return NextPin;
			case Direction.Before:
				return BeforePin;
			default:
				throw new ArgumentOutOfRangeException("direction", direction, null);
		}
	}

	
	/// <summary>
	/// This gets the first pin thats not the one passed 
	/// </summary>
	/// <param name="pin"></param>
	/// <returns></returns>
	public Pin GetNextPin(Pin pin)
	{
		return _pinDirections.FirstOrDefault(x => x.Value != null && x.Value != pin).Value;
	}



    /// <summary>
    /// Draw lines between connected pins
    /// </summary>
    private void OnDrawGizmos()
	{
		if(NextPin != null) DrawLine(NextPin);
		if(BeforePin != null) DrawLine(BeforePin);
	}


	/// <summary>
	/// Draw one pin line
	/// </summary>
	/// <param name="pin"></param>
	protected void DrawLine(Pin pin)
	{   
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, pin.transform.position);
	}

    private void OnMouseDown()
    {
        Debug.Log("clicked!");



        if (!Character.GetComponent<Animator>().GetBool("IsMoving"))
        {
            if (transform.position.x > Character.transform.position.x)
            {
                Character.TrySetDirection(Direction.Next);
            }

            else if (transform.position.x < Character.transform.position.x)
            {
                Character.TrySetDirection(Direction.Before);
            }

            else if (transform.position.y > Character.transform.position.y)
            {
                Character.TrySetDirection(Direction.Next);
            }

            else if (transform.position.y < Character.transform.position.y)
            {
                Character.TrySetDirection(Direction.Before);
            }
        }
        }

 }