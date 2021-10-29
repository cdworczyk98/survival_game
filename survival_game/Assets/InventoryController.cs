using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    [HideInInspector]
    public ItemGrid selectedItemGrid;

    InventoryItem selectedItem;
    RectTransform rectTransform;
    private void Update() 
    {
        //get mouse position to be used for dragging
        if(selectedItem != null)
            rectTransform.position = Input.mousePosition;

       if(selectedItemGrid == null) { return; }

       if(Input.GetMouseButton(0))
       {
           //select the item at the grid from the mouse POS
            Vector2Int tileGridPosition = selectedItemGrid.GetTileGridPosition(Input.mousePosition);

            if(selectedItem == null)
            {
                //pickup the item
               selectedItem = selectedItemGrid.PickupItem(tileGridPosition.x, tileGridPosition.y);
               if(selectedItem != null)
               {
                   //drag the item along with mouse
                    rectTransform = selectedItem.GetComponent<RectTransform>();
               }
               
            }
            else
            {
                //drop the item and make selected false
                selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y);
                selectedItem = null;
            }
       }
    }
}
