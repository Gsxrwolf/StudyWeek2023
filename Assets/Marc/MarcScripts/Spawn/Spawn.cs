using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject m_gameobject;
   //public GameObject s_gameobject;

    public static int i = 0;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if(i == 1)
        {
            Instantiate(m_gameobject, transform.position,Quaternion.identity);
            Enemy.isAlive = true;
            i = 2; // dafür da damit if nur einmal ausgelöst wird
        }
    }
}
