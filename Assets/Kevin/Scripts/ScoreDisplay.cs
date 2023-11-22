using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public void OnScoreChange(float _newScore)
    {
        string output = "Score: " + _newScore;
        GetComponent<TextMeshProUGUI>().text = output;
    }
}
