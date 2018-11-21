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
    private Item[] Drops;
    public Transform zombie;

    //Shiv's Code
    float timeAnimStarted;

    void Start()
    {
        animator = GetComponent<Animator>();

        Item headArmor = ScriptableObject.CreateInstance<Item>();
        Item meleeWeapon = ScriptableObject.CreateInstance<Item>();
        Item rangeWeapon = ScriptableObject.CreateInstance<Item>();
        Item watermelon = ScriptableObject.CreateInstance<Item>();

        headArmor = Resources.Load<Item>("Items/HeadArmor");
        meleeWeapon = Resources.Load<Item>("Items/MeleeWeapon");
        rangeWeapon = Resources.Load<Item>("Items/RangeWeapon");
        watermelon = Resources.Load<Item>("Items/Watermelon");

        curHealth = maxHealth;

        Drops = new Item[4];
        Drops[0] = headArmor;
        Drops[1] = meleeWeapon;
        Drops[2] = rangeWeapon;
        Drops[3] = watermelon;
    }

    private void Update()
    {
        
        if (Time.time >= timeAnimStarted + delay && isDead == true)
        {
            Vector3 deathLocation = zombie.transform.position;

            Destroy(gameObject);

            System.Random r = new System.Random();
            int dropChoice = r.Next(0, 4);

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

    private void DropItem(Vector3 deathLocation, int dropChoice)
    {
        if (!isDropped)
        {
            isDropped = true;

            switch (dropChoice)
            {
                case 0:
                    Debug.Log("Dropped HeadArmor");
                    GameObject HeadArmor = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    HeadArmor.name = "HeadArmor";
                    HeadArmor.transform.localScale = new Vector3(0.3948148f, 0.3948148f, 0.3948148f);
                    HeadArmor.AddComponent<ItemStore>();
                    HeadArmor.GetComponent<ItemStore>().item = Drops[dropChoice];
                    HeadArmor.GetComponent<ItemStore>().radius = 3;
                    HeadArmor.AddComponent<Rigidbody>();
                    HeadArmor.GetComponent<Rigidbody>().useGravity = true;
                    HeadArmor.transform.position = deathLocation;
                    break;
                case 1:
                    Debug.Log("Dropped MeleeWeapon");
                    GameObject MeleeWeapon = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    MeleeWeapon.name = "MeleeWeapon";
                    MeleeWeapon.transform.localScale = new Vector3(0.3948148f, 0.3948148f, 0.3948148f);
                    MeleeWeapon.AddComponent<ItemStore>();
                    MeleeWeapon.GetComponent<ItemStore>().item = Drops[dropChoice];
                    MeleeWeapon.GetComponent<ItemStore>().radius = 3;
                    MeleeWeapon.AddComponent<Rigidbody>();
                    MeleeWeapon.GetComponent<Rigidbody>().useGravity = true;
                    MeleeWeapon.transform.position = deathLocation;
                    break;
                case 2:
                    Debug.Log("Dropped RangeWeapon");
                    GameObject RangeWeapon = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RangeWeapon.name = "RangeWeapon";
                    RangeWeapon.transform.localScale = new Vector3(0.3948148f, 0.3948148f, 0.3948148f);
                    RangeWeapon.AddComponent<ItemStore>();
                    RangeWeapon.GetComponent<ItemStore>().item = Drops[dropChoice];
                    RangeWeapon.GetComponent<ItemStore>().radius = 3;
                    RangeWeapon.AddComponent<Rigidbody>();
                    RangeWeapon.GetComponent<Rigidbody>().useGravity = true;
                    RangeWeapon.transform.position = deathLocation;
                    break;
                case 3:
                    Debug.Log("Dropped Watermelon");
                    GameObject Watermelon = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    Watermelon.name = "Watermelon";
                    Watermelon.transform.localScale = new Vector3(0.3948148f, 0.3948148f, 0.3948148f);
                    Watermelon.AddComponent<ItemStore>();
                    Watermelon.GetComponent<ItemStore>().item = Drops[dropChoice];
                    Watermelon.GetComponent<ItemStore>().radius = 3;
                    Watermelon.AddComponent<Rigidbody>();
                    Watermelon.GetComponent<Rigidbody>().useGravity = true;
                    Watermelon.transform.position = deathLocation;
                    break;
            }
        }
    }

    public bool isDeath()
    {
        return isDead;
    }
}
