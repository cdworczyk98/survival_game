using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DTInventory
{
    public class EquipmentManager : MonoBehaviour
    {

        private DTInventory inventory;

        public Transform headEquip; 
        public Transform rHandEquip; 
        public Transform lHandEquip; 

        public List<GameObject> weaponList;

        void Awake () {
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = 500;
        }


        void Start()
        {
            if (inventory == null)
                inventory = FindObjectOfType<DTInventory>();

            
        }

        void Update()
        {
            //Check all panels for any equipped items and equip
            foreach (var panel in inventory.equipmentPanels)
            {
                if(panel.equipedItem != null && panel.allowedItemType == "Melee")
                {
                    if(panel.equipedItem.equipItem != null)
                    {
                        panel.equipedItem.equipItem.SetActive(true);
                    }
                }
                
                //disable unequpped items
                if(panel.equipedItem == null && panel.allowedItemType == "Melee")
                {
                    
                }
            }
        }
    }
}

