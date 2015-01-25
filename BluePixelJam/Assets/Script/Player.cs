using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField]
	private float maxSpeed, jumpForce, margin;
    [SerializeField]
    private bool facingRight, grounded;
    [SerializeField]
	private Transform groundCheck;
    private float groundRadius;
    [SerializeField]
    private LayerMask isGround, isEmpurravel;
    [SerializeField]
    private Animator motion;
    private bool active;
    private AudioManager audioManager;
    private Collider2D[] tile;
    [SerializeField]
    private bool empurrando, emcima;

	private Animator animRef;

    public void setActive(bool a) {
        active = a;
        gameObject.SetActive(a);
    }

	void Awake(){
		animRef = gameObject.GetComponent<Animator> ();
        active = false;
		facingRight = true;
		groundRadius = 0.001f;
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
	}
	
	void Update(){
        if (grounded && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && Time.timeScale > 0 && active){
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
            audioManager.playOneShot("jump");
		}
	}

    void OnTriggerEnter2D(Collider2D tile) {
        if (tile.tag == "Portal" && active) {
            GameConfig config = GameObject.FindWithTag("GameController").GetComponent<GameConfig>();
            AlwaysAlive fases = config.transform.GetComponent<AlwaysAlive>();
            if (config.getFase() < fases.faseMax) {
                config.setFase(config.getFase()+1);
                Application.LoadLevel("Game");
            }else{
                Application.LoadLevel("Load");
            }
        }else if (gameObject.tag == "Player2" && tile.tag == "Empurravel"){
            if (tile.rigidbody2D == null) {
                tile.gameObject.AddComponent<Rigidbody2D>();
                tile.rigidbody2D.fixedAngle = true;
                empurrando = true;
                InvokeRepeating("playArrastar", 0.1f, 1.5f);
            }else if(!grounded){
                empurrando = false;
                Destroy(tile.gameObject.rigidbody2D);
            }
        }else if (tile.gameObject.rigidbody2D && gameObject.tag != "Player2"){
            Destroy(tile.gameObject.rigidbody2D);
        }
    }

    private void playArrastar() {
        if (empurrando) { 
           if(animRef.GetFloat("speed")>0 && !emcima){audioManager.playOneShot("arrastar");} 
        } else { CancelInvoke("playArrastar"); }
    }

    void OnTriggerStay2D(Collider2D tile) {
        if (gameObject.tag == "Player2" && tile.gameObject.tag == "Empurravel") {
            if(!grounded || emcima){
                empurrando = false;
                Destroy(tile.gameObject.rigidbody2D);
            }else if(tile.rigidbody2D == null){
                tile.gameObject.AddComponent<Rigidbody2D>();
                tile.rigidbody2D.fixedAngle = true;
                empurrando = true;
                InvokeRepeating("playArrastar", 0.1f, 1.5f);
            }else{
                empurrando = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D tile) {
        if (gameObject.tag == "Player2" && empurrando){
            empurrando = false;
        }
    }

    public void player2collision() { empurrando = false; emcima = true; }
    public void player2stopCollision() { emcima = false; }

    void FixedUpdate() {
        if (active) {
            if (gameObject.tag == "Player2") {
                animRef.SetBool("collidewall", (empurrando && grounded && !emcima));
            }
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, isGround);
            animRef.SetBool("jump", !grounded);
            float move = Input.GetAxis("Horizontal");
            //motion.SetFloat("speed", Mathf.Abs(move));
            //motion.SetBool("grounded", grounded);
            rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

			animRef.SetFloat("speed",Mathf.Abs(move));

            if ((move > 0 && !facingRight) || (move < 0 && facingRight))
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
