using UnityEngine;

//아이템의 용도
public enum ItemType
{
    Resource,
    Equipable,
    Consumable,
    Architecture
}

//소비아이템 종류
public enum ConsumableType
{
    Hunger,
    Health,
    Thirst,
    Samina


}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[System.Serializable]
public class MaterialData
{
    public string materialName;
    public float value;
}

//ScriptableObject를 빠르게 만들 수 있게 메뉴창에 추가.
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    //아이템의 정보
    [Header("Info")]
    public string rcode;
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    //아이템 보유정도
    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    //소비 아이템
    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    //가공(크래프트) 아이템
    [Header("Craft")]
    public bool isCraftItem;
    public MaterialData[] materials;

    [Header("Equip")]
    public GameObject equipPrefab;
}
