using System;
using System.Collections;
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
        consumables = PermManager.pManager.player.GetItems(ScriptableItem.ItemType.consumable);
        keyitems = PermManager.pManager.player.GetItems(ScriptableItem.ItemType.keyItem);
        equipments = PermManager.pManager.player.GetItems(ScriptableItem.ItemType.equipment);

        var button = GameObject.Find("EquipmentB");
        EventSystem.current.SetSelectedGameObject(button);
        ShowEquipment(button);
    }

    void OnDisable() => CurrentTab = SelectedTab.none;

    private bool IsEquipment => CurrentTab == SelectedTab.equipment;

    /// <summary>
    /// Change the color of the oldTab and the current tab to visually see which tab is currently selected.
    /// </summary>
    /// <param name="button">The gameObject that was clicked that will became the oldTab.</param>
    public void ToggleTab(GameObject button)
    {
        oldTab.GetComponent<Button>().colors = unselectedColor;
        button.GetComponent<Button>().colors = selectedColor;
        oldTab = button;
    }

    /// <summary>
    /// Method called when the Equipment tab is selected. Change the selected tab and display the list of equipment.
    /// </summary>
    /// <param name="button">The gameObject that was clicked.</param>
    public void ShowEquipment(GameObject button)
    {
        if (!IsEquipment)
        {
            CurrentTab = SelectedTab.equipment;
            ToggleTab(button);
            DisplayItems(equipments);
        }
    }

    /// <summary>
    /// Method called when the Consumable tab is selected. Change the selected tab and display the list of consumable items.
    /// </summary>
    /// <param name="button">The gameObject that was clicked.</param>
    public void ShowConsumable(GameObject button)
    {
        if (CurrentTab != SelectedTab.consumable)
        {
            CurrentTab = SelectedTab.consumable;
            ToggleTab(button);
            DisplayItems(consumables);
        }
    }

    /// <summary>
    /// Method called when the Key item tab is selected. Change the selected tab and display the list of key item.
    /// </summary>
    /// <param name="button">The gameObject that was clicked.</param>
    public void ShowKeyItem(GameObject button)
    {
        if (CurrentTab != SelectedTab.keyItem)
        {
            CurrentTab = SelectedTab.keyItem;
            ToggleTab(button);
            DisplayItems(keyitems);
        }
    }

    /// <summary>
    /// Method that remove the old gameObject still displayed and create new gameObject for the items. If 
    /// the tab is equipment, it also add button for each type of equipment. The height of content is resized to allow scrolling.
    /// </summary>
    /// <param name="lists">The list of selectableItem to display.</param>
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

    /// <summary>
    /// Method that create the gameObject for each type of equipment, selecting it will remove the equipment assigned there.
    /// </summary>
    /// <param name="type">The list of all type of equipment.</param>
    private void CreateEquipmentButton(ScriptableEquipment.EquipmentType type)
    {
        GameObject newItem;
        newItem = Instantiate(ItemBase, RightSide.transform);

        if (PermManager.pManager.player.equipments.ContainsKey(type))
        {
            newItem.GetComponent<Button>().onClick.AddListener(() =>
            {
                PermManager.pManager.player.AddItemQuantity(PermManager.pManager.player.equipments[type]);
                PermManager.pManager.player.RemoveEquipment(type);
                equipments = PermManager.pManager.player.GetItems(ScriptableItem.ItemType.equipment);
                DisplayItems(equipments);
                EventSystem.current.SetSelectedGameObject(oldTab);
            });
        }

        newItem.SetActive(true);
        newItem.transform.Find("itemQuantity").GetComponent<Text>().text = "";
        newItem.transform.Find("itemName").GetComponent<Text>().text = type.ToString() +
            (PermManager.pManager.player.equipments.ContainsKey(type) ? " : " + PermManager.pManager.player.equipments[type].itemName : " : ");
    }

    /// <summary>
    /// Method that create a button of a ScriptableItem, if it's an equipment, it will get equipped and a consumable will be consumed!!
    /// </summary>
    /// <param name="item">The ScriptableItem to display.</param>
    public void CreateItemButton(ScriptableItem item)
    {
        GameObject newItem;
        if (IsEquipment)
        {
            newItem = Instantiate(ItemBase, LeftSide.transform);
            newItem.GetComponent<Button>().onClick.AddListener(() =>
            {
                PermManager.pManager.player.UseItem(item);
                equipments = PermManager.pManager.player.GetItems(ScriptableItem.ItemType.equipment);
                DisplayItems(equipments);

                int itemIndex = Mathf.Clamp(newItem.transform.GetSiblingIndex(), 0, equipments.Count - 1);
                if (equipments.Count > 0)
                    StartCoroutine(SelectItemAfterReload(itemIndex));
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
        newItem.transform.Find("itemQuantity").GetComponent<Text>().text = "X " + PermManager.pManager.player.GetQuantity(item);

        isLeft = !isLeft;
    }

    /// <summary>
    /// Coroutine that wait the end of frame before selecting a gameObject, since it was keeping not yet destroyed gameObject.
    /// </summary>
    /// <param name="index">The index of the item to select.</param>
    private IEnumerator SelectItemAfterReload(int index)
    {
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(LeftSide.transform.GetChild(index).gameObject);
    }

    /// <summary>
    /// Method called when a consumable item is clicked. It will call the UseItem method from ScriptableObject and 
    /// then remove 1 from the quantity or reload the items.
    /// </summary>
    /// <param name="itemObject">The gameObject of the item that was clicked</param>
    /// <param name="item"></param>
    private void ConsumableSelected(GameObject itemObject, ScriptableItem item)
    {
        PermManager.pManager.player.UseItem(item);

        if (!PermManager.pManager.player.ItemsExist(item))
        {
            consumables = PermManager.pManager.player.GetItems(ScriptableItem.ItemType.consumable);
            DisplayItems(consumables);

            if (consumables.Count == 0)
                EventSystem.current.SetSelectedGameObject(oldTab);
            else
                StartCoroutine(SelectItemAfterReload(0));
        }
        else
        {
            itemObject.transform.Find("itemQuantity").GetComponent<Text>().text = "X " + PermManager.pManager.player.GetQuantity(item);
            EventSystem.current.SetSelectedGameObject(itemObject);
        }
    }
}
