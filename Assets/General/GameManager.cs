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

    private void OnSceneUnloaded(Scene current)
    {
        if(current.buildIndex == 6)
        {
            scoreBuffer.Add(curScore);
            curScore = 0;
        }
    }

}
