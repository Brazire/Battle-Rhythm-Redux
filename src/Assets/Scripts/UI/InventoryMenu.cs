using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject LeftSide;

    [SerializeField]
    private GameObject RightSide;

    [SerializeField]
    private GameObject ItemBase;

    [SerializeField]
    private Color SelectedButtonColor;

    private GameObject oldTab;
    private ColorBlock selectedColor;
    private ColorBlock unselectedColor;

    private bool isLeft;
    private float BaseItemHeight;

    private List<ScriptableItem> equipments;
    private List<ScriptableItem> consumables;
    private List<ScriptableItem> keyitems;

    private SelectedTab CurrentTab = SelectedTab.none;
    private enum SelectedTab
    {
        equipment,
        consumable,
        keyItem,
        none
    }

    void Awake()
    {
        oldTab = GameObject.Find("ConsumableB");
        unselectedColor = oldTab.GetComponent<Button>().colors;
        selectedColor = oldTab.GetComponent<Button>().colors;
        selectedColor.normalColor = SelectedButtonColor;

        BaseItemHeight = ItemBase.GetComponent<RectTransform>().rect.height + LeftSide.GetComponent<VerticalLayoutGroup>().spacing;
    }

    void OnEnable()
    {
        consumables = InventoryManager.iManager.GetItems(ScriptableItem.ItemType.consumable);
        keyitems = InventoryManager.iManager.GetItems(ScriptableItem.ItemType.keyItem);
        equipments = InventoryManager.iManager.GetItems(ScriptableItem.ItemType.equipment);

        var button = GameObject.Find("EquipmentB");
        EventSystem.current.SetSelectedGameObject(button);
        ShowEquipment(button);
    }

    void OnDisable() => CurrentTab = SelectedTab.none;

    private bool IsEquipment => CurrentTab == SelectedTab.equipment;

    public void ToggleTab(GameObject button)
    {
        oldTab.GetComponent<Button>().colors = unselectedColor;
        button.GetComponent<Button>().colors = selectedColor;
        oldTab = button;
    }

    public void ShowEquipment(GameObject button)
    {
        if (!IsEquipment)
        {
            CurrentTab = SelectedTab.equipment;
            ToggleTab(button);
            DisplayItems(equipments);
        }
    }

    public void ShowConsumable(GameObject button)
    {
        if (CurrentTab != SelectedTab.consumable)
        {
            CurrentTab = SelectedTab.consumable;
            ToggleTab(button);
            DisplayItems(consumables);
        }
    }

    public void ShowKeyItem(GameObject button)
    {
        if (CurrentTab != SelectedTab.keyItem)
        {
            CurrentTab = SelectedTab.keyItem;
            ToggleTab(button);
            DisplayItems(keyitems);
        }
    }

    public void WipeSide()
    {
        foreach (Transform child in LeftSide.transform)
            Destroy(child.gameObject);

        foreach (Transform child in RightSide.transform)
            Destroy(child.gameObject);
    }

    private void DisplayItems(List<ScriptableItem> lists)
    {
        foreach (Transform child in LeftSide.transform)
            Destroy(child.gameObject);

        foreach (Transform child in RightSide.transform)
            Destroy(child.gameObject);

        isLeft = true;
        foreach (var item in lists)
            CreateItemButton(item);

        var size = BaseItemHeight * (IsEquipment ? lists.Count : (lists.Count + 1) / 2);
        LeftSide.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);

        if (IsEquipment)
            foreach(ScriptableEquipment.EquipmentType type in Enum.GetValues(typeof(ScriptableEquipment.EquipmentType)))
                CreateEquipmentButton(type);
    }

    private void CreateEquipmentButton(ScriptableEquipment.EquipmentType type)
    {
        GameObject newItem;
        newItem = Instantiate(ItemBase, RightSide.transform);

        if (PermManager.pManager.player.equipments.ContainsKey(type))
        {
            newItem.GetComponent<Button>().onClick.AddListener(() =>
            {
                InventoryManager.iManager.AddItemQuantity(PermManager.pManager.player.equipments[type]);
                PermManager.pManager.player.RemoveEquipment(type);
                equipments = InventoryManager.iManager.GetItems(ScriptableItem.ItemType.equipment);
                DisplayItems(equipments);
                EventSystem.current.SetSelectedGameObject(oldTab);
            });
        }

        newItem.SetActive(true);
        newItem.transform.Find("itemQuantity").GetComponent<Text>().text = "";
        newItem.transform.Find("itemName").GetComponent<Text>().text = type.ToString() +
            (PermManager.pManager.player.equipments.ContainsKey(type) ? " : " + PermManager.pManager.player.equipments[type].itemName : " : ");
    }

    public void CreateItemButton(ScriptableItem item)
    {
        GameObject newItem;
        if (IsEquipment)
        {
            newItem = Instantiate(ItemBase, LeftSide.transform);
            newItem.GetComponent<Button>().onClick.AddListener(() =>
            {
                InventoryManager.iManager.UseItem(item);
                equipments = InventoryManager.iManager.GetItems(ScriptableItem.ItemType.equipment);
                DisplayItems(equipments);

                int itemIndex = Mathf.Clamp(newItem.transform.GetSiblingIndex(), 0, equipments.Count - 1);
                if (equipments.Count > 0)
                    EventSystem.current.SetSelectedGameObject(LeftSide.transform.GetChild(itemIndex).gameObject);
                else
                    EventSystem.current.SetSelectedGameObject(oldTab);
            });
        }
        else
        {
            newItem = Instantiate(ItemBase, isLeft ? LeftSide.transform : RightSide.transform);
            if (CurrentTab == SelectedTab.consumable)
                newItem.GetComponent<Button>().onClick.AddListener(() => ConsumableSelected(newItem, item));
        }

        newItem.SetActive(true);
        newItem.transform.Find("itemName").GetComponent<Text>().text = item.itemName;
        newItem.transform.Find("itemQuantity").GetComponent<Text>().text = "X " + InventoryManager.iManager.GetQuantity(item);

        isLeft = !isLeft;
    }

    private void ConsumableSelected(GameObject itemObject, ScriptableItem item)
    {
        InventoryManager.iManager.UseItem(item);

        if (!InventoryManager.iManager.ItemsExist(item))
        {
            consumables = InventoryManager.iManager.GetItems(ScriptableItem.ItemType.consumable);
            DisplayItems(consumables);

            if(consumables.Count == 0)
                EventSystem.current.SetSelectedGameObject(oldTab);
            else
                EventSystem.current.SetSelectedGameObject(LeftSide.transform.GetChild(0).gameObject);
        }
        else
        {
            itemObject.transform.Find("itemQuantity").GetComponent<Text>().text = "X " + InventoryManager.iManager.GetQuantity(item);
            EventSystem.current.SetSelectedGameObject(itemObject);
        }
    }
}
