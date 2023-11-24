using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag;
    private PlayerController effected;
    [SerializeField] private bool directDamage;
    [SerializeField] private float directDamageAmount;

    [SerializeField] private bool continuiusDamage;
    [SerializeField] private float tickDamageAmount;
    [SerializeField] private float timeBetweenTicks;
    private float tickTimeContinuius;

    [SerializeField] private bool passedDamage;
    private bool playerIsDamagedOverTime;
    [SerializeField] private float passedDamageAmount;
    [SerializeField] private float passedTime;
    private float tickTimePassed;

    private bool playerIsStaying;
    private bool playerLeft;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            effected = other.gameObject.GetComponent<PlayerController>();
            playerIsStaying = true;
            if (directDamage)
            {
                effected.DealDamage(directDamageAmount);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            playerIsStaying = false;
            playerLeft = true;
        }
    }

    private void Update()
    {
        if (playerIsStaying)
        {
            if (continuiusDamage)
            {
                if (tickTimeContinuius >= timeBetweenTicks)
                {
                    effected.DealDamage(tickDamageAmount);
                    tickTimeContinuius = 0;
                }
                else
                {
                    tickTimeContinuius += 1 * Time.deltaTime;
                }
            }
        }
        if(passedDamage && playerLeft)
        {
            if(tickTimePassed >= passedTime)
            {
                playerLeft = false;
                tickTimePassed = 0;
            }
            else
            {
                effected.DealDamage(passedDamageAmount);
                tickTimePassed += 1 * Time.deltaTime;
            }
        }

    }
}
