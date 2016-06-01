using UnityEngine;
using System.Collections;

public class Scanner : MonoBehaviour
{
    public float timeToScan;
    private float currentScanTime;
    private bool scanned;

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
        if (input.LockOn && enabled)
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
                                EventSystem.DisplayData(newData.textData);
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
            EventSystem.DisplayData(null);
            scanned = false;
        }
    }
}
