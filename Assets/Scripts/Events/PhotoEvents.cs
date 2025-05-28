using System;
using UnityEngine;

public class PhotoEvents
{
    public event Action<bool> onPhotoCapture;
    public void PhotoCapture(bool captured)
    {
        if (onPhotoCapture != null)
        {
            onPhotoCapture(captured);
        }
    }
}
