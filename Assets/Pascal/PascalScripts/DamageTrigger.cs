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
    [SerializeField] private float tickTime;

    private bool playerIsStaying;


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
        }
    }

    private void Update()
    {
        if (playerIsStaying)
        {
            if (continuiusDamage)
            {
                if (tickTime >= timeBetweenTicks)
                {
                    effected.DealDamage(tickDamageAmount);
                    tickTime = 0;
                }
                else
                {
                    tickTime += 1 * Time.deltaTime;
                }
            }
        }

    }
}
