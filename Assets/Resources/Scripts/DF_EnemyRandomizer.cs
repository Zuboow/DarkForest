using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DF_EnemyRandomizer : MonoBehaviour
{
    public DF_Class_Enemy enemy;

    private void OnEnable()
    {
        int index = Random.Range(0, DF_MapGenerator.enemiesJSON.enemies.Length);

        DF_Class_Enemy loadedIndex = DF_MapGenerator.enemiesJSON.enemies[index];
        enemy = new DF_Class_Enemy(loadedIndex.id, loadedIndex.minDamage, loadedIndex.maxDamage, loadedIndex.currentHealth, loadedIndex.maxHealth, loadedIndex.enemyName, loadedIndex.gold);
    }
}
