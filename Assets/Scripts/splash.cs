using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    AudioSource music;

    // Use this for initialization
    void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        music = GetComponent<AudioSource>();
        StartCoroutine(BlinkText());
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Return)) {
            music = GetComponent<AudioSource>();
            music.loop = true;
            music.Play();
            SceneManager.LoadScene("Start Menu");
        }
    }

    public IEnumerator BlinkText() {

        while (true) {

            spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/splash1");
            yield return new WaitForSeconds(.5f);

            spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/splash2"); ;
            yield return new WaitForSeconds(.5f);
        }
    }
}
