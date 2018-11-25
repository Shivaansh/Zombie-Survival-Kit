using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;

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
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        player.name = "player";
        i.Construct(20, player);
        GameManager.Construct(i, gun, player);
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/ChestArmor");
        EquipmentItem item2 = Resources.Load<EquipmentItem>("Items/Cloak");
        EquipmentItem item3 = Resources.Load<EquipmentItem>("Items/RangeWeapon");

        GameManager.Equip(item1);
        yield return null;
        Assert.True(GameManager.equippedItems[(int)item1.equipSlot] == item1);

        GameManager.Equip(item2);
        yield return null;
        Assert.True(GameManager.equippedItems[(int)item2.equipSlot] == item2);
        Assert.True(i.items.Contains(item1));

        GameManager.Equip(item3);
        yield return null;
        Assert.True(GameManager.equippedItems[(int)item3.equipSlot] == item3);
        var spawnedItem = GameObject.FindGameObjectWithTag("Gun");
        Assert.IsNotNull(spawnedItem);


        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_Unequip()
    {
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

        GameManager.Equip(item1);
        yield return null;
        Assert.True(GameManager.equippedItems[(int)item1.equipSlot] == item1);

        GameManager.Unequip((int)item1.equipSlot);
        Assert.True(GameManager.equippedItems[(int)item1.equipSlot] == null);
        Assert.True(i.items.Contains(item1));

        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_UnequipAll()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame

        var GameManager = new GameObject().AddComponent<EquipmentManager>();
        yield return null;
        var i = new GameObject().AddComponent<Inventory>();
        GameObject gun = Resources.Load<GameObject>("PrefabItems/Gun");
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        player.name = "player";
        i.Construct(20, player);
        GameManager.Construct(i, gun, player);
        EquipmentItem item1 = Resources.Load<EquipmentItem>("Items/ChestArmor");
        EquipmentItem item2 = Resources.Load<EquipmentItem>("Items/HeadArmor");
        EquipmentItem item3 = Resources.Load<EquipmentItem>("Items/Axe");
        EquipmentItem item4 = Resources.Load<EquipmentItem>("Items/FeetArmor");
        EquipmentItem item5 = Resources.Load<EquipmentItem>("Items/OffHand");
        EquipmentItem item6 = Resources.Load<EquipmentItem>("Items/LegArmor");

        GameManager.Equip(item1);
        GameManager.Equip(item2);
        GameManager.Equip(item3);
        GameManager.Equip(item4);
        GameManager.Equip(item5);
        GameManager.Equip(item6);

        yield return null;
        Assert.True(GameManager.equippedItems[(int)item1.equipSlot] == item1);
        Assert.True(GameManager.equippedItems[(int)item2.equipSlot] == item2);
        Assert.True(GameManager.equippedItems[(int)item3.equipSlot] == item3);
        Assert.True(GameManager.equippedItems[(int)item4.equipSlot] == item4);
        Assert.True(GameManager.equippedItems[(int)item5.equipSlot] == item5);
        Assert.True(GameManager.equippedItems[(int)item6.equipSlot] == item6);

        GameManager.UnequipAll();

        Assert.IsNull(GameManager.equippedItems[(int)item1.equipSlot]);
        Assert.IsNull(GameManager.equippedItems[(int)item2.equipSlot]);
        Assert.IsNull(GameManager.equippedItems[(int)item3.equipSlot]);
        Assert.IsNull(GameManager.equippedItems[(int)item4.equipSlot]);
        Assert.IsNull(GameManager.equippedItems[(int)item5.equipSlot]);
        Assert.IsNull(GameManager.equippedItems[(int)item6.equipSlot]);

        Assert.True(i.items.Contains(item1));
        Assert.True(i.items.Contains(item2));
        Assert.True(i.items.Contains(item3));
        Assert.True(i.items.Contains(item4));
        Assert.True(i.items.Contains(item5));
        Assert.True(i.items.Contains(item6));

        yield return null;
    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameobject in GameObject.FindObjectsOfType<Inventory>())
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindObjectsOfType<EquipmentManager>())
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GameController"))
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("Gun"))
        {
            Object.Destroy(gameobject);
        }
    }
}
