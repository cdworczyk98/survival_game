using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupSystem : MonoBehaviour
{
    public int pickupDist = 300;
    public Text pickupText;
    public KeyCode pickupKey = KeyCode.E;
    public InventoryManager inventoryManager;
    

   
    void Start()
    {
        pickupText.text = string.Empty;
    }

   
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickupDist))
        {
            if(pickupText)
                pickupText.text = string.Empty;

            if(hit.transform.GetComponent<ItemPickup>() != null && hit.collider.CompareTag("Item"))
            {
                var item = hit.collider.GetComponent<Item>();

                if (pickupText)
                    {
                        if(item.stackable)
                            pickupText.text = string.Format("Press {0} to pickup: {1}x{2}", pickupKey.ToString(), item.itemName, item.stackSize);
                        else
                            pickupText.text = string.Format("Press {0} to pickup: {1}", pickupKey.ToString(), item.itemName);
                    }

                if (Input.GetKeyDown(KeyCode.E))
                {

                }
                                     
            }
        }
    }
}
