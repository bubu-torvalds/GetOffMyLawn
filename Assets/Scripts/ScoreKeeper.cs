using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 0;
	public AudioClip scoreSound;
	public float audioVolume = 0.2f;
	private Text scoreText;	
	
	void Start() {
		scoreText = GetComponent<Text>();
		Reset();
	}

	public void Score(int points) {
		score += points;	
		scoreText.text = score.ToString();
		AudioSource.PlayClipAtPoint(scoreSound, transform.position, audioVolume);
	}
	
	public static void Reset() {
		score = 0;
	}
	
}
