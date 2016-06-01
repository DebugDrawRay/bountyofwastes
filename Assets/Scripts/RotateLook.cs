using UnityEngine;
using DG.Tweening;

public class RotateLook : MonoBehaviour
{
    [Header("Rotate Axis'")]
    public Transform head;

    [Header("Properties")]
    public float sensitivity;
    public float smoothing;

    public CursorLockMode lockMode;
    
    //Rotations
    private float xRot;
    private float yRot;

    void Start()
    {
        Cursor.lockState = lockMode;
        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Subscribe(UpdateLook);
        }
    }

    void OnDestroy()
    {
        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Unsubscribe(UpdateLook);
        }
    }

    void OnEnable()
    {
        float currentX = head.localRotation.eulerAngles.x;
        if(currentX >= 270 && currentX <= 360)
        {
            currentX = -(360 - currentX);
        }

        xRot = currentX;
        yRot = transform.localRotation.eulerAngles.y;
    }

    void UpdateLook(InputState input)
    {
        if (enabled)
        {
            xRot += -input.Look.y * sensitivity;
            yRot += input.Look.x * sensitivity;

            xRot = Mathf.Clamp(xRot, -90, 90);
            if (yRot > 360)
            {
                yRot = 0;
            }
            if (yRot < 0)
            {
                yRot = 360;
            }
            yRot = Mathf.Clamp(yRot, 0, 360);

            Vector3 headRot = new Vector3(xRot, head.localRotation.eulerAngles.y, head.localRotation.eulerAngles.z);
            Vector3 bodyRot = new Vector3(transform.localRotation.eulerAngles.x, yRot, transform.localRotation.eulerAngles.z);

            head.DOLocalRotate(headRot, smoothing);
            transform.DORotate(bodyRot, smoothing);
        }
    }
}
