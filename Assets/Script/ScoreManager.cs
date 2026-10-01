using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance;

	public int score = 0;
	public TextMeshProUGUI scoreText;
	//ÉXÉRÉA
	void Awake()
	{
		instance = this;
	}

	public void AddScore(int amount)
	{
		score += amount;
		scoreText.text = "Score : " + score;
	}
}