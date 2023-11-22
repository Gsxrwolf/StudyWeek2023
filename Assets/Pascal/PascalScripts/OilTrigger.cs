using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag;
    private float tempNormalSpeed;
    private float tempSprintSpeed;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            PlayerController effected = other.gameObject.GetComponent<PlayerController>();
            tempNormalSpeed = effected.normalSpeed;
            tempSprintSpeed = effected.sprintSpeed;
            effected.jumpBlock = true;
            effected.sprintSpeed = effected.normalSpeed;
            effected.normalSpeed = effected.normalSpeed / 2;
            effected.SpeedUpdate();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            PlayerController effected = other.gameObject.GetComponent<PlayerController>();
            effected.jumpBlock = false;
            effected.normalSpeed = tempNormalSpeed;
            effected.sprintSpeed = tempSprintSpeed;
            effected.SpeedUpdate();
        }
    }
}
