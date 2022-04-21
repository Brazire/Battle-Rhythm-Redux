using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject Inv;

	[SerializeField]
	private GameObject Shop;

	[SerializeField]
	private GameObject Dialog;

	private bool isInvActive = false;
	private bool isShopActive = false;
	private bool isDialogActive = false;
	private RhythmControls rControls;

	/// <summary>
	/// Awake method that set and enable method on controls.
	/// </summary>
	void Awake()
    {
		rControls = new RhythmControls();
		rControls.World.Inventory.performed += ToggleInventory;
		rControls.World.Cancel.performed += CloseCurrent;

		rControls.World.Inventory.Enable();
		rControls.World.Cancel.Enable();
	}

    void OnDestroy()
	{
		rControls.World.Inventory.Disable();
		rControls.World.Cancel.Disable();
	}

	/// <summary>
	/// Method that close the current menu displayed and give back the controls to the player avatar. 
	/// If the Confirm menu from the shop is displayed, only close that.
	/// </summary>
	/// <param name="obj">The default Callback object</param>
    private void CloseCurrent(InputAction.CallbackContext obj)
    {
        if (isInvActive)
        {
			isInvActive = false;
			Inv.SetActive(isInvActive);
			if (isInvActive)
				GameObject.Find("Player").GetComponent<PlayerMovement>().DisableWalking();
			else
				GameObject.Find("Player").GetComponent<PlayerMovement>().EnableWalking();
		}
		else if (isShopActive)
        {
			if (Shop.GetComponent<ShopMenu>().IsConfirmMenuEnabled())
				Shop.GetComponent<ShopMenu>().CloseConfirm();
            else
            {
				isShopActive = false;
				Shop.SetActive(isShopActive);
				if (isShopActive)
					GameObject.Find("Player").GetComponent<PlayerMovement>().DisableWalking();
				else
					GameObject.Find("Player").GetComponent<PlayerMovement>().EnableWalking();
			}
		}
    }

	/// <summary>
	/// Method to enable or disable the Inventory if no other menu is currently enabled. It also prevent the player avatar to walk while active.
	/// </summary>
	/// <param name="obj">The default Callback object</param>
	private void ToggleInventory(InputAction.CallbackContext obj)
    {
		if (!isShopActive && !isDialogActive)
		{
			isInvActive = !isInvActive;
			Inv.SetActive(isInvActive);
			if (isInvActive)
				GameObject.Find("Player").GetComponent<PlayerMovement>().DisableWalking();
			else
				GameObject.Find("Player").GetComponent<PlayerMovement>().EnableWalking();
		}
	}

	public bool IsDialogPossible => (!isShopActive && !isInvActive);

	/// <summary>
	/// Method to enable or disable the DialogBox if no other menu is currently enabled. It also prevent the player avatar to walk while active.
	/// </summary>
	/// <param name="v">this is a bool that say to set it to active or not.</param>
	public void ToggleDialog(bool v)
	{
		if (!isShopActive && !isInvActive)
		{
			isDialogActive = v;
			Dialog.SetActive(isDialogActive);
			if (isDialogActive)
				GameObject.Find("Player").GetComponent<PlayerMovement>().DisableWalking();
			else
				GameObject.Find("Player").GetComponent<PlayerMovement>().EnableWalking();
		}
	}

	/// <summary>
	/// Method to enable or disable the shop if no other menu is currently enabled. It also prevent the player avatar to walk while active.
	/// </summary>
	public void OpenShop()
	{
		if (!isInvActive && !isDialogActive)
		{
			isShopActive = true;
			Shop.SetActive(isShopActive);
			if (isShopActive)
				GameObject.Find("Player").GetComponent<PlayerMovement>().DisableWalking();
			else
				GameObject.Find("Player").GetComponent<PlayerMovement>().EnableWalking();
		}
	}

	/// <summary>
	/// I think this method is not used anymore. It used to open the shop.
	/// </summary>
	/// <param name="obj">The default Callback object</param>
	private void ToggleShop(InputAction.CallbackContext obj)
	{
		if (!isInvActive && !isDialogActive)
		{
			isShopActive = !isShopActive;
			Shop.SetActive(isShopActive);
			if (isShopActive)
				GameObject.Find("Player").GetComponent<PlayerMovement>().DisableWalking();
			else
				GameObject.Find("Player").GetComponent<PlayerMovement>().EnableWalking();
		}
	}
}
