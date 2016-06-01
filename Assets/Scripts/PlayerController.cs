using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private InputState input;
    private PlayerActions actions;

    public InputBus targetObject;

	void Start()
    {
        input = new InputState();
        actions = PlayerActions.BindKeyboardAndJoystick();
	}
	
	void FixedUpdate()
    {
        UpdateInput();
        targetObject.Actions(input);
	}

    void UpdateInput()
    {
        input.Move = actions.Move;
        input.Look = actions.Look;

        input.Jump = actions.Jump.WasPressed;
        input.UseItem = actions.UseItem.IsPressed;
        input.LockOn = actions.LockOn.IsPressed;

        input.SwitchActionUp = actions.SwitchActionUp.WasPressed;
        input.SwitchActionDown = actions.SwitchActionDown.WasPressed;
    }
}
