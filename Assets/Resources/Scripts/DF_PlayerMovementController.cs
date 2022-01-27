using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DF_PlayerMovementController : MonoBehaviour
{
    GameObject destination;
    public static GameObject playerObject;
    bool moving = false;

    private void Update()
    {
        if (playerObject != null && !moving && DF_FightController.fighting != true && !DF_CameraController.switching && DF_PlayerStatsController.player.currentHealth > 0 && !DF_TraderUIController.trading && !DF_InfoController.infoActivated)
        {
            GetComponent<DF_FightController>().ManageSpawnedEntities(true);

            if (Input.GetKey(KeyCode.W))
            {
                CheckObject(playerObject.transform.parent.transform.parent.position, new Vector3(-1, 0, 0));
            }
            else if (Input.GetKey(KeyCode.S))
            {
                CheckObject(playerObject.transform.parent.transform.parent.position, new Vector3(1, 0, 0));
            }
            if (Input.GetKey(KeyCode.A))
            {
                CheckObject(playerObject.transform.parent.transform.parent.position, new Vector3(0, 0, -1));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                CheckObject(playerObject.transform.parent.transform.parent.position, new Vector3(0, 0, 1));
            }
        }
        if (moving)
        {
            MovePlayer(destination.transform.GetChild(0).gameObject);
        }
    }

    void CheckObject(Vector3 slotPosition, Vector3 vector)
    {
        RaycastHit hit;
        Ray ray = new Ray(slotPosition, vector);

        if (Physics.Raycast(ray, out hit, 0.5f))
        {
            if (hit.transform.tag == "Slot")
            {
                if (hit.transform.GetChild(0).transform.childCount == 0)
                {
                    destination = hit.transform.gameObject;
                    moving = true;
                }
                else if (hit.transform.GetChild(0).GetChild(0).tag == "Tree")
                {
                    destination = hit.transform.gameObject;
                    moving = true;
                    if (Random.Range(0,10) > 7 && DF_PlayerStatsController.player.currentHealth > 1 && DF_PlayerStatsController.player.gold > 20)
                    {
                        DF_InfoController.ActivateInfo();
                        DF_InfoController.SetInfo("Bandits", DF_GoldController.RemoveGold(Random.Range(20, DF_PlayerStatsController.player.gold) * -1), DF_PlayerStatsController.DamagePlayer(Random.Range(1, DF_PlayerStatsController.player.currentHealth) * -1));
                    }
                }
                else if (hit.transform.GetChild(0).GetChild(0).tag == "Trader")
                {
                    destination = hit.transform.gameObject;
                    moving = true;
                    DF_TraderUIController.ActivateTraderUI();
                }
                else if (hit.transform.GetChild(0).GetChild(0).tag == "POI")
                {
                    destination = hit.transform.gameObject;
                    moving = true;

                    switch (Random.Range(0, 100))
                    {
                        case int n when n < 50:
                            DF_FightController.currentEnemyObject = hit.transform.GetChild(0).GetChild(0).gameObject;
                            GetComponent<DF_FightController>().ManageSpawnedEntities(false);
                            DF_CameraController.switching = true;
                            break;
                        case int n when n < 70:
                            DF_InfoController.currentInfoObject = hit.transform.GetChild(0).GetChild(0).gameObject;
                            DF_InfoController.ActivateInfo();
                            DF_InfoController.SetInfo("GoldBagFound", DF_GoldController.AddGold(Random.Range(10, 81)), 0);
                            break;
                        case int n when n < 95:
                            DF_InfoController.currentInfoObject = hit.transform.GetChild(0).GetChild(0).gameObject;
                            DF_InfoController.ActivateInfo();
                            DF_InfoController.SetInfo("Nothing", 0, 0);
                            break;
                        case int n when n < 100:
                            DF_InfoController.currentInfoObject = hit.transform.GetChild(0).GetChild(0).gameObject;
                            DF_InfoController.ActivateInfo();
                            DF_InfoController.SetInfo("HealingGnome", 0, DF_PlayerStatsController.HealPlayer(DF_PlayerStatsController.player.maxHealth - DF_PlayerStatsController.player.currentHealth, false));
                            break;
                    }
                }
            }
        }
    }

    void MovePlayer(GameObject playerDestination)
    {
        if (playerObject.transform.position != playerDestination.transform.position)
        {
            playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, playerDestination.transform.position, 0.08f);
        }
        else
        {
            playerObject.transform.parent = playerDestination.transform;
            moving = false;
        }
    }
}
