using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

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
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private AnimatorController playerSwordAnimator;
    [SerializeField] private AnimatorController playerHammerAnimator;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator playerAnimator;
    bool wasWalking;

    [SerializeField] private Animator goblinAnimator;

    [SerializeField] private Animator orkAnimator;

    [SerializeField] private Animator ogerAnimator;
    private bool repeat;

    private void Start()
    {
        if (GameManager.Instance.weapon == 0)
        {
            player.GetComponent<Animator>().runtimeAnimatorController = playerSwordAnimator;
        }
        if (GameManager.Instance.weapon == 1)
        {
            player.GetComponent<Animator>().runtimeAnimatorController = playerHammerAnimator;
        }
        playerAnimator = player.GetComponent<Animator>();
    }

    private void Update()
    {
        if(repeat)
        {
            playerAnimator.SetBool("Dead", false);
            goblinAnimator.SetBool("Dead", false);
            ogerAnimator.SetBool("Dead", false);
            orkAnimator.SetBool("Dead", false);
            repeat = false;
        }
    }

    #region Player
    public void PlayerShouldWalk(float _speed, float _walkSpeed)
    {
        playerAnimator.SetBool("Attacking", false);
        playerAnimator.SetFloat("Speed", _speed / _walkSpeed);
        playerAnimator.SetBool("Walking", true);
    }
    public void PlayerShouldIdle()
    {
        playerAnimator.SetBool("Attacking", false);
        playerAnimator.SetBool("Walking", false);
    }
    public void PlayerShouldAttack()
    {
        playerAnimator.SetBool("Attacking", true);
    }
    public void PlayerShouldDie()
    {
        playerAnimator.SetBool("Dead", true);
        repeat = true;
    }
    #endregion

    #region Goblin

    public void GoblinShouldWalk()
    {
        goblinAnimator.SetBool("Attacking", false);
        goblinAnimator.SetBool("Walking", true);
    }
    public void GoblinShouldIdle()
    {
        goblinAnimator.SetBool("Attacking", false);
        goblinAnimator.SetBool("Walking", false);
    }
    public void GoblinShouldAttack()
    {
        goblinAnimator.SetBool("Attacking", true);
    }
    public void GoblinShouldDie()
    {
        goblinAnimator.SetBool("Dead", true);
        repeat = true;
    }
    #endregion

    #region Oger

    public void OgerShouldWalk()
    {
        ogerAnimator.SetBool("Attacking", false);
        ogerAnimator.SetBool("Walking", true);
    }
    public void OgerShouldIdle()
    {
        ogerAnimator.SetBool("Attacking", false);
        ogerAnimator.SetBool("Walking", false);
    }
    public void OgerShouldAttack()
    {
        ogerAnimator.SetBool("Attacking", true);
    }
    public void OgerShouldDie()
    {
        ogerAnimator.SetBool("Dead", true);
        repeat = true;
    }
    #endregion

    #region Ork

    public void OrkShouldWalk()
    {
        orkAnimator.SetBool("Attacking", false);
        orkAnimator.SetBool("Walking", true);
    }
    public void OrkShouldIdle()
    {
        orkAnimator.SetBool("Attacking", false);
        orkAnimator.SetBool("Walking", false);
    }
    public void OrkShouldAttack()
    {
        orkAnimator.SetBool("Attacking", true);
    }
    public void OrkShouldDie()
    {
        orkAnimator.SetBool("Dead", true);
        repeat = true;
    }
    #endregion
}
