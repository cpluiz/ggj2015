using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[SerializePrivateVariables]
public class Display : MonoBehaviour
{

    private Text Timer;
    private float timer;
    private bool runing;
    private int lvl;
    private Image characterDisplay;
    private Canvas Pause;

    void Start() {
        timer = -1;
        runing = false;
        characterDisplay = GameObject.FindGameObjectWithTag("CharacterDisplay").GetComponent<Image>();
        Timer = GameObject.FindGameObjectWithTag("TimerText").GetComponent<Text>();
        Pause = GameObject.FindGameObjectWithTag("Pause").GetComponent<Canvas>();
        Pause.gameObject.SetActive(false);
    }

    public void StartDisplay(float maxTime, int level)
    {
        lvl = level;
        timer = maxTime;
        runing = true;
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().playSound("Monkeys Spinning Monkeys", true);
    }

    public void setDisplay(int character) {
        characterDisplay.sprite = Resources.Load<Sprite>("hud" + character);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
                if (Time.timeScale > 0) {
                    Time.timeScale = 0;
                    GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().pauseSoud();
                    Pause.gameObject.SetActive(true);
                } else {
                    Time.timeScale = 1;
                    GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().continueSound();
                    Pause.gameObject.SetActive(false);
                }
            }
        if (runing && Time.timeScale > 0) { runTimer(); }
    }

    public void runTimer()
    {
        if (runing)
        {
            timer = timer - Time.deltaTime;
            Timer.text = Math.Round(timer, 0).ToString();
        }
        if (runing && timer <= 0) {
            Application.LoadLevel("Load");
        }
    }
    public void stopTimer()
    {
        runing = false;
    }
}
