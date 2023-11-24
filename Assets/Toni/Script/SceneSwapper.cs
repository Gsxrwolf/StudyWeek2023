using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void PressOnExit()
    {
        Application.Quit();
    }

    public void OnMUteButtonPress()
    {
    GameManager.Instance.isMuted = !GameManager.Instance.isMuted;
    }

    public void ChooseWeapon(int _Weapon)
    {
    GameManager.Instance.weapon = _Weapon;
    }
}