using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ShieldController : MonoBehaviour
{
    public GameObject shieldObject;
    private GameObject currentShield;
    public Vector3 restingPosition;
    public Vector3 raisedPosition;
    public float raiseSpeed;
    public Transform head;

    private bool raiseHeld;
    void Start()
    {
        currentShield = Instantiate(shieldObject, transform.position, Quaternion.identity) as GameObject;
        currentShield.transform.SetParent(head);
        currentShield.transform.localPosition = restingPosition;

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

    void RaiseShield(InputState input)
    {
        if(input.LockOn)
        {
            currentShield.transform.DOLocalMove(restingPosition, raiseSpeed);
        }
        else
        {
            if (input.RaiseShield)
            {
                if (!raiseHeld)
                {
                    currentShield.transform.DOLocalMove(raisedPosition, raiseSpeed);
                    raiseHeld = true;
                }
            }
            else
            {
                if (raiseHeld)
                {
                    currentShield.transform.DOLocalMove(restingPosition, raiseSpeed);
                    raiseHeld = false;
                }
            }
        }
    }
}
