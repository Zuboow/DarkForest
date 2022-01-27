using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DF_InfoController : MonoBehaviour
{
    public static DF_Class_InfoList jsonInfoList;
    public static GameObject infoUIContainer, currentInfoObject;
    public static bool infoActivated = false;

    private void OnEnable()
    {
        infoUIContainer = GameObject.FindGameObjectWithTag("MainUI").gameObject.transform.Find("InfoUI").gameObject;
        jsonInfoList = JsonUtility.FromJson<DF_Class_InfoList>((Resources.Load("JSON/infoList") as TextAsset).text);
    }

    public static void SetInfo(string infoName, int goldAmountChange, int healthAmountChange)
    {
        foreach (DF_Class_Info info in jsonInfoList.infoList)
        {
            if (info.name == infoName)
            {
                GameObject.FindGameObjectWithTag("InfoUIText").GetComponent<TextMeshProUGUI>().text = info.info;
            }
        }
        if (goldAmountChange != 0)
            GameObject.FindGameObjectWithTag("InfoUIText").GetComponent<TextMeshProUGUI>().text += "\nGold " + (goldAmountChange > 0 ? "+" : "-") + Math.Abs(goldAmountChange);
        if (healthAmountChange != 0)
            GameObject.FindGameObjectWithTag("InfoUIText").GetComponent<TextMeshProUGUI>().text += "\nHealth points " + (healthAmountChange > 0 ? "+" : "-") + Math.Abs(healthAmountChange);
    }

    public static void ActivateInfo()
    {
        infoUIContainer.SetActive(true);
        infoActivated = true;
    }

    public void DeactivateInfo()
    {
        infoUIContainer.SetActive(false);
        infoActivated = false;
        if (!DF_GameProgressController.gameOver)
        {
            if (currentInfoObject != null)
            {
                Destroy(currentInfoObject);
                DF_GameProgressController.numberOfPOIs--;
                if (DF_GameProgressController.numberOfPOIs == 0)
                {
                    ActivateInfo();
                    SetInfo("Victory", 0, 0);
                    DF_GameProgressController.gameOver = true;
                }
            }
        } else
        {
            DF_GameProgressController.EndGame();
        }
    }
}
