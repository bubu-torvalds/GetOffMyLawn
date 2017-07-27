using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float shipSpeed = 15f;
    public float padding = 1;
    public GameObject projectile;
    public float projectileSpeed;
    public float firingRate = 0.2f;
    public float health = 250f;
    public AudioClip projectileSound;
    public AudioClip dyingSound;
    public float audioVolume = 0.5f;
    public Sprite[] playerSprites;

    private HealthController healthController;
    private float minPosX;
	private float maxPosX;
	

	// Use this for initialization
	void Start () {	
		DetectGameSpace();
        healthController = GameObject.Find("Health").GetComponent<HealthController>();
    }
	
	void Fire() {
        GameObject projectile = Instantiate(this.projectile, transform.position, Quaternion.identity) as GameObject;		
		projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);		
		AudioSource.PlayClipAtPoint(projectileSound, transform.position, audioVolume);		
	}
	
	// Update is called once per frame
	void Update () {	
		if (Input.GetKeyDown(KeyCode.Space)) {		
			InvokeRepeating("Fire", 0.000001f, firingRate); 			
		} 
		
		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}
		
		if (Input.GetKey(KeyCode.LeftArrow)) {
            LoadSprites(2);
			transform.position += Vector3.left * shipSpeed * Time.deltaTime;            
		} else if (Input.GetKey(KeyCode.RightArrow)) {
            LoadSprites(1);
            transform.position += Vector3.right * shipSpeed * Time.deltaTime;		
		}
        if (Input.GetKeyUp(KeyCode.LeftArrow) || (Input.GetKeyUp(KeyCode.RightArrow))) {
            LoadSprites(0);
        } 

            // pour restreindre la position du joueur dans la zone de jeu
            float newX = Mathf.Clamp(transform.position.x, minPosX, maxPosX);		
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);		
	}
	
	void DetectGameSpace() {	
		Camera mainCamera = Camera.main;
		
		// distance en Z de la camera au gameobject du joueur
		float distance = transform.position.z - mainCamera.transform.position.z;		
		
		// Position en bas à gauche de la zone de jeu
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		
		// Position en bas à droite de la zone de jeu
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		
		minPosX = leftBoundary.x + padding;
		maxPosX = rightBoundary.x - padding;		
	}
	
	void OnTriggerEnter2D(Collider2D col) {	
		Projectile projectile = col.gameObject.GetComponent<Projectile>();
		
		if(projectile) {
			health -= projectile.GetDamage();
            healthController.Damage(projectile.GetDamage());
			projectile.Hit();
			if (health <= 0) {
				Die();	
			}			
		}
	}
	
	void Die() {
		Destroy(gameObject);
		AudioSource.PlayClipAtPoint(projectileSound, transform.position, audioVolume);
		LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		levelManager.LoadLevel("Win Screen");
	}

    void LoadSprites(int index) {
        if (playerSprites[index]) {
            this.GetComponent<SpriteRenderer>().sprite = playerSprites[index];
        } else {
            Debug.LogError("No Sprite to render. Brick sprite missing.");
        }
    }
    
}
