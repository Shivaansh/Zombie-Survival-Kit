using UnityEngine;
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
        var GameManager = new GameObject().AddComponent<Inventory>();
        GameObject player = Resources.Load<GameObject>("PrefabPlayer/FPSController");
        GameManager.Construct(20, player);
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/HeadArmor");
        GameManager.Add(item1);

        yield return null;
        Assert.True(GameManager.items.Contains(item1));
    }

    [UnityTest]
    public IEnumerator Test_InventoryEquipmentConsumable(){
        // Use the Assert class to test conditions.
        // yield to skip a frame
        var GameManager = new GameObject().AddComponent<Inventory>();
        GameObject player = Resources.Load<GameObject>("PrefabPlayer/FPSController");
        GameManager.Construct(20, player);
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/HeadArmor");
        GameManager.Add(item1);

        yield return null;
        Assert.True(GameManager.items.Contains(item1));

        GameManager.InventoryEquipmentConsumable(item1);
        yield return null;
        Assert.False(GameManager.items.Contains(item1));

    }

    [UnityTest]
    public IEnumerator Test_RemoveFromInventory()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        var GameManager = new GameObject().AddComponent<Inventory>();
        GameObject player = Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer");
        GameManager.Construct(20, player);
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/HeadArmor");
        GameManager.Add(item1);

        yield return null;
        Assert.True(GameManager.items.Contains(item1));

        GameManager.RemoveFromInventory(item1);
        yield return null;
        var spawnedItem = GameObject.FindGameObjectWithTag("Equipment");
        var prefabOfSpawnedItem = PrefabUtility.GetCorrespondingObjectFromSource(spawnedItem);
        Assert.False(GameManager.items.Contains(item1));
        Assert.AreEqual(prefabOfSpawnedItem, Resources.Load<EquipmentItem>("PrefabItems/HeadArmor"));

        yield return null;
    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameobject in Object.FindObjectsOfType<Inventory>())
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("Equipment"))
        {
            Object.Destroy(gameobject);
        }
    }
}
