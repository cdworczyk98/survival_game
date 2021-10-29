using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class InventoryManager : MonoBehaviourPunCallbacks
{
    //private variables
    private bool showInv = false;
    public PlayerInventory playerInventory;
    public GameObject invUI;

    void Start()
    {
        if(photonView.IsMine)
        {
            invUI = GameObject.FindGameObjectWithTag("Inventory");
            SetPaused();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            showInv = !showInv;
            SetPaused();
        }
    }


    void SetPaused()
    {
        //set the canvas
        if(photonView.IsMine)invUI.SetActive(showInv);
        //set the cursor lock
        //Cursor.lockState = showInv ? CursorLockMode.None : CursorLockMode.Locked;
        //set the cursor visible
        //Cursor.visible = showInv;
    }

   
}
