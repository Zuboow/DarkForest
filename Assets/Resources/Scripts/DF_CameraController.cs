using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DF_CameraController : MonoBehaviour
{
    public static GameObject mainCamera, fightCamera, activeCamera, cameraDarkener;
    static float zoomingStart = 0f, zoomingEnd = 3f;
    public static bool switching = false, zoomedOut = true;

    private void OnEnable()
    {
        fightCamera = GameObject.FindGameObjectWithTag("FightCamera");
        cameraDarkener = GameObject.FindGameObjectWithTag("CameraDarkener");
    }

    private void Update()
    {
        if (switching)
        {
            switch (zoomedOut)
            {
                case true:
                    ZoomInCameraAndSwitch();
                    break;
                case false:
                    ZoomOutCamera();
                    break;
            }
        }
    }

    public static void SetMainCamera()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        activeCamera = mainCamera;
    }

    public static void SwitchCameras(bool main)
    {
        if (!main)
        {
            mainCamera.GetComponent<Camera>().enabled = false;
            fightCamera.GetComponent<Camera>().enabled = true;
            activeCamera = fightCamera;
        }
        else 
        {
            fightCamera.GetComponent<Camera>().enabled = false;
            mainCamera.GetComponent<Camera>().enabled = true;
            activeCamera = mainCamera;
        }
    }

    public static void ZoomInCameraAndSwitch()
    {
        if (zoomingStart < zoomingEnd)
        {
            zoomingStart += (zoomingStart + Time.deltaTime) * 0.1f;
            if (activeCamera.GetComponent<Camera>().fieldOfView > 0)
            {
                activeCamera.GetComponent<Camera>().fieldOfView -= 1f;
            }

            if (cameraDarkener.GetComponent<Image>().color.a + Time.deltaTime < 255)
            {
                cameraDarkener.GetComponent<Image>().color = new Color(0, 0, 0, cameraDarkener.GetComponent<Image>().color.a + Time.deltaTime * 2);
            } 
            else
            {
                cameraDarkener.GetComponent<Image>().color = new Color(0, 0, 0, 255);
            }
        } 
        else
        {
            zoomingStart = 0f;
            zoomedOut = false;
            SwitchCameras(activeCamera.tag == "FightCamera" ? true : false);
            activeCamera.GetComponent<Camera>().fieldOfView = 0;
        }
    }

    static void ZoomOutCamera()
    {
        if (zoomingStart < zoomingEnd)
        {
            zoomingStart += (zoomingStart + Time.deltaTime) * 0.1f;
            if (activeCamera.GetComponent<Camera>().fieldOfView <= 60)
            {
                activeCamera.GetComponent<Camera>().fieldOfView += 1f;
            }

            if (cameraDarkener.GetComponent<Image>().color.a - Time.deltaTime > 0)
            {
                cameraDarkener.GetComponent<Image>().color = new Color(0, 0, 0, cameraDarkener.GetComponent<Image>().color.a - Time.deltaTime * 2);
            }
            else
            {
                cameraDarkener.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
        }
        else
        {
            zoomedOut = true;
            zoomingStart = 0f;
            switching = false;
            if (activeCamera.tag == "FightCamera")
                DF_FightController.SetFightingMode(true);
        }
    }
}
