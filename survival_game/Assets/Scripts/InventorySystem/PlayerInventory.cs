using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInventory : MonoBehaviour
{
    
    public int invSize;
    public bool dragging;

    public List<GameObject> invCells = new List<GameObject>();
    public InventoryManager inventoryManager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void AddItem()
    {

    }

    public void CellClick()
    {
        GameObject cellClicked = EventSystem.current.currentSelectedGameObject;
    }

     void GetCells()
    {
        foreach (Transform cellChild in inventoryManager.invUI.transform)
        {
            invCells.Add(cellChild.gameObject);
        }
    }
}
