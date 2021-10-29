using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New items database")]
public class ItemDatabase : ScriptableObject
{
    public string databaseName;
    public List<Item> items;
    
    public Item FindItem(string name)
        {
            foreach(var item in items)
            {
                if (item.name == name)
                    return item;
            }

            return null;
        }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
