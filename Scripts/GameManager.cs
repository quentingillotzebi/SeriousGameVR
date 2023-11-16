using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Random = System.Random;
using TMPro;


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

	public TextMeshProUGUI rushModeText;

	public Light alarme;

	public Light classicLight;

	private String rushMode = "RUSH MODE";

	IEnumerator RushModeBlinking() {
		classicLight.enabled = false;
		while (true) {
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
        while(true)
        {
            yield return new WaitForSeconds(1.0f);

            // Every second in IRL is x minutes in game. This way, one day is always 5 minutes in game.
            currentDayHourInMin += (dayEndHourInMin - dayStartHourInMin) / (60 * dayDurationInIRLMin);
			currentDayHourInMin += 15;
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
            if (currentDayHourInMin >= dayEndHourInMin )
            {
                if (currentDayIndex < Days.Last().Key)
                {
					classicLight.enabled = true;
					isRushPeriod = false;
                    currentDayIndex++;
                    currentDayHourInMin = dayStartHourInMin;
					rushModeText.text = "";
					StopCoroutine("RushModeBlinking");

                    randomRushPeriod();
                    Spawner.GetComponent<Spawner>().period = spawnerPeriod;
                }

                // Sinon screen de fin
            }
        }
    }

    private void randomRushPeriod()
    {
        rushPeriodStartHourInMin = new Random().Next(dayStartHourInMin, dayEndHourInMin);
        rushPeriodEndHourInMin = rushPeriodStartHourInMin + (dayEndHourInMin - dayStartHourInMin) / dayDurationInIRLMin;
    }

	void startBlinking() {
		StopCoroutine("RushModeBlinking");
		StartCoroutine("RushModeBlinking");		
	} 

    private void Start()
    {
        spawnerPeriod = Spawner.GetComponent<Spawner>().period;
		rushModeText.text = "";
        currentDayHourInMin = dayStartHourInMin;
        randomRushPeriod();
        StartCoroutine(ChangeHourEverySecond());
    }
}
