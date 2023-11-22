using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawnTriggerScript : MonoBehaviour
{
    [SerializeField] private string playerTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            CollectablePoolManager.Instance.MoveToSpawnPoint(gameObject.transform.position);
            gameObject.SetActive(false);
        }
    }
}
