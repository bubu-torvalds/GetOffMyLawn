using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float shipSpeed = 50000f;
    public float padding = 1;
    public GameObject projectile;
    public float projectileSpeed;
    public float firingRate = 0.2f;
    public float health = 250f;
    public AudioClip projectileSound;
    public AudioClip dyingSound;
    public float audioVolume = 0.5f;
    public Sprite[] playerSprites;
    
    private LifeBar lifeBar;
    private Animator animator;
    private float minPosX;
	private float maxPosX;

    //animation states
    const int STATE_IDLE = 0;
    const int STATE_WALK_RIGHT = 1;
    const int STATE_WALK_LEFT = 2;

    int currentAnimationState = STATE_IDLE;
	

	// Use this for initialization
	void Start () {	
        
		DetectGameSpace();
        lifeBar = GameObject.FindObjectOfType<LifeBar>();
        animator = this.GetComponentInChildren<Animator>();
    }
	
	void Fire() {
        GameObject projectile = Instantiate(this.projectile, transform.position, Quaternion.identity) as GameObject;		
		projectile.GetComponentInChildren<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);		
		AudioSource.PlayClipAtPoint(projectileSound, transform.position, audioVolume);		
	}
	
	// Update is called once per frame
	void FixedUpdate () {	
		if (Input.GetKeyDown(KeyCode.Space)) {		
			//InvokeRepeating("Fire", 0.001f, firingRate);
            Invoke("Fire", 0.001f);
		} 
		
		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}
		
        if (Input.GetKeyUp(KeyCode.LeftArrow) || (Input.GetKeyUp(KeyCode.RightArrow))) {            
            changeState(STATE_IDLE);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            changeState(STATE_WALK_LEFT);
            transform.Translate(Vector3.left * shipSpeed * Time.deltaTime);

        } else if (Input.GetKey(KeyCode.RightArrow)) {
            changeState(STATE_WALK_RIGHT);
            transform.Translate(Vector3.right * shipSpeed * Time.deltaTime);
        }

        // pour restreindre la position du joueur dans la zone de jeu
        float newX = Mathf.Clamp(transform.position.x, minPosX, maxPosX);		
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);		
	}
	
	void OnTriggerEnter2D(Collider2D col) {	
		Projectile projectile = col.gameObject.GetComponent<Projectile>();
		
		if(projectile) {
            AudioSource.PlayClipAtPoint(dyingSound, transform.position, audioVolume);
            health -= projectile.GetDamage();
            if (health > 0) {
                lifeBar.LoseLife();
            }
			projectile.Hit();
			if (health <= 0) {
				Die();	
			}			
		}
	}
	
	void Die() {
		Destroy(gameObject);
		AudioSource.PlayClipAtPoint(dyingSound, transform.position, audioVolume);
		LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		levelManager.LoadLevel("Lose Screen");
	} 
    
    void changeState(int state) {
        if (currentAnimationState == state)
            return;

        switch (state) {

            case STATE_WALK_LEFT:
                animator.SetInteger("state", STATE_WALK_LEFT);
                break;

            case STATE_WALK_RIGHT:
                animator.SetInteger("state", STATE_WALK_RIGHT);
                break;

            case STATE_IDLE:
                animator.SetInteger("state", STATE_IDLE);
                break;

        }

        currentAnimationState = state;
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 1);
    }

    void DetectGameSpace() {
        Camera mainCamera = Camera.main;

        // distance en Z de la camera au gameobject du joueur
        float distance = transform.position.z - mainCamera.transform.position.z;

        // Position en bas à gauche de la zone de jeu
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));

        // Position en bas à droite de la zone de jeu
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        minPosX = leftBoundary.x + padding;
        maxPosX = rightBoundary.x - padding;
    }
}
