using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    public static AnimManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    [SerializeField] private AnimatorController playerSwordAnimator;
    [SerializeField] private AnimatorController playerHammerAnimator;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator playerAnimator;
    bool wasWalking;

    [SerializeField] private Animator goblinAnimator;

    [SerializeField] private Animator orkAnimator;

    [SerializeField] private Animator ogerAnimator;

    private void Start()
    {
        if(GameManager.Instance.weapon == 0)
        {
            //player.GetComponent<Animator>().
        }
        if (GameManager.Instance.weapon == 1)
        {
            //player.GetComponent<Animator>().
        }
        playerAnimator = player.GetComponent<Animator>();
    }

    #region Player
    public void PlayerShouldWalk()
    {
        playerAnimator.SetBool("Walking", true);
    }
    public void PlayerShouldIdle()
    {
        playerAnimator.SetBool("Walking", false);
    }
    public void PlayerShouldAttack()
    {
        playerAnimator.SetBool("Attacking", true);
    }
    public void PlayerShouldDie()
    {
        playerAnimator.SetBool("Dead", true);
    }
    #endregion

    #region Goblin

    public void GoblinShouldWalk()
    {
        goblinAnimator.SetBool("Walking", true);
    }
    public void GoblinShouldIdle()
    {
        goblinAnimator.SetBool("Walking", false);
    }
    public void GoblinShouldAttack()
    {
        goblinAnimator.SetBool("Attacking", true);
    }
    public void GoblinShouldDie()
    {
        goblinAnimator.SetBool("Dead", true);
    }
    #endregion

    #region Oger

    public void OgerShouldWalk()
    {
        ogerAnimator.SetBool("Walking", true);
    }
    public void OgerShouldIdle()
    {
        ogerAnimator.SetBool("Walking", false);
    }
    public void OgerShouldAttack()
    {
        ogerAnimator.SetBool("Attacking", true);
    }
    public void OgerShouldDie()
    {
        ogerAnimator.SetBool("Dead", true);
    }
    #endregion

    #region Ork

    public void OrkShouldWalk()
    {
        orkAnimator.SetBool("Walking", true);
    }
    public void OrkShouldIdle()
    {
        orkAnimator.SetBool("Walking", false);
    }
    public void OrkShouldAttack()
    {
        orkAnimator.SetBool("Attacking", true);
    }
    public void OrkShouldDie()
    {
        orkAnimator.SetBool("Dead", true);
    }
    #endregion
}
