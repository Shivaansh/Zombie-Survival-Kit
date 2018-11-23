using UnityEngine;

/// <summary>
/// CharacterStats: A class used to manage the common stats of players and enemies (characters)
/// </summary>
public class CharacterStats : MonoBehaviour
{
    //Integers to store the max and current health of the character
    public int maxHealth = 100;
    public int curHealth;

    //Stat types to hold the values of the character's damage and armour
    public Stat dmg;
    public Stat armour;

    /// <summary>
    /// Start: Is a void method used for health initialization
    /// </summary>
    private void Start()
    {
        curHealth = maxHealth; //sets the initial current health to the max health
    }

    /// <summary>
    /// TakeDamage: A method to make a character take damage and reduce health
    /// </summary>
    /// <param name="damage">The damage value of whoever is attacking the character</param>
    public void TakeDamage(int damage)
    {
        //The damage done to the character is the attacker's damage subtracted by their armour
        if (damage > armour.GetValue())
            damage -= armour.GetValue();
        else
            damage = 1; //Minimum damage value

        //Subtracts the new damage value from the character's current health
        if (curHealth > damage)
            curHealth -= damage;
        else
            curHealth = 0; //Do not go below 0 health

        Debug.Log(transform.name + " takes " + damage + " damage.");

        //If the player has reached 0 health, initiate the Die method
        if (curHealth <= 0)
            Die();
    }

    /// <summary>
    /// Die: A virtual die method that is meant to be overwritten by separate player and zombie child classes.
    /// </summary>
    /// <param name="item">The item being added to the inventory</param>
    public virtual void Die()
    {
        //Die, this is meant to be overwritten
        Debug.Log(transform.name + " has died.");

    }

}
