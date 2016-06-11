using UnityEngine;
using System.Collections;

public class PlayerController : InputController
{
    public static PlayerController instance;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

        Initialize();
    }

	void Update()
    {
        UpdateInput();
        UpdateBus();
	}

    void UpdateInput()
    {
        input.Move = actions.Move;
        input.Look = actions.Look;

        input.Jump = actions.Jump.WasPressed;
        input.UseItem = actions.UseItem.IsPressed;
        input.RaiseShield = actions.RaiseShield.IsPressed;
        input.LockOn = actions.LockOn.IsPressed;
        input.ToggleScanner = actions.ToggleScanner.WasPressed;

        input.SwitchActionUp = actions.SwitchActionUp.WasPressed;
        input.SwitchActionDown = actions.SwitchActionDown.WasPressed;
    }
}
