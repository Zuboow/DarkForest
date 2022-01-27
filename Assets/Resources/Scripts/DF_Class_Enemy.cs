using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DF_Class_Enemy
{
    public string enemyName;
    public int id, minDamage, maxDamage, currentHealth, maxHealth, gold;
    
    public DF_Class_Enemy(int _id, int _minDamage, int _maxDamage, int _currentHealth, int _maxHealth, string _enemyName, int _gold)
    {
        id = _id;
        minDamage = _minDamage;
        maxDamage = _maxDamage;
        currentHealth = _currentHealth;
        maxHealth = _maxHealth;
        enemyName = _enemyName;
        gold = _gold;
    }

    public void DamageEnemy(int _damageReceived)
    {
        if (currentHealth - _damageReceived > 0)
            currentHealth -= _damageReceived;
        else
            currentHealth = 0;
    }
}

[System.Serializable]
public class DF_Class_Enemies
{
    public DF_Class_Enemy[] enemies;
}
