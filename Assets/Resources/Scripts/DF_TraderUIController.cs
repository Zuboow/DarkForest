using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DF_TraderUIController : MonoBehaviour
{
    public static GameObject traderUIContainer;
    public static bool trading = false;
    public static GameObject[] itemAmountObjects;

    public static void ActivateTraderUI()
    {
        trading = true;

        traderUIContainer = GameObject.FindGameObjectWithTag("MainUI").gameObject.transform.Find("TraderUI").gameObject;
        traderUIContainer.SetActive(true);
        itemAmountObjects = GameObject.FindGameObjectsWithTag("TraderUIItemInfo");
        UpdateItemAmountObjects();
    }

    public void DeactivateTraderUI()
    {
        trading = false;
        traderUIContainer.SetActive(false);
    }

    public void BuyItem(string itemName) //przerobić przedmioty do kupienia na klasę
    {
        switch (itemName)
        {
            case "HealthPotion":
                if (DF_PlayerStatsController.player.gold - 20 >= 0)
                {
                    DF_PlayerStatsController.player.healthPotions += 1;
                    DF_GoldController.RemoveGold(-20);
                    UpdateItemAmountObjects();
                }
                break;
        }
    }

    static void UpdateItemAmountObjects()
    {
        foreach (GameObject amountObject in itemAmountObjects)
        {
            switch (amountObject.name)
            {
                case "HealthPotionAmount":
                    amountObject.GetComponent<TextMeshProUGUI>().text = "Amount: " + DF_PlayerStatsController.player.healthPotions;
                    break;
            }
        }
    }
}
