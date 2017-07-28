using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {

	public float health = 150f;
	public GameObject projectile;
	public float projectileSpeed;
	public float shotsPerSeconds = 0.5f;
    public int spritePerSeconds = 1;
	public int scoreValue = 150;
	public AudioClip laserSound;
	public AudioClip destroySound;
	public float audioVolume = 0.5f;

    private ScoreKeeper scoreKeeper;
	
	void Start() {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        
    }

	void OnTriggerEnter2D(Collider2D col) {
	
		Projectile projectile = col.gameObject.GetComponent<Projectile>();
		
		if(projectile) {
			health -= projectile.GetDamage();
			projectile.Hit();
			if (health <= 0) {
				Die();
			}
					
		}
	}
	
	void Die() {
		Destroy(gameObject);
		AudioSource.PlayClipAtPoint(destroySound, transform.position, audioVolume);
		scoreKeeper.Score(scoreValue);
	}	
	
	void Fire() {		
		GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;			
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
		
		AudioSource.PlayClipAtPoint(laserSound, transform.position, audioVolume);
		
	}
	
	void Update() {	
		float fireProbability = Time.deltaTime * shotsPerSeconds;
		if (Random.value < fireProbability) {
			Fire ();
		}       
    }   
}
