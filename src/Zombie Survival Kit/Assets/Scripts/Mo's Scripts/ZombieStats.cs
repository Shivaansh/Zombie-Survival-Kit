using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : CharacterStats
{
    private Animator animator;
    public float delay;
    private bool isDead = false;
    private bool isDropped = false;

    //Brian's Code
    public GameObject[] Drops;
    public Transform zombie;

    //Shiv's Code
    float timeAnimStarted;

    void Start()
    {
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

    private void Update()
    {
        
        if (Time.time >= timeAnimStarted + delay && isDead == true)
        {
            //The death location of the zombie
            Vector3 deathLocation = zombie.transform.position;

            Destroy(gameObject);

            //Generates a random number between 0-9
            System.Random r = new System.Random();
            int dropChoice = r.Next(0, 10);

            DropItem(deathLocation, dropChoice);
        }
    }

    public override void Die()
    {
        zombie.GetComponent<Enemy>().enabled = false;
        isDead = true;
        animator.SetBool("dead", true);
        timeAnimStarted = Time.time;

        

        base.Die();
        //Destroy(gameObject, delay);
        

        

        //DropItem(deathLocation, dropChoice);

    }

    /// <summary>
    /// DropItem: A void method used to select an item to drop after an enemy has died
    /// </summary>
    /// <param name="deathLocation">The location of the dead enemy</param>
    /// <param name="dropChoice">Determines which item is dropped from the dead enemy</param>
    private void DropItem(Vector3 deathLocation, int dropChoice)
    {
        if (!isDropped)
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

    public bool isDeath()
    {
        return isDead;
    }
}
