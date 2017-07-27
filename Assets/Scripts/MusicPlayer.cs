using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource music;
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += this.OnLevelFinishedLoading;
            music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.Play();
		}		
	}
	
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode sceneMode) {
		Debug.Log("MusicPlayer Loaded level " + scene.buildIndex);
		music.Stop();
		
		switch(scene.buildIndex) {
			case 0:
				music.clip = startClip;
				break;
			case 1:
				music.clip = gameClip;
				break;
			case 2:
				music.clip = endClip;
				break;
			default:
				music.clip = startClip;
				break;
		}
		music.loop = true;
		music.Play();
	}
}
