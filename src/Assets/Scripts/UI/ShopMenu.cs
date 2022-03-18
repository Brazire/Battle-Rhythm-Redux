using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject ConfirmMenu;

    [SerializeField]
    private GameObject ItemBase;

    [SerializeField]
    private GameObject Content;

    [SerializeField]
    private GameObject QuantityT;

    [SerializeField]
    private Color SelectedButtonColor;

    private List<ScriptableItem> shopItems;
    private List<ScriptableItem> playerItems;
    private GameObject selectedObject;
    private ScriptableItem selectedItem;

    private GameObject oldTab;
    private ColorBlock selectedColor;
    private ColorBlock unselectedColor;

    private string CURRENCY_NAME = "Gold";

    private SelectedTab CurrentTab = SelectedTab.none;
    private enum SelectedTab
    {
        shop,
        player,
        none
    }

    void Awake()
    {
        shopItems = Resources.LoadAll<ScriptableItem>("").ToList();

        oldTab = GameObject.Find("BuyB");
        unselectedColor = oldTab.GetComponent<Button>().colors;
        selectedColor = oldTab.GetComponent<Button>().colors;
        selectedColor.normalColor = SelectedButtonColor;
    }

    private void OnEnable()
    {
        playerItems = InventoryManager.iManager.GetAllItems(); 
        
        var button = GameObject.Find("BuyB");
        EventSystem.current.SetSelectedGameObject(button);
        ShowShopItem(button);
        GameObject.Find("MoneyT").GetComponent<Text>().text = CURRENCY_NAME + " : " + InventoryManager.iManager.currentMoney;
    }

    private void OnDisable() => CurrentTab = SelectedTab.none;

    private void DisplayItems(List<ScriptableItem> list)
    {
        foreach (Transform child in Content.transform)
            Destroy(child.gameObject);

        foreach (var item in list)
            CreateItemButton(item);

        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
            (ItemBase.GetComponent<RectTransform>().rect.height + Content.GetComponent<VerticalLayoutGroup>().spacing) * list.Count);
    }

    public void CreateItemButton(ScriptableItem item)
    {
        GameObject newItem = Instantiate(ItemBase, Content.transform);

        newItem.GetComponent<Button>().onClick.AddListener(() => {
            selectedItem = item;
            selectedObject = newItem;
            ShowConfirm();
        });

        newItem.SetActive(true);
        newItem.transform.Find("NameT").GetComponent<Text>().text = item.itemName;
        if(CurrentTab == SelectedTab.player)
            newItem.transform.Find("QuantityT").GetComponent<Text>().text = "X " + InventoryManager.iManager.GetQuantity(item);
        else
            newItem.transform.Find("QuantityT").GetComponent<Text>().text = item.buyingPrice + "$";
    }

    public void ShowPlayerItem(GameObject button)
    {
        if (CurrentTab != SelectedTab.player)
        {
            CurrentTab = SelectedTab.player;
            ToggleTab(button);
            DisplayItems(playerItems);
        }
    }

    public void ShowShopItem(GameObject button)
    {
        if (CurrentTab != SelectedTab.shop)
        {
            CurrentTab = SelectedTab.shop;
            ToggleTab(button);
            DisplayItems(shopItems);
        }
    }

    public void ToggleTab(GameObject button)
    {
        oldTab.GetComponent<Button>().colors = unselectedColor;
        button.GetComponent<Button>().colors = selectedColor;
        oldTab = button;
    }

    public void ShowConfirm()
    {
        ConfirmMenu.SetActive(true);
        GetComponent<CanvasGroup>().interactable = false;
        QuantityT.GetComponent<InputField>().text = "1";

        int maxQty = (CurrentTab == SelectedTab.shop) ? 
            InventoryManager.iManager.currentMoney / selectedItem.buyingPrice : InventoryManager.iManager.GetQuantity(selectedItem);
        GameObject.Find("QtyMax").GetComponent<Text>().text = "/ " + maxQty;

        ValidateQuantity();
        EventSystem.current.SetSelectedGameObject(QuantityT);
    }

    public void CloseConfirm()
    {
        GetComponent<CanvasGroup>().interactable = true;
        ConfirmMenu.SetActive(false);

        if (CurrentTab == SelectedTab.player && !playerItems.Contains(selectedItem))
        {
            int itemIndex = selectedObject.transform.GetSiblingIndex() + 1;
            if (itemIndex > playerItems.Count)
                itemIndex -= 2;

            if (playerItems.Count > 0)
                EventSystem.current.SetSelectedGameObject(Content.transform.GetChild(itemIndex).gameObject);
            else
                EventSystem.current.SetSelectedGameObject(oldTab);
        }
        else
            EventSystem.current.SetSelectedGameObject(selectedObject);
    }

    public void ConfirmClicked()
    {
        var qty = int.Parse(QuantityT.GetComponent<InputField>().text);
        if (qty > 0)
        {
            if (CurrentTab == SelectedTab.player)
            {
                if (qty <= InventoryManager.iManager.GetQuantity(selectedItem))
                    SellItem(qty);
            }
            else
                BuyItem(qty);
        }
        else
            Debug.LogError("must buy more than 0 item ;P");
    }

    private void SellItem(int quantity)
    {
        InventoryManager.iManager.currentMoney += selectedItem.sellingPrice * quantity;
        InventoryManager.iManager.AddItemQuantity(selectedItem, -quantity);

        if (InventoryManager.iManager.ItemsExist(selectedItem))
            selectedObject.transform.Find("QuantityT").GetComponent<Text>().text = "X " + InventoryManager.iManager.GetQuantity(selectedItem);
        else
        {
            Destroy(selectedObject);
            playerItems.Remove(selectedItem);
        }

        CloseConfirm();
        GameObject.Find("MoneyT").GetComponent<Text>().text = CURRENCY_NAME + " : " + InventoryManager.iManager.currentMoney;
    }

    private void BuyItem(int quantity)
    {
        var cost = selectedItem.buyingPrice * quantity;
        if (cost <= InventoryManager.iManager.currentMoney)
        {
            InventoryManager.iManager.currentMoney -= cost;
            InventoryManager.iManager.AddItemQuantity(selectedItem, quantity);
            playerItems = InventoryManager.iManager.GetAllItems();
            CloseConfirm();
            GameObject.Find("MoneyT").GetComponent<Text>().text = CURRENCY_NAME + " : " + InventoryManager.iManager.currentMoney;
        }
    }

    public void ValidateQuantity()
    {
        if (CurrentTab == SelectedTab.player)
        {
            var qty = Mathf.Clamp(int.Parse(QuantityT.GetComponent<InputField>().text), 0, InventoryManager.iManager.GetQuantity(selectedItem));

            QuantityT.GetComponent<InputField>().text = qty.ToString();
            GameObject.Find("TransactionT").GetComponent<Text>().text = "Sell for : " + qty * selectedItem.sellingPrice;
        }
        else
        {
            var max = InventoryManager.iManager.currentMoney / selectedItem.buyingPrice;
            var qty = Mathf.Clamp(int.Parse(QuantityT.GetComponent<InputField>().text), 0, max);

            QuantityT.GetComponent<InputField>().text = qty.ToString();
            GameObject.Find("TransactionT").GetComponent<Text>().text = "Spend : " + qty * selectedItem.buyingPrice;
        }
    }
}
