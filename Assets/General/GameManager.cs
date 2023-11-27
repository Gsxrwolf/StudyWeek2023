using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public float curScore;
    public int weapon;
    public List<float> scoreboard = new List<float>();
    public List<float> scoreBuffer = new List<float>();

    public bool isMuted;

    public void OnPlayerDeath()
    {
            scoreBuffer.Add(curScore);
            curScore = 0;
    }

    internal void OnMobDeath(float _scorePoints)
    {
        curScore += _scorePoints;
    }
}
