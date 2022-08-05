using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpiralGunTimer : MonoBehaviour
{
    public Transform activeByTimer;

    Image timerBar;
    private float myTime = 10f;
    private float timeLeft = 10f;
    private bool flag;

    protected void Start()
    {
        timerBar = GetComponent<Image>();
    }

    void Update()
    {

        if (Ship.timerIsActive)
        {
            timerBar.enabled = true;

            if (timeLeft > 0)
            {
                flag = true;
                timeLeft -= Time.deltaTime;
                timerBar.fillAmount = timeLeft / myTime;
            }
            else
            {
                timeLeft = 10f;
                myTime = 10f;
                flag = false;
                Ship.timerIsActive = false;
            }

            activeByTimer.gameObject.SetActive(flag);

            for (int j = 0; j < 4; j++)
            {
                activeByTimer.GetChild(j).gameObject.SetActive(flag);
            }
        }
    }

}
