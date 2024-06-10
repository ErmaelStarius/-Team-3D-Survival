using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;
using System.Reflection;



public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public ItemSlot[] craftSlots;
    public ItemSlot[] architectureSlots;

    public GameObject inventoryWindow;
    public Transform slotPanel;
    public Transform ArchitectureSlotPanel;
    public Transform dropPosition;
    public GameObject craftWindow;
    public Transform craftSlotPanel;
    public UICraft craft;

    [Header("Selected Item")]

    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;
    public GameObject buildButton;

    private int curEquipIndex;

    private PlayerController controller;
    private PlayerCondition condition;

    private ItemData selectedItem;
    private int selectedItemIndex = 0;

    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;

        controller.inventory += Toggle;
        CharacterManager.Instance.Player.addItem += AddItem;

        craft = craftWindow.GetComponent<UICraft>();

        inventoryWindow.SetActive(false);              
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;

        }

        craftSlots = new ItemSlot[craftSlotPanel.childCount];

        for (int i = 0; i < craftSlots.Length; i++)
        {
            craftSlots[i] = craftSlotPanel.GetChild(i).GetComponent<ItemSlot>();
            craftSlots[i].index = i;
            craftSlots[i].inventory = this;
        }

        architectureSlots = new ItemSlot[ArchitectureSlotPanel.childCount];

        for (int i = 0; i < architectureSlots.Length; i++)
        {
            architectureSlots[i] = ArchitectureSlotPanel.GetChild(i).GetComponent<ItemSlot>();
            architectureSlots[i].index = i;
            architectureSlots[i].inventory = this;
            architectureSlots[i].isArchitectureInventory = true;
        }

        ClearSelectedItemWindow();
        UpdateUI();
    }

    void ClearSelectedItemWindow()
    {


        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
        buildButton.SetActive(false);
    }

    public void Toggle()
    {
        if(IsOpen())
        {
            inventoryWindow.SetActive(false);
            craft.Close();
        }
        else
        {
            ClearSelectedItemWindow();
            inventoryWindow.SetActive(true);
        }
    }


    
    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    //�������� �ߺ��Ǵ��� �������� �ְ�, ����(�������) üũ, ���ڸ��� ���ٸ� ����
    public void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        if (data.canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        ThrowItem(data);
        CharacterManager.Instance.Player.itemData = null;
    }



    
    
    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }

        for (int i = 0; i < craftSlots.Length; i++)
        {
            if (craftSlots[i].item != null)
            {
                craftSlots[i].Set();
            }
            else
            {
                craftSlots[i].Clear();
            }
        }
    }

    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    public ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    public ItemSlot GetEmptyArchitectureSlot()
    {
        for (int i = 0; i < architectureSlots.Length; i++)
        {
            if (architectureSlots[i].item == null)
            {
                return architectureSlots[i];
            }
        }
        return null;
    }

    //������ ������
    public void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * UnityEngine.Random.value * 360));
    }
    
    public void SelectItem(int index)
    {
        ClearSelectedItemWindow();

        if (slots[index].item == null) return;


        selectedItem = slots[index].item;
        selectedItemIndex = index;


        selectedItemName.text = selectedItem.displayName;
        selectedItemDescription.text = selectedItem.description;

        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            selectedItemStatName.text += selectedItem.consumables[i].type.ToString() + "\n";
            selectedItemStatValue.text += selectedItem.consumables[i].value.ToString() + "\n";
        }

        useButton.SetActive(selectedItem.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.type == ItemType.Equipable && !slots[index].equipped);
        unEquipButton.SetActive(selectedItem.type == ItemType.Equipable && slots[index].equipped);
        dropButton.SetActive(true);
    }

    public void SelectArchitecture(int index)
    {
        ClearSelectedItemWindow();

        if (architectureSlots[index].item == null) return;


        selectedItem = architectureSlots[index].item;
        selectedItemIndex = index;


        selectedItemName.text = selectedItem.displayName;
        selectedItemDescription.text = selectedItem.description;

        buildButton.SetActive(true);
    }

    public void OnUseButton()
    {
        if (selectedItem.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.consumables.Length; i++)
            {
                switch (selectedItem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(selectedItem.consumables[i].value); 
                        break;
                    case ConsumableType.Hunger:
                        condition.Eat(selectedItem.consumables[i].value); 
                        break;
                    case ConsumableType.Thirst:
                        condition.Drink(selectedItem.consumables[i].value);
                        break;
                    case ConsumableType.Samina:
                        condition.Power(selectedItem.consumables[i].value);
                        break;
                }
            }
            RemoveSelctedItem();
        }
    }

    public void OnDropButton()
    {
        if(selectedItem.type == ItemType.Equipable && slots[selectedItemIndex].equipped)
        {
            UnEquip(selectedItemIndex);
        }

        ThrowItem(selectedItem);
        RemoveSelctedItem();
    }

    
    void RemoveSelctedItem()
    {

        slots[selectedItemIndex].quantity--;


       if (slots[selectedItemIndex].quantity <= 0)
        {
            selectedItem = null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex = -1;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }


    public void OnEquipButton()
    {
        if (slots[curEquipIndex].equipped)
        {
            UnEquip(curEquipIndex);
        }

        slots[selectedItemIndex].equipped = true;
        curEquipIndex = selectedItemIndex;
        CharacterManager.Instance.Player.equip.EquipNew(selectedItem);

        UpdateUI();

        SelectItem(selectedItemIndex);
    }

    void UnEquip(int index)
    {
        slots[index].equipped = false;
        CharacterManager.Instance.Player.equip.UnEquip();
        UpdateUI();

        if (selectedItemIndex == index)
        {
            SelectItem(selectedItemIndex);
        }
    }

    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIndex);
    }
    
    void CraftUI()
    {
        craftWindow.SetActive(true);
    }

    public void OnCraftButton()
    {
        CraftUI();
    }

    public int GetItemQuantity(string name)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                continue;
            }
            if (slots[i].item.rcode == name)
            {
                return slots[i].quantity;
            }
        }
        return 0;
    }

    public void SubItemQuantity(string name, int subValue)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                continue;
            }
            if (slots[i].item.rcode == name)
            {
                slots[i].quantity -= subValue;

                if(slots[i].quantity <= 0)
                {
                    slots[i].item = null;
                }

                return;
            }
        }

        return;
    }

    public void OnBuildButton()
    {
        // TODO: 건물 건축하기

        Instantiate(architectureSlots[selectedItemIndex].item.dropPrefab, CharacterManager.Instance.Player.transform.position + Vector3.forward, Quaternion.identity);

        RemoveSelctedArchitecture();
    }

    void RemoveSelctedArchitecture()
    {

        selectedItem = null;
        architectureSlots[selectedItemIndex].item = null;
        selectedItemIndex = -1;
        ClearSelectedItemWindow();

        UpdateUI();
    }
}
