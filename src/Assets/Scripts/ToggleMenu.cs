using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject Inv;

	[SerializeField]
	private GameObject Shop;

	private bool isInvActive = false;
	private bool isShopActive = false;
	private RhythmControls rControls;

	void Awake()
    {
		rControls = new RhythmControls();
		rControls.World.Inventory.performed += ToggleInventory;
		rControls.World.Shop.performed += ToggleShop;

		rControls.World.Inventory.Enable();
		rControls.World.Shop.Enable();
	}


	private void ToggleInventory(InputAction.CallbackContext obj)
    {
		if (!isShopActive)
		{
			isInvActive = !isInvActive;
			Inv.SetActive(isInvActive);
			GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = !isInvActive;
		}
	}

	private void ToggleShop(InputAction.CallbackContext obj)
	{
		if (!isInvActive)
		{
			isShopActive = !isShopActive;
			Shop.SetActive(isShopActive);
			GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = !isShopActive;
		}
	}
}
