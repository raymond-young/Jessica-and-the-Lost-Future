using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NUnitTests
{

    //Tests NPC movement
    [UnityTest]
    public IEnumerator TestNPCMovement()
    {

        SceneManager.LoadScene("Resources/Scenes for unit tests/TestMovementScene");

        yield return new WaitForSeconds(0.5f);

        GameObject instanciatedObj = GameObject.FindGameObjectWithTag("TestNPC");

        float time = 10f;

        //Loop until pos is (5, 5)
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

    //Tests Item pickup NOTE: user must press spacebar to pickup the item otherwise will fail
    [UnityTest]
    public IEnumerator TestItemPickup()
    {

        SceneManager.LoadScene("Resources/Scenes for unit tests/TestItemScene");

        yield return new WaitForSeconds(0.5f);

        GameObject item = GameObject.FindGameObjectWithTag("Item");

        //Tests that item is destroyed
        if (item != null)
        {
            Assert.Fail();
        }


        GameObject itemSlots = GameObject.FindGameObjectWithTag("ItemSlots");



        GameObject slot = itemSlots.transform.GetChild(0).gameObject;

        //Ensures correct item placed in the first slot
        if (!slot.GetComponent<Image>().sprite.name.Equals("genericItems_spritesheet_colored_5"))
        {
            Assert.Fail();
        }
        

    }
}
