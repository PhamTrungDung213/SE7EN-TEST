using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;

    public GameObject KickB;

    void Awake() {
        
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    void Start()
    {
        KickB.SetActive(false);
    }

    public void ShowKickButton(bool isShow)
    {
        if(!isShow)
        {
            KickB.SetActive(false);
        }else KickB.SetActive(true); 
    }

    public void OnResetButtonClicked()
    {
        GameManager.Instance.Reset();
    }

    public void OnKickButtonClicked()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null) return;

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        GameObject nearestBall = null;
        float minBallDist = Mathf.Infinity;

        foreach (GameObject b in balls)
        {
            float dist = Vector3.Distance(player.transform.position, b.transform.position);
            if (dist < minBallDist)
            {
                minBallDist = dist;
                nearestBall = b;
            }
        }

        GameObject[] goals = GameObject.FindGameObjectsWithTag("Goal");
        if (goals.Length == 0) return;

        GameObject nearestGoal = null;
        float minGoalDist = Mathf.Infinity;

        foreach (GameObject g in goals)
        {
            float dist = Vector3.Distance(nearestBall.transform.position, g.transform.position);
            if (dist < minGoalDist)
            {
                minGoalDist = dist;
                nearestGoal = g;
            }
        }

        if (nearestGoal != null)
        {
            Ball ballScript = nearestBall.GetComponent<Ball>();
            if (ballScript != null)
            {
                ballScript.KickToGoal(nearestGoal);
            }
        }
    }

    public void OnAutoKickButtonClicked()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null) return;

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        GameObject farestBall = null;
        float farBallDist = 0;

        foreach (GameObject b in balls)
        {
            float dist = Vector3.Distance(player.transform.position, b.transform.position);
            if (dist > farBallDist)
            {
                farBallDist = dist;
                farestBall = b;
            }
        }

        GameObject[] goals = GameObject.FindGameObjectsWithTag("Goal");
        if (goals.Length == 0) return;

        GameObject nearestGoal = null;
        float minGoalDist = Mathf.Infinity;

        foreach (GameObject g in goals)
        {
            float dist = Vector3.Distance(farestBall.transform.position, g.transform.position);
            if (dist < minGoalDist)
            {
                minGoalDist = dist;
                nearestGoal = g;
            }
        }

        if (nearestGoal != null)
        {
            Ball ballScript = farestBall.GetComponent<Ball>();
            if (ballScript != null)
            {
                ballScript.KickToGoal(nearestGoal);
            }
        }

        ShowKickButton(false);
    }

    public void OnExitBClicked()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}