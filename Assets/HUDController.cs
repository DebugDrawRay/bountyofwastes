using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HUDController : MonoBehaviour
{
    [Header("Data Display")]
    public Text textData;
    private bool textDisplayed;
    void Start()
    {
        EventSystem.DataDisplayEvent += DisplayData;
        textData.text = "";
    }

    void OnDestroy()
    {
        EventSystem.DataDisplayEvent -= DisplayData;
    }

    void DisplayData(TextAsset text)
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
    }
}
