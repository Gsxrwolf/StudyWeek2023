using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour
{
    public Image image;

    public Enemy enemy;

    void Update()
    {
        image.fillAmount = enemy.health * 0.01f; // Fill amount bedienen zu können * 0.01f
    }
}
