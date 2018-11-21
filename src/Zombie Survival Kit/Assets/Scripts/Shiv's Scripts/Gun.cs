using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Gun: A class used to define Gun behavior and define values required for Gun operation
/// </summary>
public class Gun : MonoBehaviour {

    [SerializeField] int ammoPerClip = 7; //max capacity for a clip
    int ammoInClip;  //the number of bullets in the current clip, if this reaches 0 a reload is needed

    [SerializeField] GameObject bullet; //reference to a bullet gameObject

    [SerializeField] AudioClip gunShotSound; //sound clip for gun shot

    [SerializeField] AudioClip reloadSound; //sound clip for reloading

    [SerializeField] float bulletSpeed = 100f; //speed of bullet

    [SerializeField] float fireRate = 0.8f; //rate of fire for weapon

    [SerializeField] Transform barrelLocation; //location of the gun barrell in 3d space

    //[SerializeField] GameObject muzzleFlashPrefab;

    [SerializeField] GameObject cam; //reference to MainCamera object

    Transform startPoint;//start point for bullet

    float startTime; //used to create a clock to keep track of time



    /// <summary>  
    ///  Start: This method runs at the start of the game and initialized the various variables required for the level.  
    /// </summary>  
    void Start () {
        startTime = Time.time;//start clock

        if (barrelLocation == null)
            barrelLocation = transform;

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        startPoint = cam.transform;

        reload();
	}
    /// <summary>  
    ///  Update: This method is called once per frame.  
    /// </summary>  
	void Update ()
    { 
        float elapsedTime = Time.time - startTime; //current time - time since last shot
        if(Input.GetKeyDown(KeyCode.Mouse0) && elapsedTime >= fireRate) //when left mouse button is clicked, and time passed since last shot is more than fire rate
        {
            shoot(); //shoot a bullet
            elapsedTime = 0f; //reset time since last shot to 0
            startTime = Time.time; //start time set to time of shot
        }
        else if(Input.GetKeyDown(KeyCode.R)) //if the 'R' button is pressed
        {
            reload(); //reload the gun
        }	
	}

    /// <summary>  
    ///  shoot: This method shoots a bullet towards a target, and updates the number of bullets in the clip. If the clip is empty, this method reloads the weapon.  
    ///  It also instantiates a temporary "muzzle flash" at the end of the gun barrell
    /// </summary> 
    void shoot()
    {
        if(ammoInClip > 0)
        {
            createBullet();
            GetComponent<Animator>().SetTrigger("Fire");
            ammoInClip--;
        }
        else
        {
            reload();
        }

        //if (ammoInClip == 0)
          //  reload();

        //GameObject tempFlash;
        //tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
    }

    /// <summary>  
    ///  reload: This method reloads the weapon by restoring the number of bullets in the clip to full capacity.  
    /// </summary>  
    void reload()
    {
        ammoInClip = ammoPerClip; //loads the weapon
        AudioSource.PlayClipAtPoint(reloadSound, transform.position);
    }

    /// <summary>  
    ///  createBullet: This method instantiates a bullet at the end of the gun barrel and sends it forward with respect to the starting position.  
    /// </summary>  
    void createBullet()
    {
        Instantiate(bullet, startPoint.position, startPoint.rotation).GetComponent<Rigidbody>().AddForce(startPoint.forward * bulletSpeed);
        AudioSource.PlayClipAtPoint(gunShotSound, transform.position);
    }

    /// <summary>
    /// getAmmoInClip: An int method used for other classes to access the amount of ammo left in the clip
    /// </summary>
    /// <returns>The amount of ammo left in the clip</returns>
    public int getAmmoInClip()
    {
        return ammoInClip;
    }
}
