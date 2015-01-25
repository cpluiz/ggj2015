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
        }
    }

    void OnCollisionEnter2D(Collision2D tile) {
        if (gameObject.rigidbody2D.mass > 1 && tile.gameObject.tag == "Empurravel" && grounded) {
			print("Collide");
            if (tile.gameObject.tag == "Empurravel") {
				//Verificar collider tag
                tile.gameObject.AddComponent<Rigidbody2D>();
                tile.gameObject.rigidbody2D.fixedAngle = true;
                animRef.SetBool("collidewall", true);
                if (animRef.GetFloat("speed") > 0) { audioManager.playOneShot("arrastar"); }
            }
        }else if(tile.gameObject.rigidbody2D && gameObject.rigidbody2D.mass<=1){
			print("Collide cancel");
            animRef.SetBool("collidewall", false);
            Destroy(tile.gameObject.rigidbody2D);
        }
    }

    void OnCollisionStay2D(Collision2D tile) {
        if (gameObject.rigidbody2D.mass > 1 && tile.gameObject.tag == "Empurravel" && !grounded) {
            animRef.SetBool("collidewall", (true && grounded));
            animRef.SetBool("jump", false);
            Destroy(tile.gameObject.rigidbody2D);
        }
    }

    void OnCollisionExit2D(Collision2D tile) {
        if (gameObject.rigidbody2D.mass > 1){
            animRef.SetBool("collidewall", false);
        }
    }

    void FixedUpdate() {
        if (active) {
            if (gameObject.rigidbody2D.mass > 1) {
                Physics2D.OverlapCircleNonAlloc(new Vector2(groundCheck.position.x, groundCheck.position.y), groundRadius, tile);
                if (animRef.GetBool("collidewall") && tile != null) {
                    grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, isGround);
                    for (int i = 0; i < tile.Length; i++) {
                        if (tile[i].tag == "Empurravel") { grounded = false; }
                    }
                }else{
                    grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, isGround);
                }
            } else {
                grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, isGround);
            }
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
