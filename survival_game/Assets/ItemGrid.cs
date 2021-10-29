using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    const float tileSizeWidth = 32;
    const float tileSizeHeight = 32;
    InventoryItem[,] inventoryItemSlot;
    RectTransform rectTransform;
    [SerializeField] int gridSizeWidth = 20;
    [SerializeField] int gridSizeHeight = 20;

    private void Start() {
        rectTransform  = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
    }

    private void Init(int width, int height)
    {
        //initialise the grid size and draw it
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizeHeight, height * tileSizeHeight);
        rectTransform.sizeDelta = size; //resize THIS transform to the inv size, icons are 32px X 32px so 2x2 inv would be size 64x64
    }

    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();
    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        //Get position of mouse and turn it into a grid coordinate (X,Y)
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidth);
        tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeHeight);

        return tileGridPosition;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        //take the instatiated item object and get its transform
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform); //set its parent as this grid
        inventoryItemSlot[posX, posY] = inventoryItem; //place the item inside the 2D array inventoryItemSlot at the X and Y position from the grid

        //init the transform of the item on grid with needed offsets
        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight / 2);

        rectTransform.localPosition = position; //place objects
    }

    public InventoryItem PickupItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];
        inventoryItemSlot[x, y] = null;
        return toReturn;
    }
}
