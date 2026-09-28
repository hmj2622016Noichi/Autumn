using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float jumpPower = 7f;

	public GameObject pickaxePrefab;
	public Transform shootPoint;
	public float shootSpeed = 20f;

	Rigidbody rb;
	bool isGrounded;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		float x = 0;
		float z = 0;

		if (Keyboard.current.aKey.isPressed) x = -1;
		if (Keyboard.current.dKey.isPressed) x = 1;
		if (Keyboard.current.sKey.isPressed) z = -1;
		if (Keyboard.current.wKey.isPressed) z = 1;

		Vector3 move = new Vector3(x, 0, z).normalized;
		transform.position += move * moveSpeed * Time.deltaTime;

		if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
		{
			rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
			isGrounded = false;
		}

		if (Mouse.current.leftButton.wasPressedThisFrame)
		{
			GameObject pickaxe = Instantiate(pickaxePrefab, shootPoint.position, shootPoint.rotation);

			Rigidbody pickaxeRb = pickaxe.GetComponent<Rigidbody>();

			if (pickaxeRb != null)
			{
				pickaxeRb.linearVelocity = shootPoint.forward * shootSpeed;
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isGrounded = true;
		}
	}
}