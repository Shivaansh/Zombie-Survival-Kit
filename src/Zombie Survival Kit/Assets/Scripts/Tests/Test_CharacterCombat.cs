using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test_CharacterCombat
{
    [UnityTest]
    public IEnumerator Test_Attack()
    {
        GameObject GameManager = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/GameManager"));
        GameObject character = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        GameObject character2 = GameObject.Instantiate(Resources.Load<GameObject>("PrefabPlayer/PrimaryPlayer"));
        CharacterCombat attacker = character.GetComponent<CharacterCombat>();
        CharacterStats attackerStats = character.GetComponent<CharacterStats>();
        CharacterStats defenderStats = character2.GetComponent<CharacterStats>();
        yield return null;

        int oldHealth = defenderStats.curHealth;
        attacker.Attack(defenderStats);
        yield return new WaitForSeconds(2);
        Assert.True(defenderStats.curHealth == defenderStats.maxHealth - attackerStats.dmg.GetValue());

    }

    [TearDown]
    public void AfterEveryTest()
    {
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GameManager"))
        {
            Object.Destroy(gameobject);
        }
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("GameController"))
        {
            Object.Destroy(gameobject);
        }
    }
}
