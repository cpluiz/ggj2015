using UnityEngine;
using System.Collections;
using System;

public class Tile : MonoBehaviour {

    public int tileImage;
    private int tilePosition = 1;
    public bool notVisible;
    private bool empurrando;
    private int totalPositions = 5;
    private AudioManager audioManager;

    public void changeTile(Sprite[] tilemap, float tileDistance, int tPosition) {
        tilePosition = tPosition;
        changeTile(tilemap, tileDistance);
    }

    public void changeTile(Sprite[] tilemap,float tileDistance){
        int tileSprite = ((tileImage - 1) * totalPositions) + tilePosition;
        if (!notVisible) {
            try{
                gameObject.GetComponent<SpriteRenderer>().sprite = tilemap[tileSprite];
            }catch(Exception e){
                Debug.Log("Algo errado que não está certo");
            }
        }
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2((tileDistance + 0.19f) / 2, (tileDistance + 0.19f) / 2);
        ChangePriority();
    }

    private void ChangePriority() {
        int order;
        switch (tilePosition) {
            case 0:
                order = 5;
                break;
            case 1:
                order = 4;
                break;
            case 2:
                order = 3;
                break;
            case 3:
                order = 2;
                break;
            default:
                order = 1;
                break;
        }
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = order;
    }

    public void ArrastarSound(bool empurra, AudioManager audioManager) {
        this.audioManager = audioManager;
        if (!empurrando){
            InvokeRepeating("playArrastar", 0.1f, 1.5f);
        }
        empurrando = empurra;
    }

    private void playArrastar() {
        if (gameObject.rigidbody2D != null) {
            if (transform.InverseTransformDirection(gameObject.rigidbody2D.velocity).x>0) {
                audioManager.playOneShot("arrastar");
            } else { stopArrastar(); }
        } else { stopArrastar(); }
    }
    private void stopArrastar() {
        empurrando = false;
        audioManager = null;
        CancelInvoke("playArrastar");
    }

    public float getTileDistance(){
        return gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }
}
