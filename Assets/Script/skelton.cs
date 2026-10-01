using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnPickaxe : MonoBehaviour
{
	Vector3 spawnPosition;
	Quaternion spawnRotation;

	void Start()
	{
		spawnPosition = transform.position;
		spawnRotation = transform.rotation;
	}

	private void OnCollisionEnter(Collision collision)
	{
		//つるはしに当たると削除後再スポーン
		if (collision.gameObject.CompareTag("Pickaxe"))
		{
			gameObject.SetActive(false);
			ScoreManager.instance.AddScore(1);
			Invoke(nameof(Respawn), 10f);
		}
		//しゃべるに当たると削除後再スポーン
		if (collision.gameObject.CompareTag("Shovel"))
		{
			gameObject.SetActive(false);
			ScoreManager.instance.AddScore(1);
			Invoke(nameof(Respawn), 10f);
		}
	}
	//再スポーン
	void Respawn()
	{
		transform.position = spawnPosition;
		transform.rotation = spawnRotation;
		gameObject.SetActive(true);
	}
}