using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
	
	[SerializeField]
	private float speed;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private Text hpText;
	private RhythmControls rControls;
	private float newVelocityX = 0f;
	private float newVelocityY = 0f;

	void Awake () {
		Debug.Log("Starting movement");
		PermManager.pManager.PlacePlayerBack();
		PermManager.pManager.UpdatePlayerHealth();
		rControls = new RhythmControls();
		rControls.World.Horizontal.performed += LeftRightPressed;
		rControls.World.Vertical.performed += UpDownPressed;

		rControls.World.Horizontal.canceled += LeftRightCancel;
		rControls.World.Vertical.canceled += UpDownCancel;

		EnableWalking();
	}

	void OnDestroy() => DisableWalking();

    public void EnableWalking()
	{
		rControls.World.Horizontal.Enable();
		rControls.World.Vertical.Enable();
	}

	public void DisableWalking()
	{
		rControls.World.Horizontal.Disable();
		rControls.World.Vertical.Disable();
		animator.SetInteger("DirectionX", 0);
		animator.SetInteger("DirectionY", 0);
		newVelocityX = 0;
		newVelocityY = 0;
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(newVelocityX, newVelocityY);
	}

	public void UpdateHPText(float amount, float max)
    {
		hpText.text = amount + " / " + max;
    }

	public void MakePlayerMove(Vector3 direction)
    {
		animator.SetInteger("DirectionX", (int)direction.x);
		animator.SetInteger("DirectionY", (int)direction.y);
	}

	public void UpDownCancel(InputAction.CallbackContext obj)
	{
		animator.SetInteger("DirectionY", 0);
		newVelocityY = 0;
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(newVelocityX, newVelocityY);
	}

	public void LeftRightCancel(InputAction.CallbackContext obj)
	{
		animator.SetInteger("DirectionX", 0);
		newVelocityX = 0;
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(newVelocityX, newVelocityY);
	}

	public void UpDownPressed(InputAction.CallbackContext obj)
	{
		float moveVertical = rControls.World.Vertical.ReadValue<float>();
		Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
		if (moveVertical < 0 && currentVelocity.y <= 0)
		{
			newVelocityY = -speed - 150;
			animator.SetInteger("DirectionY", -1);
		}
		else if (moveVertical > 0 && currentVelocity.y >= 0)
		{
			newVelocityY = speed + 150;
			animator.SetInteger("DirectionY", 1);
		}
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(newVelocityX, newVelocityY);
	}

	public void LeftRightPressed(InputAction.CallbackContext obj)
	{
		float moveHorizontal = rControls.World.Horizontal.ReadValue<float>();
		Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
		if (moveHorizontal < 0 && currentVelocity.x <= 0)
		{
			newVelocityX = -speed - 150;
			animator.SetInteger("DirectionX", -1);
		}
		else if (moveHorizontal > 0 && currentVelocity.x >= 0)
		{
			newVelocityX = speed + 150;
			animator.SetInteger("DirectionX", 1);
		}
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(newVelocityX, newVelocityY);
	}
}
