using UnityEngine;
using System.Collections;

public static class EventSystem
{
    public delegate void SendTextAsset(TextAsset text, Sprite image);
    public delegate void SendFloat(float targetFloat);
    public delegate void Toggle();

    public static event SendTextAsset DataDisplayEvent;
    public static void DisplayData(TextAsset text, Sprite image)
    {
        if (DataDisplayEvent != null)
        {
            DataDisplayEvent(text, image);
        }
    }

    public static event SendFloat ScanProgressEvent;
    public static void ScanProgress(float targetFloat)
    {
        if(ScanProgressEvent != null)
        {
            ScanProgressEvent(targetFloat);
        }
    }

    public static event Toggle ScanModeEvent;
    public static void ToggleScanMode()
    {
        if(ScanModeEvent != null)
        {
            ScanModeEvent();
        }
    }
}
