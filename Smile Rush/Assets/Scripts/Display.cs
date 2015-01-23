using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[SerializePrivateVariables]
public class Display : MonoBehaviour {

    private Text Timer, Level;
    private float timer;
    private bool runing;
    private int lvl;

    public void StartDisplay(int level, float maxTime) {
        Level.text = level.ToString();
        timer = maxTime;
        lvl = level;
        runing = true;
    }

    public float runTimer() {
        if (runing) {
            timer = timer - Time.deltaTime;
            Timer.text = Math.Round(timer, 0).ToString();
        }
        return timer;
    }
    public void stopTimer() {
        runing = false;
    }
}
