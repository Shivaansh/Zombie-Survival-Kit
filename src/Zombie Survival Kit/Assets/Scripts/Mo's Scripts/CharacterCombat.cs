using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CharacterCombat: A class to manage combat between characters
/// </summary>
[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    //Variables to control how fast a character can attack and a timer/delay between attacks
    [SerializeField] float attackSpeed = 0.5f;
    private float attackCooldown = 0f;
    private float attackDelay = 1.5f;

    //Reference to the stats of the attacker
    private CharacterStats attackerStats;

    /// <summary>
    /// Start: Is a void method used for stats initialization
    /// </summary>
    private void Start()
    {
        attackerStats = GetComponent<CharacterStats>();
    }

    /// <summary>
    /// Update: Is a void method that is called once per frame
    /// </summary>
    private void Update()
    {
        attackCooldown -= Time.deltaTime; //Lowers the cooldown timer
    }

    /// <summary>
    /// Attack: A method to initiate an attack between 2 characters 
    /// </summary>
    /// <param name="targetStats">The stats of the character being attacked</param>
    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f) //If the cooldown timer has reached 0
        {
            //Start the coroutine to apply damage and refresh the cooldown
            StartCoroutine(DoDamage(targetStats, attackDelay));
            attackCooldown = 1f / attackSpeed;
        }
    }

    /// <summary>
    /// DoDamage: A method to reduce a character's damage after an attack
    /// </summary>
    /// <param name="stats">The stats of the character being attacked</param>
    /// <param name="delay">Delay time between attacks</param>
    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        //Wait for the delay to finish then apply the attacker's damage to the target
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(attackerStats.dmg.GetValue());
    }
}
