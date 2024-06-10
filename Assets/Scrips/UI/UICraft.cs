using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class UICraft : MonoBehaviour
{
    public ItemSlot[] craftSlots;
    public Transform craftSlotPanel;
    public GameObject craftWindow;

    public GameObject inventoryWindow;
    public UIInventory inventory;

    [Header("Selected Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;
    public GameObject craftButton;

    [Header("Material")]
    public TextMeshProUGUI materialLabel;
    public TextMeshProUGUI requiredMaterialName;
    public TextMeshProUGUI requiredMaterialValue;

    private int curEquipIndex;

    private ItemSlot selectedItemSlot;
    private ItemData selectedItem;
    private int selectedItemIndex = 0;

    void Start()
    {
        inventory = inventoryWindow.GetComponent<UIInventory>();

        craftWindow.SetActive(false);

        craftSlots = new ItemSlot[craftSlotPanel.childCount];

        for (int i = 0; i < craftSlots.Length; i++)
        {
            craftSlots[i] = craftSlotPanel.GetChild(i).GetComponent<ItemSlot>();
            craftSlots[i].index = i;
            craftSlots[i].craftInventory = this;
            craftSlots[i].isCraftInventory = true;
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

        materialLabel.text = string.Empty;
        requiredMaterialName.text = string.Empty;
        requiredMaterialValue.text = string.Empty;

        craftButton.SetActive(false);
    }

    public void UpdateUI()
    {
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

    public void SelectItem(int index)
    {
        if (craftSlots[index].item == null) return;

        selectedItemSlot = craftSlots[index];
        selectedItem = craftSlots[index].item;
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

        materialLabel.text = "Material";

        requiredMaterialName.text = string.Empty;
        requiredMaterialValue.text = string.Empty;

        for (int i = 0; i < selectedItem.materials.Length; i++)
        {
            requiredMaterialName.text += selectedItem.materials[i].materialName + "\n";
            requiredMaterialValue.text += inventory.GetItemQuantity(selectedItem.materials[i].materialName).ToString();
            requiredMaterialValue.text += " / ";
            requiredMaterialValue.text += selectedItem.materials[i].value.ToString() + "\n";
        }

        craftButton.SetActive(true);
    }

    public void Close()
    {
        ClearSelectedItemWindow();
        craftWindow.SetActive(false);
    }

    public void OnCloseButton()
    {
        Close();
    }

    bool CanCraft()
    {
        for (int i = 0; i < selectedItem.materials.Length; i++)
        {
            if (inventory.GetItemQuantity(selectedItem.materials[i].materialName) < selectedItem.materials[i].value)
            {
                return false;
            }
        }

        return true;
    }

    void ArchitectureCraft()
    {
        if (!CanCraft()) return;

        // craft
        ItemSlot slot = inventory.GetEmptyArchitectureSlot();

        if(slot == null) return;

        for (int i = 0; i < selectedItem.materials.Length; i++)
        {
            inventory.SubItemQuantity(selectedItem.materials[i].materialName, selectedItem.materials[i].value);
        }

        slot.item = selectedItem;
        slot.item.icon = selectedItem.icon;

        inventory.UpdateUI();
        
        ClearSelectedItemWindow();
    }

    public void OnCraftButton()
    {
        ArchitectureCraft();
    }
}
