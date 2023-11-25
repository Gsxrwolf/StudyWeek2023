using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ScoreBoardManager : MonoBehaviour
{
    public static ScoreBoardManager Instance { get; private set; }
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

    private List<float> curScoreBuffer = new List<float>();
    private List<float> lastScoreboard = new List<float>();
    private List<float> updatedScoreboard = new List<float>();


    [SerializeField] private List<TextMeshProUGUI> scoreTextFields = new List<TextMeshProUGUI>();

    private void Start()
    {
        curScoreBuffer = GameManager.Instance.scoreBuffer;
        lastScoreboard = GameManager.Instance.scoreboard;
        foreach (float processedScore in curScoreBuffer)
        {
            if (CheckIfScoreboardHasEmptySlots())
            {
                SortScoreInScoreBoard(processedScore);
            }
            else
            {
                if (CheckIfProcessedScoreIsHigherThanLowestScore(processedScore))
                {
                    DeleteLowestScore();
                    SortScoreInScoreBoard(processedScore);
                }
            }

        }
        SendUpdatedScoreBoardToGameManagerAndResetCurScore();
        PrintUpdatedScoreBoardToText();
    }

    private bool CheckIfProcessedScoreIsHigherThanLowestScore(float _processedScore)
    {
        if (_processedScore > updatedScoreboard[0])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SendUpdatedScoreBoardToGameManagerAndResetCurScore()
    {
        GameManager.Instance.scoreboard = updatedScoreboard;
    }

    private void PrintUpdatedScoreBoardToText()
    {
        updatedScoreboard.Sort();
        updatedScoreboard.Reverse();
        for (int i = 0; i < scoreTextFields.Count; i++)
        {
            if (updatedScoreboard[i] != null)
            {
                scoreTextFields[i].text = updatedScoreboard[i].ToString();
            }
        }
    }
    

    private void DeleteLowestScore()
    {
        updatedScoreboard.Sort();
        updatedScoreboard.RemoveAt(0);
    }

    private void SortScoreInScoreBoard(float _processedScore)
    {
        updatedScoreboard.Add(_processedScore);
        updatedScoreboard.Sort();
    }

    public bool CheckIfScoreboardHasEmptySlots()
    {
        if (updatedScoreboard.Count < 10)
        {
            return true;
        }
        if (updatedScoreboard.Count >= 10)
        {
            return false;
        }
        return true;
    }
}
