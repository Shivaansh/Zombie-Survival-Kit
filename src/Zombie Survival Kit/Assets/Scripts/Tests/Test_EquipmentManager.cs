using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_EquipmentManager {

    //[Test]
    //public void Test_EquipmentManagerSimplePasses() {
    //    // Use the Assert class to test conditions.
    //}

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator Test_Equip() {
        // Use the Assert class to test conditions.
        // yield to skip a frame

        var GameManager = new GameObject().AddComponent<EquipmentManager>();
        yield return null;
        var i = new GameObject().AddComponent<Inventory>();
        GameObject gun = Resources.Load<GameObject>("PrefabItems/Gun");
        GameObject player = Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer");
        i.Construct(20, player);
        GameManager.Construct(i, gun, player);
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/ChestArmor");
        EquipmentItem item2 = Resources.Load<EquipmentItem>("Items/Cloak");

        GameManager.Equip(item1);
        yield return null;
        Assert.True(GameManager.equippedItems[(int)item1.equipSlot] == item1);

        GameManager.Equip(item2);
        yield return null;
        Assert.True(GameManager.equippedItems[(int)item2.equipSlot] == item2);
        Assert.True(i.items.Contains(item1));

        yield return null;
    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameobject in Object.FindObjectsOfType<Inventory>())
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in Object.FindObjectsOfType<EquipmentManager>())
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("Gun"))
        {
            Object.Destroy(gameobject);
        }
    }
}
