using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    [HideInInspector]
    public ItemGrid selectedItemGrid;

    InventoryItem selectedItem;
    InventoryItem overlapItem;
    RectTransform rectTransform;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;

    InventoryHighlight inventoryHighlight;

    private void Awake() {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }
    private void Update() 
    {
        //get mouse position to be used for dragging
        ItemIconDrag();

        if(Input.GetKeyDown(KeyCode.Q)){
            CreateRandomItem();
            print("spawning stuff");
        }

       if(selectedItemGrid == null) 
       { 
           inventoryHighlight.Show(false);
           return; 
        }

       HandleHighlight();

       if(Input.GetMouseButton(0))
       {
           print("Clikcing inside grid.");
           LeftMouseButtonPress();
       }
    }

    InventoryItem itemToHighlight;
    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        if(selectedItem == null)
        {
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if(itemToHighlight != null)
            {
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetPosition(selectedItemGrid, itemToHighlight);
            }
            else
            {
                inventoryHighlight.Show(false);
            }
        }
            
        else
        {

        }
    }

    private void LeftMouseButtonPress()
    {
        
        Vector2Int tileGridPosition = GetTileGridPosition();

        if (selectedItem == null)
        {
            PickupItem(tileGridPosition);
        }
        else
        {
            PlaceItem(tileGridPosition);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;

        if (selectedItem != null)
        {
            position.x -= (selectedItem.itemData.width - 1) * ItemGrid.tileSizeWidth / 2;
            position.y -= (selectedItem.itemData.hieght - 1) * ItemGrid.tileSizeHeight / 2;
        }
        
        return selectedItemGrid.GetTileGridPosition(position);
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        //drop the item and make selected false
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);
        if(complete)
        {
            selectedItem = null;
            if(overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
            }
        }
        
    }

    private void PickupItem(Vector2Int tileGridPosition)
    {
        //pickup the item
        selectedItem = selectedItemGrid.PickupItem(tileGridPosition.x, tileGridPosition.y);
        if(selectedItem != null)
        {
            //drag the item along with mouse
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }

    public void ItemIconDrag()
    {
        if(selectedItem != null)
            rectTransform.position = Input.mousePosition;
    }

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }

}
