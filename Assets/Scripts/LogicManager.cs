using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startScreen;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject creditsScreen;
    [SerializeField]
    private GameObject mainScreen;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject helpScreen;
    public bool gameIsActive = false;
    private Camera MainCamera;

    private bool reached = false;
    public float DayTime;
    private float timer = 0;

    public GameObject Sun { get; set; }
    public GameObject Moon { get; set; }
    public GameObject Shade { get; set; }
    public GameObject Timer { get; set; }

    public void GameStart()
    {
        gameIsActive = true;
        startScreen.SetActive(false);
        mainScreen.SetActive(true);
    }
    public void GameOver() 
    {
        gameOverScreen.SetActive(true);
        gameIsActive = false;
    }
    public void ShowCredits()
    {
        startScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }
    public void ExitCredits()
    {
        creditsScreen.SetActive(false);
        startScreen.SetActive(true);
    }
    public void Winning()
    {
        winScreen.SetActive(true);
        mainScreen.SetActive(false);
        gameIsActive = false;
    }
    public void ShowHelp()
    {
        startScreen.SetActive(false);
        helpScreen.SetActive(true);
    }
    public void ExitHelp()
    {
        startScreen.SetActive(true);
        helpScreen.SetActive(false);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        DayTime = 22;
        MainCamera = GameObject.FindGameObjectWithTag(nameof(MainCamera)).GetComponent<Camera>();
        MainCamera.backgroundColor = new Color(0,0.742f, 1);

        Sun = GameObject.FindGameObjectWithTag(nameof(Sun));
        Moon = GameObject.FindGameObjectWithTag(nameof(Moon));
        Shade = GameObject.FindGameObjectWithTag(nameof(Shade));
        Moon.SetActive(false);
        Shade.SetActive(false);
        Timer = GameObject.FindGameObjectWithTag(nameof(Timer));
        mainScreen.SetActive(false);
    }
    private void Update()
    {
        if (!reached && gameIsActive)
        {
            if (timer <= DayTime)
            {
                timer += Time.deltaTime;
                Timer.GetComponent<Text>().text = timer.ToString("00") + "/" + DayTime.ToString();
            }
            else
            {
                reached = true;
                
                if(gameIsActive)
                    NightComes();
            }
        }
    }

    private void NightComes()
    {
        MainCamera.backgroundColor = new Color(0, 0.056f, 0.377f);
        Sun.SetActive(false);
        Moon.SetActive(true);
        Shade.SetActive(true);

        GameObject.FindGameObjectWithTag(nameof(MainCamera)).transform.position =
            new Vector3(0.57f, 0.11f, -10);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        GameObject.FindGameObjectWithTag("SpawnPortal").GetComponent<Spawner>().SpawnPlayer();

        mainScreen.SetActive(false);
    }
}
