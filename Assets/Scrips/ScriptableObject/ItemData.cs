using UnityEngine;

//�������� �뵵
public enum ItemType
{
    Resource,
    Equipable,
    Consumable,
    Architecture
}

//�Һ������ ����
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
    public int value;
}

//ScriptableObject�� ������ ���� �� �ְ� �޴�â�� �߰�.
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    //�������� ����
    [Header("Info")]
    public string rcode;
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    //������ ��������
    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    //�Һ� ������
    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    //����(ũ����Ʈ) ������
    [Header("Craft")]
    public bool isCraftItem;
    public MaterialData[] materials;

    [Header("Equip")]
    public GameObject equipPrefab;
}
