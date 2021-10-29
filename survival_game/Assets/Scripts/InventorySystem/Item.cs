using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Weapon,
        Food,
        Armour
    }

    public int id;
    public string itemName;
    public string description;
    [SerializeField] public ItemType type;
    public Sprite icon;

    [Header("Stack options")]
    public bool stackable;

    [Range(1, 100)]
    public int maxStackSize = 1;

    [Range(1, 100)]
    public int stackSize = 1;
    public int health;
    public int damage;
    public int defense;

}
