using UnityEngine;
using System.Collections;

public class RightArm : MonoBehaviour
{
    public GameObject[] availableItems;
    public int selectedItem;
    //For parenting
    public Transform head;

    [Header("Scanner Reference")]
    public Scanner scanner;

    private GameObject currentlyHeldItem;
    private Useable currentlyHeldActions;

    void Awake()
    {
        HoldItem(selectedItem);
        scanner.enabled = false;
    }

    void Start()
    {
        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Subscribe(UseItem);
            GetComponent<InputBus>().Subscribe(SwitchHeld);

        }
    }
    void OnDestroy()
    {
        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Unsubscribe(UseItem);
            GetComponent<InputBus>().Unsubscribe(SwitchHeld);
        }
    }

    void ToggleScanner(bool enable)
    {
        if (scanner != null)
        {
            Destroy(currentlyHeldItem);
            scanner.enabled = enable;
        }
    }

    void HoldItem(int newItem)
    {
        if (newItem < availableItems.Length)
        {
            selectedItem = newItem;
            Destroy(currentlyHeldItem);

            currentlyHeldItem = Instantiate(availableItems[selectedItem]);
            currentlyHeldItem.transform.position = transform.position;
            currentlyHeldItem.transform.SetParent(head);
            currentlyHeldItem.transform.localRotation = Quaternion.identity;
            currentlyHeldActions = currentlyHeldItem.GetComponent<Useable>();
        }          
    }

    void UseItem(InputState input)
    {
        if(currentlyHeldActions != null && input.UseItem)
        {
            currentlyHeldActions.Action.Invoke();
        }
    }

    void SwitchHeld(InputState input)
    {
        if(input.SwitchActionUp)
        {
            selectedItem += 1;
            if(selectedItem >= availableItems.Length)
            {
                if (scanner != null)
                {
                    selectedItem = -1;
                }
                else
                {
                    selectedItem = 0;
                }
            }
        }
        if (input.SwitchActionDown)
        {
            selectedItem -= 1;
            if(selectedItem < -1)
            {
                selectedItem = availableItems.Length - 1;
            }
        }

        if(selectedItem == -1)
        {
            ToggleScanner(true);
        }
        else
        {
            ToggleScanner(false);
            HoldItem(selectedItem);
        }
    }
}
