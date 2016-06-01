using UnityEngine;
using System.Collections;

public static class EventSystem
{
    public delegate void SendTextAsset(TextAsset asset);

    public static event SendTextAsset DataDisplayEvent;
    public static void DisplayData(TextAsset asset)
    {
        if(DataDisplayEvent != null)
        {
            DataDisplayEvent(asset);
        }
    }
}
