using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DF_Class_Player
{
    public string name;
    public int currentHealth, maxHealth, damage, gold, healthPotions;

    public DF_Class_Player(string _name, int _currentHealth, int _maxHealth, int _damage, int _gold, int _healthPotions)
    {
        name = _name;
        currentHealth = _currentHealth;
        maxHealth = _maxHealth;
        damage = _damage;
        gold = _gold;
        healthPotions = _healthPotions; //do przeniesienia na klasę Inventory
    }
}
