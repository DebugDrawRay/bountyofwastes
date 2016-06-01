using UnityEngine;
using System.Collections;

public class InputBus : MonoBehaviour
{
    public delegate void InputAction(InputState input);
    public InputAction Actions;
    
    public void Subscribe(InputAction targetAction)
    {
        Actions += targetAction;
    } 
    public void Unsubscribe(InputAction targetAction)
    {
        Actions -= targetAction;
    }
}
