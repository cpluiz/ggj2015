using UnityEngine;
using System.Collections;
using System;

//Script para movimento do player.

public class player : MonoBehaviour {
	[SerializeField]
	private float speed = 0.4f; //Modificador na velocidade de movimento do player no eixo x,y.
    private ObjectManager manager;
    [SerializeField]
    private bool moving;
    [SerializeField]
    private Vector3 newPosition = new Vector3();
    private Vector3 oldPosition = new Vector3();

	void Update()
	{
		//Pega o input do player pelas setas, o valor varia entre (-1,0,1)
		float inputX = Input.GetAxis("Horizontal"); 
		float inputY = Input.GetAxis("Vertical");

        if (inputX != 0 && !moving) {
            setPosition('x',inputX);
        }else if (inputY != 0 && !moving){
            setPosition('y',inputY);
        }
	}

    private void setPosition(char ax,float inputAxis) {
        moving = true;
        newPosition = transform.position;
        oldPosition = transform.position;
        if (ax == 'x') {
            newPosition.x += (float)Math.Round((Mathf.Sign(inputAxis)*manager.tileDistance()),2);
        }
        else {
            newPosition.y += (float)Math.Round((Mathf.Sign(inputAxis) * manager.tileDistance()), 2);
        }
    }

    private void backMove() {
        moving = false;
        Debug.Log("returning to position x:" + oldPosition.x + ", y:" + oldPosition.y);
        transform.position = oldPosition;
    }

    void OnCollisionEnter2D(Collision2D coll){
        backMove();
    }
	
	void FixedUpdate()
	{
        if (moving){
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
        }
        if (moving && transform.position == newPosition){
            moving = false;
        }
	}

	public void DisablePlayer()
	{
		gameObject.SetActive (false);
	}

    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "Finish") {
            verifyManager();
            manager.endLevel();
        }
    }

	public void SpawnPlayer(Vector2 position)
	{
		transform.position = position;
        newPosition = transform.position;
        verifyManager();
        gameObject.SetActive(true);
	}

    public void verifyManager(){
        if (manager == null) { manager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>(); manager.forceAwake(); }
    }
	
}
