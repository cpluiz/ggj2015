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

	void Awake(){
		facingRight = true;
		groundRadius = 0.2f;
	}
	
	void Update(){
        if (grounded && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && Time.timeScale > 0){
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}
	}

    void FixedUpdate() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, isGround);
        float move = Input.GetAxis("Horizontal");
        //motion.SetFloat("speed", Mathf.Abs(move));
        //motion.SetBool("grounded", grounded);
        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);


        if ((move > 0 && !facingRight) || (move < 0 && facingRight)){
            Flip();
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
