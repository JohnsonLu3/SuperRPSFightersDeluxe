using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour {

    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] GameObject rockLabel;
    [SerializeField] GameObject paperLabel;
    [SerializeField] GameObject scissorLabel;
    [SerializeField] GameObject shootLabel;

    int time;
    float countDown;

    // Use this for initialization
    void Start () {

        time = roundCountDownTime * 60;
        countDown = Time.time + Round.roundCountDownTime * 60;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!Round.roundStart)
        {
            float countDownLeft = countDown - Time.time ;

            if (countDownLeft < 3.5 * 60)
            {
                if (countDownLeft > 2 * 60)
                {
                    rockLabel.SetActive(true);
                }
                else if (countDownLeft > 1 * 60)
                {
                    rockLabel.SetActive(false);
                    paperLabel.SetActive(true);
                }
                else if (countDownLeft > .3 * 60)
                {
                    paperLabel.SetActive(false);
                    scissorLabel.SetActive(true);
                }
                else if (countDownLeft > 0 * 60)
                {
                    scissorLabel.SetActive(false);
                    shootLabel.SetActive(true);
                }
                else if (countDownLeft <= 0)
                {
                    shootLabel.SetActive(false);
                    Round.roundStart = true;
                }
            }

            if(countDown > 0)
            {
                countDown--;
            }
            


        }
        else {
            if (Round.roundStart && time > 0 )
            {
                float countDownLeft = time - Time.time;
                if (Mathf.RoundToInt(countDownLeft / 60) > 9) {
                    timer.SetText(Mathf.RoundToInt(countDownLeft / 60).ToString());
                }
                else {
                    timer.SetText("0" + Mathf.RoundToInt(countDownLeft / 60).ToString());

                }
            }

            if (time > 0)
            {
                time--;
            }

        }
    }

}
