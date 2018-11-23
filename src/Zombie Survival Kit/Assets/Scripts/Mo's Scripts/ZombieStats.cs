using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ZombieStats: A class used to manage stats specific to the zombies, inherited from CharacterStats
/// </summary>
public class ZombieStats : CharacterStats
{
    //Animator to control death animation
    private Animator animator;

    //Delay and timer to allow animation to complete
    [SerializeField] float delay;
    private float timeAnimStarted;

    //Booleans to check if the zombie has died or dropped an item
    private bool isDead = false;
    private bool isDropped = false;

    //Array of droppable objects
    private GameObject[] Drops;

    //To store the death location
    private Transform zombie;

    /// <summary>
    /// Start: Is a void method used for initialization
    /// </summary>
    private void Start()
    {
        //Initializes the animator reference and their initial health to max 
        animator = GetComponent<Animator>();
        curHealth = maxHealth;

        //Loads All the possible items that can be dropped by a zombie
        Drops = new GameObject[10];
        Drops[0] = Resources.Load<GameObject>("PrefabItems/HeadArmor");
        Drops[1] = Resources.Load<GameObject>("PrefabItems/Axe");
        Drops[2] = Resources.Load<GameObject>("PrefabItems/RangeWeapon");
        Drops[3] = Resources.Load<GameObject>("PrefabItems/Watermelon");
        Drops[4] = Resources.Load<GameObject>("PrefabItems/LegArmor");
        Drops[5] = Resources.Load<GameObject>("PrefabItems/Apple");
        Drops[6] = Resources.Load<GameObject>("PrefabItems/ChestArmor");
        Drops[7] = Resources.Load<GameObject>("PrefabItems/FeetArmor");
        Drops[8] = Resources.Load<GameObject>("PrefabItems/OffHand");
        Drops[9] = Resources.Load<GameObject>("PrefabItems/Cloak");
    }

    /// <summary>
    /// Update: Is a void method that is called once per frame
    /// </summary>
    private void Update()
    {
        //If the delay has passed since the zombie's health has reached 0
        if (Time.time >= timeAnimStarted + delay && isDead == true)
        {
            //Mark the death location and destroy the zombie game object
            Vector3 deathLocation = zombie.transform.position;
            Destroy(gameObject);

            //Drops a random item
            System.Random r = new System.Random();
            int dropChoice = r.Next(0, 10);
            DropItem(deathLocation, dropChoice);
        }
    }

    /// <summary>
    /// Die: An overwritten Die method to control what happens when the zombie dies
    /// </summary>
    public override void Die()
    {
        base.Die();
        zombie.GetComponent<Enemy>().enabled = false;

        //Let the update method know the zombie is dead, play the animation, and start the timer
        isDead = true;
        animator.SetBool("dead", true);
        timeAnimStarted = Time.time;
    }

    /// <summary>
    /// DropItem: A void method used to select an item to drop after an enemy has died
    /// </summary>
    /// <param name="deathLocation">The location of the dead enemy</param>
    /// <param name="dropChoice">Determines which item is dropped from the dead enemy</param>
    private void DropItem(Vector3 deathLocation, int dropChoice)
    {
        if (!isDropped) //So multiple items do not drop from the enemy
        {
            isDropped = true;

            switch (dropChoice)
            {
                case 0:
                    Debug.Log("Dropped HeadArmor");
                    GameObject HeadArmor = Instantiate(Drops[dropChoice]) as GameObject;
                    HeadArmor.transform.position = deathLocation;
                    break;
                case 1:
                    Debug.Log("Dropped MeleeWeapon");
                    GameObject MeleeWeapon = Instantiate(Drops[dropChoice]) as GameObject;
                    MeleeWeapon.transform.position = deathLocation;
                    break;
                case 2:
                    Debug.Log("Dropped RangeWeapon");
                    GameObject RangeWeapon = Instantiate(Drops[dropChoice]) as GameObject;
                    RangeWeapon.transform.position = deathLocation;
                    break;
                case 3:
                    Debug.Log("Dropped Watermelon");
                    GameObject Watermelon = Instantiate(Drops[dropChoice]) as GameObject;
                    Watermelon.transform.position = deathLocation;
                    break;
                case 4:
                    Debug.Log("Dropped LegArmor");
                    GameObject LegArmor = Instantiate(Drops[dropChoice]) as GameObject;
                    LegArmor.transform.position = deathLocation;
                    break;
                case 5:
                    Debug.Log("Dropped Apple");
                    GameObject Apple = Instantiate(Drops[dropChoice]) as GameObject;
                    Apple.transform.position = deathLocation;
                    break;
                case 6:
                    Debug.Log("Dropped ChestArmor");
                    GameObject ChestArmor = Instantiate(Drops[dropChoice]) as GameObject;
                    ChestArmor.transform.position = deathLocation;
                    break;
                case 7:
                    Debug.Log("Dropped FeetArmor");
                    GameObject FeetArmor = Instantiate(Drops[dropChoice]) as GameObject;
                    FeetArmor.transform.position = deathLocation;
                    break;
                case 8:
                    Debug.Log("Dropped OffHand");
                    GameObject OffHand = Instantiate(Drops[dropChoice]) as GameObject;
                    OffHand.transform.position = deathLocation;
                    break;
                case 9:
                    Debug.Log("Dropped Cloak");
                    GameObject Cloak = Instantiate(Drops[dropChoice]) as GameObject;
                    Cloak.transform.position = deathLocation;
                    break;
            }
        }
    }

    /// <summary>
    /// IsDead: A boolean method that allows other classes to know if the zombie has died
    /// </summary>
    /// <returns>true or false</returns>
    public bool isDeath()
    {
        return isDead;
    }
}
