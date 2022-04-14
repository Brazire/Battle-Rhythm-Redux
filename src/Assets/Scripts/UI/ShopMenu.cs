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
        shopItems = PermManager.pManager.GetAllShopItems();

        oldTab = GameObject.Find("BuyB");
        unselectedColor = oldTab.GetComponent<Button>().colors;
        selectedColor = oldTab.GetComponent<Button>().colors;
        selectedColor.normalColor = SelectedButtonColor;
    }

    private void OnEnable()
    {
        playerItems = PermManager.pManager.player.GetAllItems(); 
        
        var button = GameObject.Find("BuyB");
        EventSystem.current.SetSelectedGameObject(button);
        ShowShopItem(button);
        GameObject.Find("MoneyT").GetComponent<Text>().text = CURRENCY_NAME + " : " + PermManager.pManager.player.currentMoney;
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
            newItem.transform.Find("QuantityT").GetComponent<Text>().text = "X " + PermManager.pManager.player.GetQuantity(item);
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
            PermManager.pManager.player.currentMoney / selectedItem.buyingPrice : PermManager.pManager.player.GetQuantity(selectedItem);
        GameObject.Find("QtyMax").GetComponent<Text>().text = "/ " + maxQty;

        ValidateQuantity();
        EventSystem.current.SetSelectedGameObject(QuantityT);
    }

    public bool IsConfirmMenuEnabled() => ConfirmMenu.activeSelf;

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
                if (qty <= PermManager.pManager.player.GetQuantity(selectedItem))
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
        PermManager.pManager.player.currentMoney += selectedItem.sellingPrice * quantity;
        PermManager.pManager.player.AddItemQuantity(selectedItem, -quantity);

        if (PermManager.pManager.player.ItemsExist(selectedItem))
            selectedObject.transform.Find("QuantityT").GetComponent<Text>().text = "X " + PermManager.pManager.player.GetQuantity(selectedItem);
        else
        {
            Destroy(selectedObject);
            playerItems.Remove(selectedItem);
        }

        CloseConfirm();
        GameObject.Find("MoneyT").GetComponent<Text>().text = CURRENCY_NAME + " : " + PermManager.pManager.player.currentMoney;
    }

    private void BuyItem(int quantity)
    {
        var cost = selectedItem.buyingPrice * quantity;
        if (cost <= PermManager.pManager.player.currentMoney)
        {
            PermManager.pManager.player.currentMoney -= cost;
            PermManager.pManager.player.AddItemQuantity(selectedItem, quantity);
            playerItems = PermManager.pManager.player.GetAllItems();
            CloseConfirm();
            GameObject.Find("MoneyT").GetComponent<Text>().text = CURRENCY_NAME + " : " + PermManager.pManager.player.currentMoney;
        }
    }

    public void ValidateQuantity()
    {
        if (CurrentTab == SelectedTab.player)
        {
            var qty = Mathf.Clamp(int.Parse(QuantityT.GetComponent<InputField>().text), 0, PermManager.pManager.player.GetQuantity(selectedItem));

            QuantityT.GetComponent<InputField>().text = qty.ToString();
            GameObject.Find("TransactionT").GetComponent<Text>().text = "Sell for : " + qty * selectedItem.sellingPrice;
        }
        else
        {
            var max = PermManager.pManager.player.currentMoney / selectedItem.buyingPrice;
            var qty = Mathf.Clamp(int.Parse(QuantityT.GetComponent<InputField>().text), 0, max);

            QuantityT.GetComponent<InputField>().text = qty.ToString();
            GameObject.Find("TransactionT").GetComponent<Text>().text = "Spend : " + qty * selectedItem.buyingPrice;
        }
    }
}
