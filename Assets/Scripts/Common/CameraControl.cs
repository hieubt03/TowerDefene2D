using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private int defaultWidth;
    private int lastUpdatedWidth;
    private float defaultCameraSize;

    void Start()
    {
        defaultWidth = lastUpdatedWidth = Camera.main.pixelWidth;
        defaultCameraSize = Camera.main.orthographicSize;
        ChangeCameraSize();
    }

    void Update()
    {
        if (lastUpdatedWidth != Camera.main.pixelWidth)
        {
            lastUpdatedWidth = Camera.main.pixelWidth;
            ChangeCameraSize();
        }
    }

    private void ChangeCameraSize()
    {
        Camera.main.orthographicSize = defaultCameraSize * ((float)defaultWidth / (float)lastUpdatedWidth);
    }
}
