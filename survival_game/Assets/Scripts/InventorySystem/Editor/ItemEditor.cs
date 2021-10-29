using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Item)), CanEditMultipleObjects]
public class ItemEditor : Editor
{
    Item item;
    public override void OnInspectorGUI()
    {
        item = target as Item;
            
        if(item)
        DrawGeneralItem();
            
        var style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = Color.red;
        style.fontStyle = FontStyle.Bold;

        if (GUILayout.Button("Save changes?", style))
        {
            EditorUtility.SetDirty(item);
            EditorSceneManager.MarkSceneDirty(item.gameObject.scene);
        }
    }

    public void DrawGeneralItem()
    {
        GUILayout.Label("General item settings", EditorStyles.boldLabel);
        GUILayout.BeginVertical("HelpBox");
        item.itemName = EditorGUILayout.TextField("Name", item.itemName);
        item.description = EditorGUILayout.TextField("Description", item.description);
        item.type = (Item.ItemType)EditorGUILayout.EnumPopup("Item Type", item.type);


        switch (item.type)
        {
            case Item.ItemType.Weapon:
                item.damage = EditorGUILayout.IntSlider("Weapon damage", item.damage, 1, 100);
                break;
            case Item.ItemType.Armour:
                item.damage = EditorGUILayout.IntSlider("Armour Defense", item.defense, 1, 100); 
                break;
            case Item.ItemType.Food:
                item.damage = EditorGUILayout.IntSlider("Food health", item.health, 1, 100); 
                break;
            default:
                break;
        }

        item.icon = (Sprite)EditorGUILayout.ObjectField("Item icon", item.icon, typeof(Sprite), false);
        item.id = EditorGUILayout.IntField("ID", item.id);

        

        if (GUILayout.Button("Generate ID"))
        {
            item.id = Random.Range(0, int.MaxValue);
        }
        


        GUILayout.Label("Stackable?", EditorStyles.boldLabel);
        GUILayout.BeginVertical("HelpBox");
        item.stackable = EditorGUILayout.Toggle("Stackable", item.stackable);
        if (item.stackable)
        {
            item.stackSize = EditorGUILayout.IntSlider("Item stack size", item.stackSize, 1, 100);
            item.maxStackSize = EditorGUILayout.IntSlider("Max stack size", item.maxStackSize, 1, 100);
        }
        GUILayout.EndVertical();

        GUILayout.EndVertical();
    }
}
