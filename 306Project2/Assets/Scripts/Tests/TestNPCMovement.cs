using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;


public class TestNPCMovement {

    //Tests NPC movement
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses() {

        GameObject enemyPrefab = (GameObject) Resources.Load("PrefabsForUnitTests/BadNPC");

        Object.Instantiate(enemyPrefab);

        GameObject instanciatedObj = GameObject.FindGameObjectWithTag("TestNPC");
        
        float time = 10f;

        //Loop until pos is 5,5
        while ((instanciatedObj.transform.position.x > 4.95 && instanciatedObj.transform.position.x < 5.05) && (instanciatedObj.transform.position.y > 4.95 && instanciatedObj.transform.position.y < 5.05))
        {
            time = time - Time.deltaTime;

            if (time < 0)
            {
                Assert.Fail();
                break;
            }
            yield return new WaitForFixedUpdate();
        }
       
    }
}
