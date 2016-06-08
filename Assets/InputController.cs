using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    protected InputState input;
    protected PlayerActions actions;

    public InputBus targetObject;

    protected void Initialize()
    {
        input = new InputState();
        actions = PlayerActions.BindKeyboardAndJoystick();
    }

    protected void UpdateBus()
    {
        if (targetObject)
        {
            targetObject.Actions(input);
        }
    }
}
