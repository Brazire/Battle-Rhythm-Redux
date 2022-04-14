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

	void Awake()
    {
		rControls = new RhythmControls();
		rControls.World.Inventory.performed += ToggleInventory;
		rControls.World.Cancel.performed += CloseCurrent;

		rControls.World.Inventory.Enable();
		rControls.World.Cancel.Enable();
	}

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
