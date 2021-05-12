using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMusic : MonoBehaviour {

    private AudioSource music;

    // Use this for initialization
    void Start () {
        music = GetComponent<AudioSource>();
        music.loop = true;
        music.Play();
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
