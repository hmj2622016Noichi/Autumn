using UnityEngine;
using UnityEngine.InputSystem;

public class SkeletonSpawner : MonoBehaviour
{
	public GameObject skeletonPrefab;
	public float moveSpeed = 5f;

	void Update()
	{
		if (Keyboard.current.eKey.wasPressedThisFrame)
		{
			Vector3 spawnPosition = new Vector3(12f, 2f, 9f);
			GameObject skeleton = Instantiate(skeletonPrefab, spawnPosition, transform.rotation);
			skeleton.AddComponent<SkeletonMove>().moveSpeed = moveSpeed;
		}
	}
}

public class SkeletonMove : MonoBehaviour
{
	public float moveSpeed;

	void Update()
	{
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Pickaxe"))
		{
			Destroy(gameObject);
		}
	}
}