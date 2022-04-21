using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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
    private RhythmControls rControls;
    private bool isHolded = false;

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

    /// <summary>
    /// Awake get the list of items to sell, set the control for the gamepad, 
    /// set the default Tab to Buy and set the colors for the selected and unselected tab.
    /// </summary>
    void Awake()
    {
        shopItems = PermManager.pManager.GetAllShopItems();
        rControls = new RhythmControls();

        rControls.World.LeftShoulder.started += LeftDecrement;
        rControls.World.LeftShoulder.performed += HoldLeft;
        rControls.World.LeftShoulder.canceled += StopHolding;

        rControls.World.RightShoulder.started += RightIncrement;
        rControls.World.RightShoulder.performed += HoldRight;
        rControls.World.RightShoulder.canceled += StopHolding;

        rControls.World.Validate.performed += GoToConfirm;

        oldTab = GameObject.Find("BuyB");
        unselectedColor = oldTab.GetComponent<Button>().colors;
        selectedColor = oldTab.GetComponent<Button>().colors;
        selectedColor.normalColor = SelectedButtonColor;
    }

    /// <summary>
    /// Method for the gamepad, allow to press Confirm
    /// </summary>
    /// <param name="obj">The default Callback object</param>
    private void GoToConfirm(InputAction.CallbackContext obj)
    {
        if (EventSystem.current.currentSelectedGameObject == QuantityT)
            EventSystem.current.SetSelectedGameObject(GameObject.Find("ConfirmB"));
    }

    /// <summary>
    /// Method for the gamepad, allow to buy or sell 1 less.
    /// </summary>
    /// <param name="obj">The default Callback object</param>
    private void LeftDecrement(InputAction.CallbackContext obj)
    {
        int qty = int.Parse(QuantityT.GetComponent<InputField>().text) - 1;
        QuantityT.GetComponent<InputField>().text = qty.ToString();
        ValidateQuantity();
    }

    /// <summary>
    /// Method for the gamepad, allow to buy or sell 1 more.
    /// </summary>
    /// <param name="obj">The default Callback object</param>
    private void RightIncrement(InputAction.CallbackContext obj)
    {
        int qty = int.Parse(QuantityT.GetComponent<InputField>().text) + 1;
        QuantityT.GetComponent<InputField>().text = qty.ToString();
        ValidateQuantity();
    }

    /// <summary>
    /// Method for the gamepad, allow to buy or sell 1 less while the button is pressed.
    /// </summary>
    /// <param name="obj">The default Callback object</param>
    private void HoldLeft(InputAction.CallbackContext obj)
    {
        if (!isHolded)
        {
            isHolded = true;
            StartCoroutine(IncrementOnHold(-1));
        }
    }

    /// <summary>
    /// Method for the gamepad, allow to buy or sell 1 more while the button is pressed.
    /// </summary>
    /// <param name="obj">The default Callback object</param>
    private void HoldRight(InputAction.CallbackContext obj)
    {
        if (!isHolded)
        {
            isHolded = true;
            StartCoroutine(IncrementOnHold(1));
        }
    }

    /// <summary>
    /// Method called when the button is released
    /// </summary>
    /// <param name="obj">The default Callback object</param>
    private void StopHolding(InputAction.CallbackContext obj) => isHolded = false;

    /// <summary>
    /// Coroutine that add or remove a value while a button is pressed.
    /// </summary>
    /// <param name="value">The value to add or remove.</param>
    /// <returns></returns>
    IEnumerator IncrementOnHold(int value)
    {
        while (isHolded)
        {
            int qty = int.Parse(QuantityT.GetComponent<InputField>().text) + value;
            QuantityT.GetComponent<InputField>().text = qty.ToString();
            ValidateQuantity();
            yield return new WaitForSeconds(0.2f);
        }
    }

    /// <summary>
    /// Get the items list of the player, set the menu on the Buy tab and set how much money the player currently have.
    /// </summary>
    private void OnEnable()
    {
        playerItems = PermManager.pManager.player.GetAllItems(); 
        
        var button = GameObject.Find("BuyB");
        EventSystem.current.SetSelectedGameObject(button);
        ShowShopItem(button);
        GameObject.Find("MoneyT").GetComponent<Text>().text = CURRENCY_NAME + " : " + PermManager.pManager.player.currentMoney;
    }

    /// <summary>
    /// Set the current tab to none when the script get disabled.
    /// </summary>
    private void OnDisable() => CurrentTab = SelectedTab.none;

    /// <summary>
    /// Remove the old gameObject, create new gameObject with the list and set the height of the Content to allow scrolling.
    /// </summary>
    /// <param name="list">The list of items.</param>
    private void DisplayItems(List<ScriptableItem> list)
    {
        foreach (Transform child in Content.transform)
            Destroy(child.gameObject);

        foreach (var item in list)
            CreateItemButton(item);

        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
            (ItemBase.GetComponent<RectTransform>().rect.height + Content.GetComponent<VerticalLayoutGroup>().spacing) * list.Count);
    }

    /// <summary>
    /// Method that create a new button for the item and add it to the Content.
    /// </summary>
    /// <param name="item">The scriptableItem to create a button with.</param>
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

    /// <summary>
    /// Method called when the Sell button is pressed. Set the current tab to player and display the player items to sell.
    /// </summary>
    /// <param name="button">The gameObject that was clicked.</param>
    public void ShowPlayerItem(GameObject button)
    {
        if (CurrentTab != SelectedTab.player)
        {
            CurrentTab = SelectedTab.player;
            ToggleTab(button);
            DisplayItems(playerItems);
        }
    }

    /// <summary>
    /// Method called when the Buy button is pressed. Set the current tab to shop and display the shop items to buy.
    /// </summary>
    /// <param name="button">The gameObject that was clicked.</param>
    public void ShowShopItem(GameObject button)
    {
        if (CurrentTab != SelectedTab.shop)
        {
            CurrentTab = SelectedTab.shop;
            ToggleTab(button);
            DisplayItems(shopItems);
        }
    }

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
    /// Method that disable the list of items, display a Confirm popup and allow the player to choose 
    /// the quantity of items to buy or sell. Also display the maximum of items the player can sell or buy.
    /// </summary>
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
        rControls.World.LeftShoulder.Enable();
        rControls.World.RightShoulder.Enable();
        rControls.World.Validate.Enable();
    }

    /// <summary>
    /// Method used by ToggleMenu to know if the ConfirmMenu is currently displayed or not to know what to close.
    /// </summary>
    /// <returns>Return a bool to know if the Confirm popup is displayed or not.</returns>
    public bool IsConfirmMenuEnabled() => ConfirmMenu.activeSelf;

    /// <summary>
    /// Method that close the Confirm popup and select the right items.
    /// </summary>
    public void CloseConfirm()
    {
        rControls.World.LeftShoulder.Disable();
        rControls.World.RightShoulder.Disable();
        rControls.World.Validate.Disable();
        GetComponent<CanvasGroup>().interactable = true;

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

        ConfirmMenu.SetActive(false);
    }

    /// <summary>
    /// Method called when the Confirm button in the Confirm popup is pressed. 
    /// Make sure there is actually a quantity selected and then redirect to buy or sell items.
    /// </summary>
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

    /// <summary>
    /// Method that sell items to the shop. Add the money to the player and remove the quantity of items.
    /// </summary>
    /// <param name="quantity">The quantity of items to sell</param>
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

    /// <summary>
    /// Method that buy item from the shop. Remove the cost from the player and add the quantity to the inventory.
    /// </summary>
    /// <param name="quantity">The quantity of items to buy</param>
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

    /// <summary>
    /// Method called everytime the quantity is updated. Make sure the quantity is a quantity the player can afford and change it if not.
    /// </summary>
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
