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
    private LayerMask isGround;
    [SerializeField]
    private Animator motion;
    private bool active;
    private AudioManager audioManager;

	private Animator animRef;

    public void setActive(bool a) {
        active = a;
        gameObject.SetActive(a);
    }

	void Awake(){
		animRef = gameObject.GetComponent<Animator> ();
        active = false;
		facingRight = true;
		groundRadius = 0.2f;
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
        if (gameObject.rigidbody2D.mass > 1 && tile.gameObject.tag == "Empurravel") {
            if (tile.gameObject.rigidbody2D == null) {
                tile.gameObject.AddComponent<Rigidbody2D>(); Debug.Log("Entrou na condição");
                animRef.SetBool("collidewall", true);
                if (animRef.GetFloat("speed") > 0) { audioManager.playOneShot("arrastar"); }
            }
        }else if(tile.gameObject.rigidbody2D && gameObject.rigidbody2D.mass<=1){
            Destroy(tile.gameObject.rigidbody2D);
        }
    }

    void OnCollisionExit2D(Collision2D tile) {
        if (tile.gameObject.tag == "Empurravel") {
            if (tile.gameObject.rigidbody2D != null) { Destroy(tile.gameObject.rigidbody2D); animRef.SetBool("collidewall", false); }
        }
    }

    void FixedUpdate() {
        if (active) {
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, isGround);
			animRef.SetBool("jump",!grounded);
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
