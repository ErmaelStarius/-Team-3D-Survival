using TMPro;
using UnityEngine;

public class UICraft : MonoBehaviour
{
    public ItemSlot[] slots;

    public Transform slotPanel;
    public GameObject craftWindow;

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

    private ItemData selectedItem;
    private int selectedItemIndex = 0;

    void Start()
    {
        craftWindow.SetActive(false);

        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].craftInventory = this;
            slots[i].isCraftInventory = true;
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
    }

    public void SelectItem(int index)
    {
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

        materialLabel.text = "Material";

        requiredMaterialName.text = string.Empty;
        requiredMaterialValue.text = string.Empty;

        for (int i = 0; i < selectedItem.materials.Length; i++)
        {
            requiredMaterialName.text += selectedItem.materials[i].materialName + "\n";
            requiredMaterialValue.text += selectedItem.materials[i].value.ToString() + "\n";
        }

        craftButton.SetActive(true);
    }

    void Close()
    {
        craftWindow.SetActive(false);
    }

    public void OnCloseButton()
    {
        Close();
    }
}
