using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[SerializePrivateVariables]
public class ObjectManager : MonoBehaviour {
    private Tile tile;
    private Display display;
    private Image loadScreen;
    private Text loadText;
    public player playerRef;
	public float timer;

    private int level;
    private bool loaded;

    public void forceAwake(){
        if (tile == null) { Awake(); }
    }

    void Awake(){
        tile = GameObject.Find("GeraTile").GetComponent<Tile>();
        display = GameObject.Find("Display").GetComponent<Display>();
        loadScreen = GameObject.Find("LoadScreen").GetComponent<Image>();
        loadText = GameObject.Find("LoadText").GetComponent<Text>();
        loaded = false;
        level = 0;
    }

    void Update() {
        if (loaded) {
            if (display.runTimer() < 0) {
            restart();
            }
        }
    }

    private void restart() {
        if (!loadScreen.gameObject.activeSelf) {
            loaded = false;
            display.stopTimer();
            loadScreen.gameObject.SetActive(true);
            tile.restart(); // constroi novamente o mesmo tile
        }
    }

    public void endLevel() {
        loaded = false;
        display.stopTimer();
        loadScreen.gameObject.SetActive(true);
        //tile.newMap(); // essa função cria um tile novo
    }

    public void loading(int atual, int total) {
        if (loadScreen.gameObject.activeSelf) {
            float percent = (atual*100)/total;
            if (percent < 100){
                loadText.text = "Loading " + percent + "%";
            }
            else {
                loadText.text = "Loading " + percent + "%";
                loadScreen.gameObject.SetActive(false);
                Time.timeScale = 1;
                level++;
                display.StartDisplay(level, timer);
                loaded = true;
            }
        }
    }

    public float tileDistance() {
        return tile.tileDistance;
    }
}
