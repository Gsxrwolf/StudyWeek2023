using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollactableHover : MonoBehaviour
{

    [SerializeField] float sinTime = 0f;
    [SerializeField] float sinOffset = 0f;
    [SerializeField] float sinFrequenz = 0.1f;
    [SerializeField] float sinAmplitude = 0.1f;

    private bool hover;
    void Update()
    {
        if (hover)
        {
            transform.position = new Vector3(transform.position.x + sinAmplitude * (Mathf.Sin(sinFrequenz * (sinTime-1)) + sinOffset), transform.position.y + sinAmplitude * (Mathf.Sin(sinFrequenz * sinTime) + sinOffset), transform.position.z);
            sinTime += 1f * Time.deltaTime;
        }
    }
    private void OnEnable()
    {
        hover = true;
    }
    private void OnDisable()
    {
        hover = false;
    }
}
