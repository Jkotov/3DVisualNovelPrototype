using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item itemData;
    public GameObject pickupEffect;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (other.tag == "Player")
            {
                if (GameManager.instance.items.Count < GameManager.instance.slots.Length)
                {
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject);

                    GameManager.instance.AddItem(itemData);
                }
                else
                {
                    Debug.Log("Bag is full");
                }
            }
        }
    }
}
