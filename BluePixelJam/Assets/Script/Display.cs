using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[SerializePrivateVariables]
public class Display : MonoBehaviour
{

    private Text Timer,CutsceneText;
    private float timer;
    private bool runing;
    private int lvl;
    private Image characterDisplay, PauseButton;
    private Canvas Pause,Cutscene,MainDisplay;
    private AudioManager audioManager;
    private GameConfig config;
    private string[] cut;

    void Start() {
        cut = new string[3];
        cut[0] = "Rabicó está acabando de acordar na fazenda da Sammy. Quando abre os olhos, metade do celeiro está destruído! Ele imagina que um furacão tenha passado e levado seus amigos. Então observa que Sammy está chorando por ter perdido seus animais. Corajoso, Rabicó faz uma cara que expressa bravura e decide partir em busca de seus amigos.";
        cut[1] = "Rabicó encontra Bisteca presa numa jaula (que só possui laterais) e fica surpreso. Eles se encontram e, enquanto a porquinha fica com cara de assustada, seu irmão menor diz “Sammy está triste e os outros animais sumiram!”. Para tirar Bisteca de lá, Rabicó começa a cavar um buraco. Mas a porquinha o impede de continuar dizendo “Espera aí”. Então ela dá um salto por cima da jaula. Agora, juntos, eles estão determinados a encontrar os outros animais da fazenda e descobrir o mistério.";
        cut[2] = "Rabicó e Bisteca avistam Toicinho preso numa jaula (dessa vez com teto e chão de metal) e correm para conversar com ele. Toicinho pergunta “o que aconteceu?!” com um olhar assustado. Rabicó diz “Não sabemos! Mas vamos te tirar daí e achar os outros”. Toicinho olha para a grade e, com uma cara de bravo agora, diz: “Para trás!”. Então ele corre contra as barras de metal e as quebra, ficando livre. Agora ele dá um sorriso esperto e termina com um “Vamos lá”.";
        timer = -1;
        runing = false;
        characterDisplay = GameObject.FindGameObjectWithTag("CharacterDisplay").GetComponent<Image>();
        Timer = GameObject.FindGameObjectWithTag("TimerText").GetComponent<Text>();
        Pause = GameObject.FindGameObjectWithTag("Pause").GetComponent<Canvas>();
        Cutscene = GameObject.FindGameObjectWithTag("Cutscene").GetComponent<Canvas>();
        MainDisplay = GameObject.FindGameObjectWithTag("MainDisplay").GetComponent<Canvas>();
        PauseButton = GameObject.Find("PauseButton").GetComponent<Image>();
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        config = GameObject.FindWithTag("GameController").GetComponent<GameConfig>();
        Pause.gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(false);
        StartCutscene();
    }

    private void StartCutscene() {
        GameObject.FindWithTag("Background").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("backgrounds/bg"+lvl);
        Cutscene.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("cutscenes/cutscene" + lvl);
        audioManager.playSound("The Builder", true);
        Cutscene.gameObject.SetActive(true);
        if (CutsceneText != null) {
            CutsceneText.text = cut[(lvl-1)];
        }else{
            CutsceneText = GameObject.FindWithTag("CutsceneText").GetComponent<Text>();
            CutsceneText.text = cut[lvl - 1];
        }
    }

    public void SkipCutscene() {
        Cutscene.gameObject.SetActive(false);
        audioManager.stopSound();
        Time.timeScale = 1;
        Invoke("RunDisplay", 0.1f);
    }

    public void StartDisplay(float maxTime){
        Time.timeScale = 0;
        lvl = config.getFase();
        MainDisplay.gameObject.SetActive(false);
        timer = maxTime;
        StartCutscene();
        PauseButton.gameObject.SetActive(true);
    }

    public void RunDisplay() {
        MainDisplay.gameObject.SetActive(true);
        runing = true;
        audioManager.playSound("Monkeys Spinning Monkeys", true);
    }

    public void setDisplay(int character) {
        characterDisplay.sprite = Resources.Load<Sprite>("hud" + character);
    }

    public void pauseFunction() {
        if (Time.timeScale > 0) {
            audioManager.pauseSoud();
            PauseButton.sprite = Resources.Load<Sprite>("Interface/play");
            Pause.gameObject.SetActive(true);
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
            audioManager.continueSound();
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
            GameObject.FindWithTag("GameController").GetComponent<GameConfig>().setFase(1);
            Application.LoadLevel("Load");
        }
    }
    public void stopTimer()
    {
        runing = false;
    }
}
