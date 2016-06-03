using UnityEngine;
using System.Collections;

public class HeldItemController : MonoBehaviour
{
    public GameObject[] availableItems;
    public int selectedItem;
    //For parenting
    public Transform head;
    private GameObject currentlyHeldItem;
    private Useable currentlyHeldActions;

    private bool actionHeld;

    void Start()
    {
        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Subscribe(UseItem);
            GetComponent<InputBus>().Subscribe(SwitchHeld);
        }
    }

    void OnEnable()
    {
        HoldItem(selectedItem);
    }

    void OnDisable()
    {
        HoldItem(-1);
    }

    void OnDestroy()
    {
        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Unsubscribe(UseItem);
            GetComponent<InputBus>().Unsubscribe(SwitchHeld);
        }
    }

    void HoldItem(int newItem)
    {
        Destroy(currentlyHeldItem);

        if (newItem < availableItems.Length && newItem >= 0)
        {
            selectedItem = newItem;

            currentlyHeldItem = Instantiate(availableItems[selectedItem]);
            currentlyHeldItem.transform.position = head.position;
            currentlyHeldItem.transform.SetParent(head);
            currentlyHeldItem.transform.localRotation = Quaternion.identity;
            currentlyHeldActions = currentlyHeldItem.GetComponent<Useable>();
        }          
    }

    void UseItem(InputState input)
    {
        if (enabled)
        {
            if (input.UseItem)
            {
                if (currentlyHeldActions != null && !actionHeld)
                {
                    actionHeld = true;
                    currentlyHeldActions.Action.Invoke();
                }
            }
            else
            {
                actionHeld = false;
            }
        }
    }

    void SwitchHeld(InputState input)
    {
        if (enabled)
        {
            if (input.SwitchActionUp)
            {
                selectedItem += 1;
                if (selectedItem >= availableItems.Length)
                {
                    selectedItem = 0;
                }
            }
            if (input.SwitchActionDown)
            {
                selectedItem -= 1;
                if (selectedItem < 0)
                {
                    selectedItem = availableItems.Length - 1;
                }
            }
        }
    }
}
