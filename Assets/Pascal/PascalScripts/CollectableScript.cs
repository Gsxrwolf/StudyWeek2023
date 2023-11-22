using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{

    [SerializeField] private string playerTag;
    [SerializeField] public bool heart;
    [SerializeField] public bool star;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            PlayerController effected = other.gameObject.GetComponent<PlayerController>();
            if (star)
            {

            }

            if (heart)
            {

            }
        }
    }
}
