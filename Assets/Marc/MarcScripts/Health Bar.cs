using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform player;
    public Image healthBar;

    public float xPos = 0f;


    void Update()
    {      
        if(Boss.isAlive)
        {
            Vector3 newXPos = Camera.main.WorldToScreenPoint(player.position + new Vector3(xPos, 1, 0));
            healthBar.transform.position = newXPos;
        }
        
    }
}
