using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DayNightCycle : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI dayDisplay;
    public Volume ppv;

    public float tick = 1000;
    public float seconds;
    public int mins;
    public int hours;
    public int days = 1;


    public GameObject openShutters;
    public GameObject closedShutters;

    void Start()
    {
        
        ppv = gameObject.GetComponent<Volume>();
    }

    void FixedUpdate()
    {
        CalcTime();
        DisplayTime();
    }

    public void CalcTime()
    {
        seconds += Time.fixedDeltaTime * tick;

        if (GameStateManager.endLevel == true && hours <18)
        {
            hours = 18;
            mins = 0;
        }

        if (seconds >= 60)
        {
            seconds = 0;
            mins += 1;
        }

        if (mins >= 60)
        {
            mins = 0;
            hours += 1;
        }

        if (hours >= 24)
        {
            hours = 0;
            days += 1;
        }
        ControlPPV();
    }

    public void ControlPPV()
    {
        

        if (hours >= 0 && hours < 7) // Dawn at 6:00 / 6am    -   until 7:00 / 7am
        {
            ppv.weight = 1 - (float)mins / 60; // we minus 1 because we want it to go from 1 - 0
        }

        if (hours >= 7 && hours < 18)
        {
            closedShutters.SetActive(false);
            openShutters.SetActive(true);
            tick = 100;
            //Debug.Log("Cafe is Open (day night code)");
            GameStateManager.Instance.iscafeOpen = true;
            //Debug.Log("day and night code" + GameStateManager.Instance.iscafeOpen);
            return;
        }
        if (hours >= 18 && hours < 19)
        {
            ppv.weight = (float)mins / 60;
            tick = 10000;
        }
        else
        {
            openShutters.SetActive(false);
            closedShutters.SetActive(true);
            GameStateManager.Instance.iscafeOpen = false;

            if (hours >= 19)
            {
                SceneManager.LoadScene(2);
            }
            return;
        }


    }

    public void DisplayTime()
    {
        timeDisplay.text = string.Format("{0:00}:{1:00}", hours, mins);
        dayDisplay.text = "Day: " + days;
    }
}
