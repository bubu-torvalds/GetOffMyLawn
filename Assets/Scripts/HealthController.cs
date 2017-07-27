using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

    private float health;
    private Text healthText;

	// Use this for initialization
	void Start () {
        healthText = GetComponent<Text>();
        PlayerController player = GameObject.FindObjectOfType<PlayerController>();
        health = player.health;
        healthText.text = health.ToString();
    }

    public void Damage(float damage) {
        health -= damage;
        healthText.text = health.ToString();
    }

    public void Reset(float newHealth) {
        health = newHealth;
        healthText.text = health.ToString();
    }
}
