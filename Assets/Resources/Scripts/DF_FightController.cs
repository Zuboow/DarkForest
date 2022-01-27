using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DF_FightController : MonoBehaviour
{
    public static bool fighting = false, readyForNextMove = false;
    public GameObject playerHealthSlider, enemyHealthSlider, playerDamageIndicatorSpawner, enemyDamageIndicatorSpawner, potionAmount, playerHealthBar, enemyHealthBar;
    public static GameObject currentEnemyObject, playerSpawner, enemySpawner, playerModelObject, enemyModelObject;
    static DF_Class_Enemy currentEnemy;

    private void OnEnable()
    {
        playerSpawner = GameObject.FindGameObjectWithTag("PlayerModelSpawner").gameObject;
        enemySpawner = GameObject.FindGameObjectWithTag("EnemyModelSpawner").gameObject;
    }

    public static void SetFightingMode(bool fightingMode)
    {
        fighting = fightingMode;
    }

    public void SetUpFightingField()
    {
        currentEnemy = currentEnemyObject.GetComponent<DF_EnemyRandomizer>().enemy;
        Debug.Log("Fighting " + currentEnemy.enemyName);
        GetComponent<DF_SpriteLoader>().SetAvatar(currentEnemy.enemyName);

        playerHealthSlider.GetComponent<Scrollbar>().size = 1f - (DF_PlayerStatsController.player.currentHealth * 1f) / (DF_PlayerStatsController.player.maxHealth * 1f);
        enemyHealthSlider.GetComponent<Scrollbar>().size = 1f - (currentEnemy.currentHealth * 1f) / (currentEnemy.maxHealth * 1f);

        potionAmount.GetComponent<TextMeshProUGUI>().text = DF_PlayerStatsController.player.healthPotions.ToString();

        UpdateGUI();

        readyForNextMove = true;
    }

    public void ManageSpawnedEntities(bool removeEntities)
    {
        if (!removeEntities)
        {
            playerModelObject = Instantiate(Resources.Load("Prefabs/Entities/Hero") as GameObject, playerSpawner.transform.position, Quaternion.identity);
            enemyModelObject = Instantiate(Resources.Load("Prefabs/Entities/" + currentEnemyObject.GetComponent<DF_EnemyRandomizer>().enemy.enemyName) as GameObject, enemySpawner.transform.position, Quaternion.identity);

            SetUpFightingField();  
        } 
        else
        {
            Destroy(playerModelObject);
            Destroy(enemyModelObject);
        }
    }

    private void Update()
    {
        if (fighting && currentEnemy == null)
        {
            SetUpFightingField();
        } 
        else if (fighting)
        {
            if (!readyForNextMove && !DF_CameraController.switching && currentEnemy.currentHealth > 0)
            {
                DamagePlayer(currentEnemy.minDamage, currentEnemy.maxDamage);
            }
            if ((currentEnemy.currentHealth == 0 || DF_PlayerStatsController.player.currentHealth == 0) && !DF_CameraController.switching && DF_CameraController.activeCamera.tag == "FightCamera") 
            {
                if (currentEnemy.currentHealth == 0)
                {
                    DF_InfoController.currentInfoObject = currentEnemyObject;
                    DF_InfoController.ActivateInfo();
                    DF_InfoController.SetInfo("VictoriousFight", DF_GoldController.AddGold(currentEnemy.gold), 0);
                }
                DF_CameraController.switching = true;
                currentEnemy = null;
                SetFightingMode(false);
            }
        }
    }

    public void Attack()
    {
        int damageDealt = Random.Range(DF_PlayerStatsController.player.damage - (int)(DF_PlayerStatsController.player.damage * 0.3), DF_PlayerStatsController.player.damage + (int)(DF_PlayerStatsController.player.damage * 0.3));
        currentEnemy.DamageEnemy(damageDealt);
        UpdateGUI();
        enemyDamageIndicatorSpawner.GetComponent<DF_DamageIndicatorSpawner>().SpawnDamageIndicator(damageDealt, true);
        readyForNextMove = false;
    }

    public void UseFirstSkill()
    {
        currentEnemy.DamageEnemy(15);
        UpdateGUI();
        enemyDamageIndicatorSpawner.GetComponent<DF_DamageIndicatorSpawner>().SpawnDamageIndicator(15, true);
        readyForNextMove = false;
    }

    public void UseSecondSkill()
    {
        currentEnemy.DamageEnemy(50);
        UpdateGUI();
        enemyDamageIndicatorSpawner.GetComponent<DF_DamageIndicatorSpawner>().SpawnDamageIndicator(50, true);
        readyForNextMove = false;
    }

    public void Heal()
    {
        int healingAmountToShowOnGUI = DF_PlayerStatsController.HealPlayer(50, true);

        if (healingAmountToShowOnGUI > 0)
        {
            playerDamageIndicatorSpawner.GetComponent<DF_DamageIndicatorSpawner>().SpawnDamageIndicator(healingAmountToShowOnGUI, false);
            potionAmount.GetComponent<TextMeshProUGUI>().text = DF_PlayerStatsController.player.healthPotions.ToString();
        }
        UpdateGUI();
    }

    public void DamagePlayer(int minDamage, int maxDamage)
    {
        int damageDealt = Random.Range(minDamage, maxDamage + 1) * -1;
        DF_PlayerStatsController.DamagePlayer(damageDealt);
        UpdateGUI();
        playerDamageIndicatorSpawner.GetComponent<DF_DamageIndicatorSpawner>().SpawnDamageIndicator(System.Math.Abs(damageDealt), true);
        readyForNextMove = true;
    }

    public void UpdateGUI()
    {
        playerHealthSlider.GetComponent<Scrollbar>().size = 1f - (DF_PlayerStatsController.player.currentHealth * 1f) / (DF_PlayerStatsController.player.maxHealth * 1f);
        playerHealthBar.GetComponent<TextMeshProUGUI>().text = DF_PlayerStatsController.player.currentHealth + "/" + DF_PlayerStatsController.player.maxHealth;
        if (currentEnemy != null)
        {
            enemyHealthSlider.GetComponent<Scrollbar>().size = 1f - (currentEnemy.currentHealth * 1f) / (currentEnemy.maxHealth * 1f);
            enemyHealthBar.GetComponent<TextMeshProUGUI>().text = currentEnemy.currentHealth + "/" + currentEnemy.maxHealth;
        }
    }
}
