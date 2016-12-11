// Date   : 11.12.2016 18:59
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerHealth : MonoBehaviour {

    [SerializeField]
    [Range(5, 25)]
    private int health;

    private int maxHealth;

    public int Health { set { health = value; } }

    [SerializeField]
    private Text txtHealth;

    [SerializeField]
    private Text txtName;
    
    [SerializeField]
    private Color goodHealth;

    [SerializeField]
    private Color midHealth;

    [SerializeField]
    private Color lowHealth;

    [SerializeField]
    private bool isEnemy = false;

    void Start()
    {
        txtHealth.text = health.ToString();
        maxHealth = health;
    }

    public void InitEnemy(Customer customer, Enemy enemy)
    {
        txtName.text = customer.CustomerName;
        txtName.color = customer.Color;
        health = enemy.Health;
        maxHealth = enemy.Health;
        txtHealth.text = health.ToString();
        txtHealth.color = goodHealth;
    }

    public void GainLevel(int level)
    {
        maxHealth += level * 5;
        health = maxHealth;
        txtHealth.text = health.ToString();
        txtHealth.color = goodHealth;
        UIManager.main.ShowMessage("You rested well, and feel much stronger. Your max health is now  "+ maxHealth + "!");
    }

    public int AddToHealth(int addition)
    {
        health += addition;
        if(health <= 0)
        {
            health = 0;
        } else if (health > maxHealth)
        {
            health = maxHealth;
        }
        if(health > 0) { 
            float healthTemp = (float)health / (float)maxHealth * 100f;
            int healthPercentage = (int)healthTemp;
            if(healthPercentage >= 75)
            {
                txtHealth.color = goodHealth;
            } else if(healthPercentage >= 25)
            {
                txtHealth.color = midHealth;
            } else
            {
                txtHealth.color = lowHealth;
            }
        } else
        {
            txtHealth.color = lowHealth;
        }
        txtHealth.text = health.ToString();
        return health;
    }

}
