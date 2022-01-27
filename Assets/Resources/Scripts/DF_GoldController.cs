using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DF_GoldController : MonoBehaviour
{
    public static GameObject goldAmountTextObject;

    public static int AddGold(int amountAdded)
    {
        if (goldAmountTextObject == null)
            goldAmountTextObject = GameObject.FindGameObjectWithTag("GoldAmountTextObject").gameObject;
        DF_PlayerStatsController.player.gold += amountAdded;
        goldAmountTextObject.GetComponent<TextMeshProUGUI>().text = DF_PlayerStatsController.player.gold.ToString();

        return amountAdded;
    }

    public static int RemoveGold(int amountRemoved)
    {
        if (goldAmountTextObject == null)
            goldAmountTextObject = GameObject.FindGameObjectWithTag("GoldAmountTextObject").gameObject;
        DF_PlayerStatsController.player.gold += amountRemoved;
        goldAmountTextObject.GetComponent<TextMeshProUGUI>().text = DF_PlayerStatsController.player.gold.ToString();

        return amountRemoved;
    }
}
