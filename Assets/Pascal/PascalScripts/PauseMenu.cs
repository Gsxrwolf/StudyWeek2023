using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool paused;
    private GameObject UIToggel;

    private void Start()
    {
        UIToggel = gameObject.transform.GetChild(0).gameObject;
        UIToggel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }
        if (paused)
        {
            Pause();
        }
        if (!paused)
        {
            UnPause();
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        UIToggel.SetActive(true);
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
        UIToggel.SetActive(false);
    }
    public void onReturn()
    {
        paused = false;
    }
    public void onMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void onRestart()
    {
        SceneManager.LoadScene(6);
    }
}
