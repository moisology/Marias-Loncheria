using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class scoreHandle : MonoBehaviour
{
    public GameObject player, currentScore;
    float endTime, timer, currTime;
    bool hasEnded;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        hasEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasEnded)
        {
            SceneManager.LoadScene("end scene");
        }
        updateScore();
        checkHighScore();
        currTime += Time.deltaTime;
    }

    void checkHighScore()
    {
        if(player.GetComponent<inventoryHandle>().hasReachedGoal() && !hasEnded)
        {
            hasEnded = true;
            endTime = currTime;
            if(endTime < PlayerPrefs.GetFloat("HighScore", float.MaxValue) || (PlayerPrefs.GetFloat("HighScore") <= 0f) )
            {
                PlayerPrefs.SetFloat("HighScore", endTime);
            }
        }
    }

    void updateScore()
    {
        this.GetComponent<nameTag>().setName("Fastest Time: " + getHighScore() + "s");
        currentScore.GetComponent<nameTag>().setName("Current Time: " + getCurrScore() + "s");
    }

    string getHighScore()
    {
        return System.Math.Round(PlayerPrefs.GetFloat("HighScore"), 1).ToString();
    }

    string getCurrScore()
    {
        return System.Math.Round(currTime,1).ToString();
    }
}
