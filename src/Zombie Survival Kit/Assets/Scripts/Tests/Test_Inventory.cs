﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;

public class Test_Inventory {

    //[Test]
    //public void Test_InventorySimplePasses() {
    //    // Use the Assert class to test conditions.
    //}

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator Test_Add() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer");
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/HeadArmor");
        GameManager.GetComponent<Inventory>().Add(item1);

        yield return null;
        Assert.True(GameManager.GetComponent<Inventory>().items.Contains(item1));
    }

    [UnityTest]
    public IEnumerator Test_InventoryEquipmentConsumable(){
        // Use the Assert class to test conditions.
        // yield to skip a frame
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer");
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/HeadArmor");
        GameManager.GetComponent<Inventory>().Add(item1);

        yield return null;
        Assert.True(GameManager.GetComponent<Inventory>().items.Contains(item1));

        GameManager.GetComponent<Inventory>().InventoryEquipmentConsumable(item1);
        yield return null;
        Assert.False(GameManager.GetComponent<Inventory>().items.Contains(item1));
        Assert.IsNull(GameObject.FindGameObjectWithTag("Equipment"));

    }

    [UnityTest]
    public IEnumerator Test_RemoveFromInventory()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer");
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/HeadArmor");
        GameManager.GetComponent<Inventory>().Add(item1);

        yield return null;
        Assert.True(GameManager.GetComponent<Inventory>().items.Contains(item1));

        GameManager.GetComponent<Inventory>().RemoveFromInventory(item1);
        yield return null;
        var spawnedItem = GameObject.FindGameObjectWithTag("Equipment");
        var prefabOfSpawnedItem = PrefabUtility.GetCorrespondingObjectFromSource(spawnedItem);
        Assert.False(GameManager.GetComponent<Inventory>().items.Contains(item1));
        Assert.AreEqual(prefabOfSpawnedItem, Resources.Load<EquipmentItem>("PrefabItems/HeadArmor"));

        yield return null;
    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GameManager"))
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("Equipment"))
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GameController"))
        {
            Object.Destroy(gameobject);
        }
    }
}
