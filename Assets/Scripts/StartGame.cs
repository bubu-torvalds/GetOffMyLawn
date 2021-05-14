using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    AudioSource music;

    // Use this for initialization
    void Start () {
        music = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return)) {
            music = GetComponent<AudioSource>();
            music.Play();
            SceneManager.LoadScene("Game");
        }
    }
}
