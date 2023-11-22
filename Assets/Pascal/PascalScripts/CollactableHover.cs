using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollactableHover : MonoBehaviour
{

    [SerializeField] float sinTime = 0f;
    [SerializeField] float sinOffset = 0f;
    [SerializeField] float sinFrequenz = 0.1f;
    [SerializeField] float sinAmplitude = 0.1f;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + sinAmplitude * (Mathf.Sin(sinFrequenz * sinTime) + sinOffset), transform.position.z);
        sinTime += 1f * Time.deltaTime;
    }
}
