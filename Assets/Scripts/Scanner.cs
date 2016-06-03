using UnityEngine;
using System.Collections;

public class Scanner : MonoBehaviour
{
    public float timeToScan;
    private float currentScanTime;
    private bool scanned;
    private bool scannerActive;

    void Start()
    {
        currentScanTime = timeToScan;

        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Subscribe(Scan);
        }
    }

    void OnDestroy()
    {
        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Unsubscribe(Scan);
        }
    }

    void Scan(InputState input)
    {
        if(input.ToggleScanner)
        {
            scannerActive = !scannerActive;
            if(GetComponent<HeldItemController>() != null)
            {
                GetComponent<HeldItemController>().enabled = !scannerActive;
            }
        }

        if (scannerActive && input.LockOn && enabled)
        {
            LockOn locked = GetComponent<LockOn>();
            if (locked != null && locked.target != null)
            {
                Data newData = locked.target.GetComponent<Data>();
                if (newData != null)
                {
                    if (input.UseItem)
                    {
                        if (currentScanTime > 0)
                        {
                            currentScanTime -= Time.deltaTime;
                        }
                        else
                        {
                            if (!scanned)
                            {
                                EventSystem.DisplayData(newData.textData, newData.imageData);
                                scanned = true;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            currentScanTime = timeToScan;
            EventSystem.DisplayData(null, null);
            scanned = false;
        }
    }
}
