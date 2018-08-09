﻿using UnityEngine;

public class Character : MonoBehaviour
{
    public float Speed = 6f;

    static public Pin CurrentPin { get; private set; }
    private Pin _targetPin;
    private MapManager _mapManager;

    private GameObject Level_Pin;

    void Awake()
    {
        Level_Pin = Pin.Level_Pin;
    }


    public void Initialize(MapManager mapManager, Pin startPin)
    {
        gameObject.GetComponent<Animator>().SetBool("IsMoving", false);
        _mapManager = mapManager;
        SetCurrentPin(startPin);
    }
    
    
    /// <summary>
    /// This runs once a frame
    /// </summary>
    private void Update()
    {
        if (_targetPin == null) return;

        // Get the characters current position and the targets position
        var currentPosition = transform.position;
        var targetPosition = _targetPin.transform.position;

        // If the character isn't that close to the target move closer
        if (Vector3.Distance(currentPosition, targetPosition) > .02f)
        {
            if (currentPosition.x - targetPosition.x > 0.2f)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (currentPosition.x - targetPosition.x < 0.2f)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            transform.position = Vector3.MoveTowards(
                currentPosition,
                targetPosition,
                Time.deltaTime * Speed
            );
        }
        else
        {
            if (_targetPin.IsAutomatic)
            {
                // Get a direction to keep moving in
                var pin = _targetPin.GetNextPin(CurrentPin);
                MoveToPin(pin);
            }
            else
            {
                SetCurrentPin(_targetPin);
            }
        }
    }

    
    /// <summary>
    /// Check the if the current pin has a reference to another in a direction
    /// If it does the move there
    /// </summary>
    /// <param name="direction"></param>
    public void TrySetDirection(Direction direction)
    {
        // Try get the next pin
        var pin = CurrentPin.GetPinInDirection(direction);
        
        // If there is a pin then move to it
        if (pin == null) return;
        MoveToPin(pin);
    }


    /// <summary>
    /// Move to a new pin
    /// </summary>
    /// <param name="pin"></param>
    private void MoveToPin(Pin pin)
    {
        _targetPin = pin;
        gameObject.GetComponent<Animator>().SetBool("IsMoving", true);
    }

    
    /// <summary>
    /// Set the current pin
    /// </summary>
    /// <param name="pin"></param>
    public void SetCurrentPin(Pin pin)
    {
        CurrentPin = pin;
        _targetPin = null;
        transform.position = pin.transform.position;
        gameObject.GetComponent<Animator>().SetBool("IsMoving", false);

        // Tell the map manager that
        // the current pin has changed
        _mapManager.UpdateGui();
    }
}