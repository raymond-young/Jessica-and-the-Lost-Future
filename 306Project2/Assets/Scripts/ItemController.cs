using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour {

    private List<GameObject> items = new List<GameObject>();

    private List<Sprite> itemSprites = new List<Sprite>();

	// Use this for initialization
	void Start () {

        GameObject itemSlots = GameObject.FindGameObjectWithTag("ItemSlots");

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


        // Make sure halo is visible.
        GameObject[] withHalo = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in withHalo) {
            //item.GetComponent<ParticleSystemRenderer>().sortingLayerName = "Objects";
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
