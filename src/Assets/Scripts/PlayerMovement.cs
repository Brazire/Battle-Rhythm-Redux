using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	[SerializeField]
	private float speed;

	[SerializeField]
	private Animator animator;
	private RhythmControls rControls;

	void Awake () {
		Debug.Log("Starting movement");
		rControls = new RhythmControls();

		rControls.World.Horizontal.Enable();
		rControls.World.Vertical.Enable();
	}
	
	void FixedUpdate () {

		// Set lastest player position

		float moveHorizontal = rControls.World.Horizontal.ReadValue<float>();
		float moveVertical = rControls.World.Vertical.ReadValue<float>();

		Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D> ().velocity;

		float newVelocityX = 0f;
		if (moveHorizontal < 0 && currentVelocity.x <= 0) {
			newVelocityX = -speed - 150;
			animator.SetInteger ("DirectionX", -1);
		} else if (moveHorizontal > 0 && currentVelocity.x >= 0) {
			newVelocityX = speed + 150;
			animator.SetInteger ("DirectionX", 1);
		} else {
			animator.SetInteger ("DirectionX", 0);
		}

		float newVelocityY = 0f;
		if (moveVertical < 0 && currentVelocity.y <= 0) {
			newVelocityY = -speed - 150;
			animator.SetInteger ("DirectionY", -1);
		} else if (moveVertical > 0 && currentVelocity.y >= 0) {
			newVelocityY = speed + 150;
			animator.SetInteger ("DirectionY", 1);
		} else {
			animator.SetInteger ("DirectionY", 0);
		}

		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (newVelocityX, newVelocityY);
	}
}
