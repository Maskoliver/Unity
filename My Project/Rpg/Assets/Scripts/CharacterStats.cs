using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public Stat damage;
    public Stat armor;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        
    }
    public void UpdateHealth(int mod)
    {
        //mod -= armor.getValue();
        mod = Mathf.Clamp(mod, 0, int.MaxValue);
        currentHealth -= mod;
        Debug.Log(transform.name + " takes " + mod + " damage");
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
    public virtual void Die()
    {
        //Die !
        Debug.Log("Died");
    }
}
