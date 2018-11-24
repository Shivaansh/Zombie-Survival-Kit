using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_EquipmentManager {

    [Test]
    public void Test_EquipmentManagerSimplePasses() {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator Test_EquipmentManagerWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
