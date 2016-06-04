using UnityEngine;
using System.Collections;

public class MouseState : MonoBehaviour
{
    public CursorLockMode lockMode;

	void Update ()
    {
        Cursor.lockState = lockMode;
	}
}
