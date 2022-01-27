using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DF_DamageIndicatorSpawner : MonoBehaviour
{
    public void SpawnDamageIndicator(int amount, bool isDamage)
    {
        GameObject spawnedIndicator = Instantiate(Resources.Load("Prefabs/DamageIndicator") as GameObject, transform.position + Vector3.up, Quaternion.identity);
        if (tag == "EnemyModelSpawner")
        {
            spawnedIndicator.transform.position = spawnedIndicator.transform.position + new Vector3(0, 0, -0.5f);
        }
        spawnedIndicator.transform.GetChild(0).GetComponent<TextMeshPro>().text = (isDamage ? "-" : "+") + amount;
        if (!isDamage)
            spawnedIndicator.transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.green;
        spawnedIndicator.transform.parent = transform;
    }
}
