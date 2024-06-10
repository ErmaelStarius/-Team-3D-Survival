using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;

    public Button button;
    public Image icon;
    public TextMeshProUGUI quatityText;
    private Outline outline;

    public UIInventory inventory;
    public UICraft craftInventory;
    public bool isCraftInventory = false;
    public bool isArchitectureInventory = false;

    public int index;
    public bool equipped;
    public int quantity;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = equipped;
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quatityText.text = quantity > 1 ? quantity.ToString() : string.Empty;

        if (outline != null)
        {
            outline.enabled = equipped;
        }
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quatityText.text = string.Empty;
    }
    
    public void OnClickButton()
    {
        if (isCraftInventory)
        {
            craftInventory.SelectItem(index);
        }
        else if(isArchitectureInventory)
        {
            inventory.SelectArchitecture(index);
        }
        else
        {
            inventory.SelectItem(index);
        }
    }
}
