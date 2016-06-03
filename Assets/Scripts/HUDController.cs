using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HUDController : MonoBehaviour
{
    [Header("Data Display")]
    public Text textData;
    public Image imageData;
    private bool textDisplayed;
    private bool imageDisplayed;

    void Start()
    {
        EventSystem.DataDisplayEvent += DisplayData;
        textData.text = "";
        Color none = Color.white;
        none.a = 0;
        imageData.color = none;
        imageData.sprite = null;
    }

    void OnDestroy()
    {
        EventSystem.DataDisplayEvent -= DisplayData;
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
                textData.DOText(text.text, 2f, false, ScrambleMode.All);
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
            imageData.color = Color.white;
            imageData.sprite = image;
        }
    }
}
