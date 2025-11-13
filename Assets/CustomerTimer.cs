using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using System.Data.Common;

//code was used from https://www.youtube.com/watch?v=bcvLM_riVuw
public class CustomerTimer : MonoBehaviour
{
    public Image timerBar;
    

    private float maxTime = 50f;
    float timeLeft;
    public GameObject timesUpText;


    void Start()
    {

        timesUpText.SetActive(false);
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }


    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
            timerBar.color = Color.green;

            if (timeLeft < 30f)
            {
                timerBar.color = Color.yellow;
                //if the timer has 5 seconds left then change the colour to red
                if (timeLeft < 10f)
                {
                    //https://docs.unity3d.com/6000.2/Documentation/ScriptReference/Color.html
                    timerBar.color = Color.red;
                }
            }
            
        }
        else
        {
            Debug.Log("BOOOOOOOOOOOOO");
            SceneManager.LoadScene(3);
            Time.timeScale = 0;

        }
    }

    public void StartTimer()
    {
        timesUpText.SetActive(false);
        timeLeft = maxTime;
        timerBar.fillAmount = 1;
    }

}
