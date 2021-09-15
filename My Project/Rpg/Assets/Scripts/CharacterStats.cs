using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int level = 1;
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public Stat Armor;
    public Stat Damage;
    public Stat Str;
    public Stat Agi;
    public Stat Int;
   

    void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(Mathf.RoundToInt(Damage.getValue()));
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Heal(5);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            levelUp();
        }
    }
    public void TakeDamage(int mod)
    {
        mod -= Armor.getValue();
        mod = Mathf.Clamp(mod, 0, int.MaxValue);
        currentHealth -= mod;
        Debug.Log(transform.name + " takes " + mod + " damage and have " + currentHealth + " hp left");
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(int mod)
    {
        mod = Mathf.Clamp(mod, 0, int.MaxValue);
        currentHealth += mod;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log(transform.name + " healed " + mod + " hp and have " + currentHealth + " hp");
      
    }
    public virtual void Die()
    {
        //Die !
        Debug.Log("Died");
    }

    public void addStr(int mod)
    {
        Debug.Log("Increased str by " + mod + " and damage increased by " + mod*2);
        Damage.AddModifier(mod*2);
    }

    public void levelUp()
    {
        Str.AddModifier(1);
        addStr(1);
    }
}
