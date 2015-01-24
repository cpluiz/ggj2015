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

    public void StartDisplay(float maxTime)
    {
        timer = maxTime;
        runing = true;
    }

    public void setDisplay(int character) {
        characterDisplay.sprite = Resources.Load<Sprite>("hud" + character);
    }

    void Update() {
        if (runing) { runTimer(); }
    }

    public void runTimer()
    {
        if (runing)
        {
            timer = timer - Time.deltaTime;
            Timer.text = Math.Round(timer, 0).ToString();
        }
        if (timer <= 0) {
            Application.LoadLevel("Load");
        }
    }
    public void stopTimer()
    {
        runing = false;
    }
}
