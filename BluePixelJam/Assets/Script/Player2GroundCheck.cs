using UnityEngine;
using System.Collections;

public class Player2GroundCheck : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D tile) {
        if (tile.tag == "Empurravel") {
            gameObject.GetComponentInParent<Player>().player2collision();
        }else{
            gameObject.GetComponentInParent<Player>().player2stopCollision();
        }
    }
}
