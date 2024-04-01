// https://gamedesigntheory.blogspot.com/2010/09/controlling-aspect-ratio-in-unity.html

using System;
using UnityEngine;

public class CameraAspectEnforcer : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private const float TargetAspect = 16.0f / 9.0f;
    private float windowAspectX;
    private float windowAspectY;
    private const double Tolerance = 10;
    private void Start()
    {
        EnforceAspect();
    }

    private void Update()
    {
        
        if (Math.Abs(windowAspectX - Screen.width) > Tolerance || Math.Abs(windowAspectY - Screen.height) > Tolerance)
        {
            EnforceAspect();
        }
    }

    private void EnforceAspect()
    {
        windowAspectX = Screen.width;
        windowAspectY = Screen.height;
        var windowAspect = windowAspectX / windowAspectY;
        var scaleHeight = windowAspect / TargetAspect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = cam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            cam.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            cam.rect = rect;
        }
    }
}
