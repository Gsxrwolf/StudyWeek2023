using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexEffect : MonoBehaviour
{
    private float length;
    private float startPos;
    public GameObject cam;
    public float parallexEffect;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float dist = (cam.transform.position.x * parallexEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
    }
}
