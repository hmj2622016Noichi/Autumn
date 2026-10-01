using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
	//移動
	float moveSpeed = 5f;
	float jumpPower = 7f;

	public GameObject pickaxePrefab;
	public GameObject ePrefab;

	public Transform shootPoint;
	public Transform eShootPoint;
	//つるはし.スコップの速さ
	float shootSpeed = 25f;
	float eShootSpeed = 20f;
	//おと
	public AudioSource audioSource;
	public AudioClip pickaxeSE;
	public AudioClip shovelSE;

	Rigidbody rb;
	bool isGrounded;
	bool shovelActive;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		if (audioSource == null)
		{
			audioSource = GetComponent<AudioSource>();
		}
	}
	//移動
	void Update()
	{
		float x = 0;
		float z = 0;

		if (Keyboard.current.sKey.isPressed) x = -1;
		if (Keyboard.current.wKey.isPressed) x = 1;
		if (Keyboard.current.dKey.isPressed) z = -1;
		if (Keyboard.current.aKey.isPressed) z = 1;

		Vector3 move = new Vector3(x, 0, z).normalized;
		transform.position += move * moveSpeed * Time.deltaTime;

		if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
		{
			rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
			isGrounded = false;
		}
		//つるはし
		if (Mouse.current.leftButton.wasPressedThisFrame)
		{
			if (pickaxePrefab != null && shootPoint != null)
			{
				GameObject pickaxe = Instantiate(pickaxePrefab, shootPoint.position, shootPoint.rotation);
				Rigidbody pickaxeRb = pickaxe.GetComponent<Rigidbody>();

				if (pickaxeRb != null)
				{
					pickaxeRb.linearVelocity = shootPoint.forward * shootSpeed;
				}

				if (audioSource != null && pickaxeSE != null)
				{
					audioSource.PlayOneShot(pickaxeSE);
				}

				Destroy(pickaxe, 10f);
			}
		}
		//スコップ
		if (Keyboard.current.eKey.wasPressedThisFrame && !shovelActive)
		{
			if (ePrefab != null && eShootPoint != null)
			{
				shovelActive = true;

				GameObject shovel1 = Instantiate(ePrefab, eShootPoint.position, eShootPoint.rotation);
				Rigidbody eRb = shovel1.GetComponent<Rigidbody>();

				if (eRb != null)
				{
					eRb.linearVelocity = eShootPoint.forward * eShootSpeed;
				}

				if (audioSource != null && shovelSE != null)
				{
					audioSource.PlayOneShot(shovelSE);
				}

				Destroy(shovel1, 7f);
				Invoke(nameof(ResetShovel), 7f);
			}
		}
	}
	//スコップは１つまで
	void ResetShovel()
	{
		shovelActive = false;
	}
	//いらんかった
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isGrounded = true;
		}
	}
}