using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DF_CameraLoader : MonoBehaviour
{
    GameObject loadedCamera;
    public bool isMainUICanvas = false;

    private void Update()
    {
        if (loadedCamera == null && GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            loadedCamera = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
            if (isMainUICanvas)
            {
                GetComponent<Canvas>().worldCamera = loadedCamera.GetComponent<Camera>();
                GetComponent<Canvas>().planeDistance = 1;
            }
        }
    }
}
