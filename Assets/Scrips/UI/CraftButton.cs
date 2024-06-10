using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    public GameObject craftWindow;

    public GameObject ItemSlot_01;
    public GameObject ItemSlot_02;
    public GameObject ItemSlot_03;
    public GameObject ItemSlot_04;
    public GameObject ItemSlot_05;
    public GameObject ItemSlot_06;
    public GameObject ItemSlot_07;
    public GameObject ItemSlot_08;
    public GameObject ItemSlot_09;
    public GameObject ItemSlot_10;
    public GameObject ItemSlot_11;
    public GameObject ItemSlot_12;

    public ItemData Item_01;
    public ItemData Item_02;
    public ItemData Item_03;
    public ItemData Item_04;
    public ItemData Item_05;
    public ItemData Item_06;
    public ItemData Item_07;
    public ItemData Item_08;
    public ItemData Item_09;
    public ItemData Item_10;
    public ItemData Item_11;
    public ItemData Item_12;

    

    public void Craft()
    {
        if (craftWindow.activeSelf == false)
        {
            ItemData itemSlotData = ItemSlot_01.GetComponent<ItemData>();

            itemSlotData = Item_01;
        }
    }
}
