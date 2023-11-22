using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePoolManager : MonoBehaviour
{
    public static CollectablePoolManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private GameObject starObj;
    [SerializeField] private GameObject heartObj;
    [SerializeField] private Vector2 spawnPointOffsetToTriggerPosition;
    [SerializeField] private Vector2 hiddenPoolPoint;

    private void Start()
    {
        MoveToHiddenPoint(heartObj);
        MoveToHiddenPoint(starObj);
    }

    public void MoveToHiddenPoint(GameObject _gameobj)
    {
        _gameobj.transform.position = hiddenPoolPoint;
        _gameobj.GetComponent<CollactableHover>().enabled = false;
    }
    public void MoveToSpawnPoint(Vector3 _triggerPosition)
    {
        System.Random rnd = new System.Random();
        GameObject obj = heartObj;
        int temp = rnd.Next(0, 100);
        if (temp < 20)
        {
            obj = starObj;
        }
        if(temp >= 20)
        {
            obj = heartObj;
        }
        Vector2 spawnPoint = _triggerPosition + new Vector3(spawnPointOffsetToTriggerPosition.x, spawnPointOffsetToTriggerPosition.y, 0);
        obj.transform.position = spawnPoint;
        obj.GetComponent<CollactableHover>().enabled = true;
    }


}
