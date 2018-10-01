using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour {

    private List<GameObject> items = new List<GameObject>();

    private List<Sprite> itemSprites = new List<Sprite>();

    private PlayerController player;


	// Use this for initialization
	void Start () {

        GameObject itemSlots = GameObject.FindGameObjectWithTag("ItemSlots");

        player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerController>();


        //Loads item slots
        for (int i = 0; i < itemSlots.transform.childCount; i++)
        {
            items.Add(itemSlots.transform.GetChild(i).gameObject);
        }

        Object[] sprites = Resources.LoadAll("Sprites", typeof(Sprite));
        //Loads item sprites
        for (int i = 0; i < sprites.Length; i++)
        {
            Sprite sprite = (Sprite) sprites[i];
            itemSprites.Add(sprite);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Item"))
        {
            //Loop through item slots to find one that is free
            foreach (GameObject item in items)
            {
                if (item.GetComponent<Image>().sprite.name.Equals("Background"))
                {
                    foreach (Sprite sprite in itemSprites)
                    {

                        if (collision.gameObject.GetComponent<SpriteRenderer>().sprite.Equals(sprite))
                        {
                            item.GetComponent<Image>().sprite = sprite;
                            player.ChangeConfidence(20);
                            break;
                        }
                    }

                    break;
                }
            }

            items.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
        
       
    }
}
