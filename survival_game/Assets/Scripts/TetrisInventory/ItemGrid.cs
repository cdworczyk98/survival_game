using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float tileSizeWidth = 32;
    public const float tileSizeHeight = 32;
    InventoryItem[,] inventoryItemSlot;
    RectTransform rectTransform;
    [SerializeField] int gridSizeWidth = 20;
    [SerializeField] int gridSizeHeight = 20;

    private void Start() {
        rectTransform  = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
    }

    private void CleanGridRefrence(InventoryItem item)
    {
        for (int ix = 0; ix < item.itemData.width; ix++)
        {
            for (int iy = 0; iy < item.itemData.hieght; iy++)
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
            }
        }
    }

    private void Init(int width, int height)
    {
        //initialise the grid size and draw it
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizeHeight, height * tileSizeHeight);
        rectTransform.sizeDelta = size; //resize THIS transform to the inv size, icons are 32px X 32px so 2x2 inv would be size 64x64
    }

    internal InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x,y];
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

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        if(BoundaryCheck(posX, posY, inventoryItem.itemData.width, inventoryItem.itemData.hieght) == false)
        {
            return false;
        }

        if(OverlapCheck(posX, posY, inventoryItem.itemData.width, inventoryItem.itemData.hieght, ref overlapItem) == false)
        {
            overlapItem = null;
            return false;
        }

        if(overlapItem != null)
        {
            CleanGridRefrence(overlapItem);
        }

        //take the instatiated item object and get its transform
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform); //set its parent as this grid

        for (int x = 0; x < inventoryItem.itemData.width; x++)
        {
            for (int y = 0; y < inventoryItem.itemData.hieght; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem; //place the item inside the 2D array inventoryItemSlot at the X and Y position from the grid
            }
        }
        
        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;

        Vector2 position = CalculcatePositionOnGrid(inventoryItem, posX, posY);

        rectTransform.localPosition = position; //place objects

        return true;
    }

    public Vector2 CalculcatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
         //init the transform of the item on grid with needed offsets
        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.itemData.width / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.itemData.hieght / 2);
        return position;
    }

    public InventoryItem PickupItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];

        if(toReturn == null) {return null;}

        CleanGridRefrence(toReturn);

        return toReturn;
    }

    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(inventoryItemSlot[posX+x, posY+y] != null)
                {
                    if(overlapItem == null)
                    {
                        overlapItem = inventoryItemSlot[posX + x, posY + y];
                    }
                    else
                    {
                        if(overlapItem != inventoryItemSlot[posX + x, posY + y])
                        {
                            return false;
                        }
                    }
                    
                }
            }
        }

        return true;
    }

    bool PositionCheck(int posX, int posY)
    {
        if(posX < 0 || posY < 0)
        {
            return false;
        }

        if(posX >= gridSizeWidth || posY >= gridSizeHeight)
        {
            return false;
        }

        return true;
    }

    bool BoundaryCheck(int posX, int posY, int width, int height)
    {
        if(PositionCheck(posX, posY) == false) {return false;}

        posX += width-1;
        posY += height-1;

        if(PositionCheck(posX, posY) == false) {return false;}

        return true;
    }
}
