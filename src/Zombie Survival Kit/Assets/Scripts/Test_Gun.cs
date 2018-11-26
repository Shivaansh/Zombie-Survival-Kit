using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_Gun {
    /*
     * Methods to test are:
     * start (the setup stuff)
     * update(checking for input and updating ammo count)
     * shoot(uses create bullet and reload)
     * reload(plays a sound too)
     * creatBullter(instantiate bullet, play sound)
     * get ammo in clip (returns ammo in clip)
     * playshotatsound(plays sound)
     * playreloadsound(plays reload sound)
     */ 

        //Gun prefab in prefab items

        //pass bullet prefab in constructor
    [Test]
    public void Test_GunSimplePasses() {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator Test_GunWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_Shoot()
    {
        var GunManager = new GameObject().AddComponent<Gun>(); //create a gameobject with thte gun script as a component
        GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>("PrefabItems/bullet")); //load a bullet prefab from resources folder
        GameObject camera = GameObject.Instantiate(Resources.Load<GameObject>("Prefabplayer/PrimaryPlayer"));
        //GunManager.Construct(7, 7, bullet, camera, 1000f, 0.8f, 2f); //construct a gun with specified parameter
        

        yield return null;
        //Assert.False(GunManager.bulletHasBeenFired);

        GunManager.shoot();

        //Assert.True(GunManager.bulletHasBeenFired);
    }

    [UnityTest]
    public IEnumerator Test_Reload()
    {
        
        yield return null;
    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GameController"))
        {
            Object.Destroy(gameobject);
        }
    }
}
