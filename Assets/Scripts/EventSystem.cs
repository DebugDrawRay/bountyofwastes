using UnityEngine;
using System.Collections;

public static class EventSystem
{
    public delegate void SendTextAsset(TextAsset text, Sprite image);

    public static event SendTextAsset DataDisplayEvent;
    public static void DisplayData(TextAsset text, Sprite image)
    {
        if(DataDisplayEvent != null)
        {
            DataDisplayEvent(text, image);
        }
    }
}
