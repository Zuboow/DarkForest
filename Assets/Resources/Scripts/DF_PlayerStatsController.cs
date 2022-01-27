using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DF_PlayerStatsController : MonoBehaviour
{
    public static DF_Class_Player player;
    static GameObject healthAmountTextObject, scriptExecutor;

    private void OnEnable()
    {
        player = new DF_Class_Player("Player", 100, 100, 12, 0, 5);
        healthAmountTextObject = GameObject.FindGameObjectWithTag("HealthAmountTextObject").gameObject;
        scriptExecutor = GameObject.FindGameObjectWithTag("ScriptExecutor").gameObject;
        UpdateHealthTextObject();
    }

    public static int DamagePlayer(int _damageReceived)
    {
        if (player.currentHealth + _damageReceived >= 0)
            player.currentHealth += _damageReceived;
        else
        {
            player.currentHealth = 0;
            DF_GameProgressController.gameOver = true;
            DF_InfoController.ActivateInfo();
            DF_InfoController.SetInfo("Death", 0, 0);
        }
        
        UpdateHealthTextObject();
        scriptExecutor.GetComponent<DF_FightController>().UpdateGUI();

        return _damageReceived;
    }

    public static int HealPlayer(int _healingAmount, bool usingPotion)
    {
        int numberToReturn = _healingAmount;

        if ((player.currentHealth != player.maxHealth && player.healthPotions > 0) || !usingPotion)
        {
            if (player.currentHealth + _healingAmount <= player.maxHealth)
                player.currentHealth += _healingAmount;
            else
            {
                numberToReturn = player.maxHealth - player.currentHealth;
                player.currentHealth = player.maxHealth;
            }

            if (usingPotion) 
                player.healthPotions -= 1;
        }
        else if (usingPotion)
        {
            return 0;
        }

        UpdateHealthTextObject();
        scriptExecutor.GetComponent<DF_FightController>().UpdateGUI();

        return numberToReturn;
    }

    public static void UpdateHealthTextObject()
    {
        healthAmountTextObject.GetComponent<TextMeshProUGUI>().text = player.currentHealth + "/" + player.maxHealth;
    }
}
