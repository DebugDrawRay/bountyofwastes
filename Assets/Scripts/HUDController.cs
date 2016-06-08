using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HUDController : MonoBehaviour
{
    [Header("Data Display")]
    public Image scanModeImage;
    public Image scanProgressImage;
    public Text textData;
    public Image imageData;
    private bool textDisplayed;
    private bool imageDisplayed;


    void Start()
    {
        EventSystem.ScanModeEvent += ToggleScanMode;
        EventSystem.DataDisplayEvent += DisplayData;
        EventSystem.ScanProgressEvent += ScanProgress;
        textData.text = "";
        Color none = Color.white;
        none.a = 0;
        imageData.color = none;
        imageData.sprite = null;
    }

    void OnDestroy()
    {
        EventSystem.DataDisplayEvent -= DisplayData;
        EventSystem.ScanProgressEvent -= ScanProgress;
        EventSystem.ScanModeEvent -= ToggleScanMode;
    }

    void ToggleScanMode()
    {
        scanModeImage.enabled = !scanModeImage.enabled;
    }

    void ScanProgress(float progress)
    {
        scanProgressImage.fillAmount = progress;
        if(progress >= 1)
        {
            scanProgressImage.fillAmount = 0;
        }
    }

    void DisplayData(TextAsset text, Sprite image)
    {
        if(text == null)
        {
            textData.text = "";
            textDisplayed = false;
        }
        else
        {
            if (!textDisplayed)
            {
                textData.text = text.text;
                textDisplayed = true;
            }
        }

        if(image == null)
        {
            Color none = Color.white;
            none.a = 0;
            imageData.color = none;
            imageData.sprite = null;
        }
        else
        {
            Color white = Color.white;
            white.a = .5f;
            imageData.color = white;
            imageData.sprite = image;
        }
    }
}
