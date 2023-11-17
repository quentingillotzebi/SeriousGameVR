using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using Random = System.Random;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    /** PLayer */


    // Remaining life of the player.
    public float nbLives = 3;

    // Score of the player.
    public float score = 0;


    /** Manage days and hours */

    // Current day index
    public int currentDayIndex = 1;

    // Dict for the days of the week
    public IDictionary<int, string> Days = new Dictionary<int, string>
    {
        { 1, "Monday" },
        { 2, "Tuesday" },
        { 3, "Wednesday" },
        { 4, "Thursday" },
        { 5, "Friday" }
    };

    // Current Date hour in minutes
    public float currentDayHourInMin = 0;

    // Conf data for the day
    private int dayStartHourInMin = 420;
    private int dayEndHourInMin = 1080;
    private int dayDurationInIRLMin = 5;


    /** Manage rush period for the spawner */


    public GameObject Spawner;

    private float spawnerPeriod;

    private int rushPeriodStartHourInMin = 0;
    private int rushPeriodEndHourInMin = 0;

    public bool isRushPeriod = false;

    public GameObject blackScreenCanvas;

    public TextMeshProUGUI rushModeText;

    public Light alarme;

    public Light classicLight;

    private String rushMode = "RUSH MODE";

    IEnumerator RushModeBlinking()
    {
        classicLight.enabled = false;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            rushModeText.text = "";
            alarme.enabled = false;
            yield return new WaitForSeconds(0.5f);
            rushModeText.text = rushMode;
            alarme.enabled = true;
        }
    }


    IEnumerator ChangeHourEverySecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            // Every second in IRL is x minutes in game. This way, one day is always 5 minutes in game.

            currentDayHourInMin += (dayEndHourInMin - dayStartHourInMin) / (60 * dayDurationInIRLMin);

            if (nbLives <= 0)
            {
                StartCoroutine(PauseGameAndDisplayScreen(false, true));
            }
            else
            {

                if (currentDayHourInMin >= rushPeriodStartHourInMin && !isRushPeriod)
                {
                    isRushPeriod = true;
                    rushModeText.text = rushMode;
                    classicLight.enabled = false;
                    alarme.enabled = true;
                    startBlinking();
                    Spawner.GetComponent<Spawner>().period = spawnerPeriod / 2;
                }

                if (currentDayHourInMin >= rushPeriodEndHourInMin && isRushPeriod)
                {
                    StopCoroutine("RushModeBlinking");
                    alarme.enabled = false;
                    //classicLight.enabled = true;
                    isRushPeriod = false;
                    rushModeText.text = "";
                    Spawner.GetComponent<Spawner>().period = spawnerPeriod;
                }

                // Change the current day index when current day is over
                if (currentDayHourInMin >= dayEndHourInMin)
                {
                    if (currentDayIndex < Days.Last().Key)
                    {

                        // Change the day and reset start hour

                        classicLight.enabled = true;
                        isRushPeriod = false;
                        currentDayIndex++;
                        currentDayHourInMin = dayStartHourInMin;
                        rushModeText.text = "";
                        StopCoroutine("RushModeBlinking");

                        // Bonus score at the end of the day + earn a new lives
                        score += nbLives * 100;
                        if (nbLives == 3) score += 200;

                        if (nbLives <= 2)
                        {
                            nbLives += 1;
                        }
                        else
                        {
                            nbLives = 3;
                        }

                        // New random rush period
                        RandomRushPeriod();

                        // Reduce the spawer period 10 percent each day.
                        spawnerPeriod = spawnerPeriod * 0.9f;
                        Spawner.GetComponent<Spawner>().period = spawnerPeriod;

                        StartCoroutine(PauseGameAndDisplayScreen(false, false));
                    }
                    else
                    {
                        StartCoroutine(PauseGameAndDisplayScreen(false, true));
                    }
                }
            }
        }

    }

    IEnumerator PauseGameAndDisplayScreen(bool startOfGame, bool endOfGame)
    {
        float pauseEndTime = Time.realtimeSinceStartup + 5f; // Set the end time for the pause

        Time.timeScale = 0; // Pause the game

        blackScreenCanvas.SetActive(true);

        if (startOfGame)
        {
            blackScreenCanvas.GetComponentInChildren<TextMeshProUGUI>().text = $"Dwane S. Imulator";
        }
        else if (endOfGame)
        {
            string hiredText = "Well done, you're hired !";

            if (nbLives <= 0)
            {
                hiredText = "Not this time, keep training ...";
            }
            blackScreenCanvas.GetComponentInChildren<TextMeshProUGUI>().text = $"The week is over. \n Lives remaining : {nbLives}\n Score : {score} \n {hiredText}";
        }
        else
        {
            blackScreenCanvas.GetComponentInChildren<TextMeshProUGUI>().text = $"Next day : \n {Days[currentDayIndex]}\n\nThe lugages will spawn faster than yesterday.";
        }

        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return null;
        }

        if (endOfGame)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(scene.name);
            SceneManager.LoadScene(scene.name);
        }
        else
        {
            blackScreenCanvas.SetActive(false);
            Time.timeScale = 1; // Resume the game
        }

    }

    private void RandomRushPeriod()
    {
        rushPeriodStartHourInMin = new Random().Next(dayStartHourInMin, dayEndHourInMin);
        rushPeriodEndHourInMin = rushPeriodStartHourInMin + (dayEndHourInMin - dayStartHourInMin) / dayDurationInIRLMin;
    }

    void startBlinking()
    {
        StopCoroutine("RushModeBlinking");
        StartCoroutine("RushModeBlinking");
    }

    private void Start()
    {
        StartCoroutine(PauseGameAndDisplayScreen(true, false));
        spawnerPeriod = Spawner.GetComponent<Spawner>().period;
        rushModeText.text = "";
        currentDayHourInMin = dayStartHourInMin;
        RandomRushPeriod();
        StartCoroutine(ChangeHourEverySecond());
    }

    public void AddScore(float pointsToAdd)
    {
        score += pointsToAdd;
    }

    // Fonction pour réduire la vie du joueur
    public void ReduceLife(double lifeToReduce)
    {
        nbLives -= (float)lifeToReduce;

/*         if (nbLives <= 0)
        {
            GameOver();
        } */
    }

    private void GameOver()
    {
        //Fin du jeu
    }
}
