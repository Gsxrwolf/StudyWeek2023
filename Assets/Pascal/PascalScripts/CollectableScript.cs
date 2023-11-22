using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{

    [SerializeField] private string playerTag;
    [SerializeField] public bool heart;
    [SerializeField] private float healAmount;
    [SerializeField] public bool star;
    [SerializeField] private float damageBlockTimeInSec;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            PlayerController effected = other.gameObject.GetComponent<PlayerController>();
            if (star)
            {
                effected.DamageBlock(damageBlockTimeInSec);
            }

            if (heart)
            {
                effected.HealHealth(healAmount);
            }
            CollectablePoolManager.Instance.MoveToHiddenPoint(this.gameObject);
        }
    }
}
