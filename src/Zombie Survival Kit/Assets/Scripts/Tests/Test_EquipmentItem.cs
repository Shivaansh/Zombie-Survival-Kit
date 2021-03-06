﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_EquipmentItem {

    //[Test]
    //public void Test_EquipmentItemSimplePasses() {
    //    // Use the Assert class to test conditions.
    //}

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator Test_Use() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/HeadArmor");

        GameManager.GetComponent<Inventory>().Add(item1);
        yield return null;
        Assert.True(GameManager.GetComponent<Inventory>().items.Contains(item1));

        GameManager.GetComponent<Inventory>().items[0].Use();
        yield return null;
        Assert.False(GameManager.GetComponent<Inventory>().items.Contains(item1));
        Assert.True(GameManager.GetComponent<EquipmentManager>().equippedItems[(int)item1.equipSlot] == item1);

        yield return null;
    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GameController"))
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GameManager"))
        {
            Object.Destroy(gameobject);
        }
    }
}
