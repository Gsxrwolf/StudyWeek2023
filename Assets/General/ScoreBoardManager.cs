using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ScoreBoardManager : MonoBehaviour
{

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
        updatedScoreboard = lastScoreboard;
        SendUpdatedScoreBoardToGameManagerAndResetCurScore();
        PrintUpdatedScoreBoardToText();
    }

    private bool CheckIfProcessedScoreIsHigherThanLowestScore(float _processedScore)
    {
        lastScoreboard.Sort();
        if (_processedScore > lastScoreboard[0])
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
        GameManager.Instance.scoreBuffer.Clear();
        GameManager.Instance.scoreboard = updatedScoreboard;
    }

    private void PrintUpdatedScoreBoardToText()
    {
        updatedScoreboard.Sort();
        updatedScoreboard.Reverse();
        for (int i = 0; i < updatedScoreboard.Count; i++)
        {
            scoreTextFields[i].text = updatedScoreboard[i].ToString();
        }
    }


    private void DeleteLowestScore()
    {
        lastScoreboard.Sort();
        lastScoreboard.RemoveAt(0);
    }

    private void SortScoreInScoreBoard(float _processedScore)
    {
        lastScoreboard.Add(_processedScore);
        lastScoreboard.Sort();
    }

    public bool CheckIfScoreboardHasEmptySlots()
    {
        if (lastScoreboard.Count < 10)
        {
            return true;
        }
        if (lastScoreboard.Count >= 10)
        {
            return false;
        }
        return true;
    }
}
