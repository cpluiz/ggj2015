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
    private Image characterDisplay, PauseButton;
    private Canvas Pause,Cutscene,MainDisplay;

    void Start() {
        timer = -1;
        runing = false;
        characterDisplay = GameObject.FindGameObjectWithTag("CharacterDisplay").GetComponent<Image>();
        Timer = GameObject.FindGameObjectWithTag("TimerText").GetComponent<Text>();
        Pause = GameObject.FindGameObjectWithTag("Pause").GetComponent<Canvas>();
        Cutscene = GameObject.FindGameObjectWithTag("Cutscene").GetComponent<Canvas>();
        MainDisplay = GameObject.FindGameObjectWithTag("MainDisplay").GetComponent<Canvas>();
        PauseButton = GameObject.Find("PauseButton").GetComponent<Image>();
        Pause.gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(false);
        StartCutscene();
    }

    private void StartCutscene() {
        Cutscene.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("cutscenes/cutscene" + lvl);
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().playSound("The Builder", true);
        Cutscene.gameObject.SetActive(true);
    }

    public void SkipCutscene() {
        Cutscene.gameObject.SetActive(false);
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().stopSound();
        Time.timeScale = 1;
        Invoke("RunDisplay", 0.1f);
    }

    public void StartDisplay(float maxTime){
        Time.timeScale = 0;
        lvl = GameObject.FindWithTag("GameController").GetComponent<GameConfig>().getFase();
        MainDisplay.gameObject.SetActive(false);
        timer = maxTime;
        StartCutscene();
        PauseButton.gameObject.SetActive(true);
    }

    public void RunDisplay() {
        MainDisplay.gameObject.SetActive(true);
        runing = true;
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().playSound("Monkeys Spinning Monkeys", true);
    }

    public void setDisplay(int character) {
        characterDisplay.sprite = Resources.Load<Sprite>("hud" + character);
    }

    public void pauseFunction() {
        if (Time.timeScale > 0) {
            GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().pauseSoud();
            PauseButton.sprite = Resources.Load<Sprite>("Interface/play");
            Pause.gameObject.SetActive(true);
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
            GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().continueSound();
            PauseButton.sprite = Resources.Load<Sprite>("Interface/pause");
            Pause.gameObject.SetActive(false);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P) && runing) {
                pauseFunction();
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
