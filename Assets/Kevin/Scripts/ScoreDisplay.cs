using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    float curScore;
    public void Update()
    {
        curScore = GameManager.Instance.score;
        string output = "Score: " + curScore;
        GetComponent<TextMeshProUGUI>().text = output;
    }
}
