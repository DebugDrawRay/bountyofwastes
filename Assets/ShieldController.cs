using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ShieldController : MonoBehaviour
{
    public GameObject shieldObject;
    public Vector3 restingPosition;
    public Vector3 raisedPosition;
    public float raiseSpeed;

    private bool raiseHeld;

    void Start()
    {
        shieldObject.transform.localPosition = restingPosition;
        shieldObject.SetActive(false);

        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Subscribe(RaiseShield);
        }
    }

    void OnDestroy()
    {
        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Unsubscribe(RaiseShield);
        }
    }

    void Update()
    {
        if(shieldObject.transform.localPosition == restingPosition)
        {
            shieldObject.SetActive(false);
        }
        else
        {
            shieldObject.SetActive(true);
        }
    }
    void RaiseShield(InputState input)
    {
        if (input.LockOn)
        {
            shieldObject.transform.DOLocalMove(restingPosition, raiseSpeed);
        }
        else
        {
            if (GetComponent<StatusController>())
            {
                if (GetComponent<StatusController>().currentShield > 0)
                {
                    if (input.RaiseShield)
                    {
                        if (!raiseHeld)
                        {
                            shieldObject.transform.DOLocalMove(raisedPosition, raiseSpeed);
                            raiseHeld = true;
                        }
                    }
                    else
                    {
                        if (raiseHeld)
                        {
                            shieldObject.transform.DOLocalMove(restingPosition, raiseSpeed);
                            raiseHeld = false;
                        }
                    }
                }
                else
                {
                    shieldObject.transform.DOLocalMove(restingPosition, raiseSpeed);
                    raiseHeld = false;
                }
            }
        }
    }
}
