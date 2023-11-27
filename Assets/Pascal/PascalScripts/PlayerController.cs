using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Vector2 playerTempPos;
    private Rigidbody2D rB;

    private float curSpeed = 2f;
    [SerializeField] public float normalSpeed = 2f;
    [SerializeField] public float sprintSpeed = 4f;
    [SerializeField] public float jumpForce = 1.5f;
    public bool jumpBlock = false;

    [SerializeField] private float health;
    private float maxHealth;
    public bool damageBlock;
    private float damageBlockTime;

    [SerializeField] private float damage;

    [SerializeField] private GameObject groundRayCastOrigin;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayCastLeangth;
    private bool grounded = true;

    public bool isDead;
    [SerializeField] private float deathCountdownTime;
    private float deathCountdown;

    [SerializeField] public Collider2D upperBody;
    [SerializeField] public Collider2D lowerBody;
    [SerializeField] public Collider2D attackTrigger;

    [SerializeField] private GameObject damageBlockParticleSystem;
    private Animator animator;
    [SerializeField] private AnimatorController playerSwordAnimator;
    [SerializeField] private AnimatorController playerHammerAnimator;

    [SerializeField] private UnityEvent<float, float> onHealthChange;

    private Vector3 originScale;
    private bool repeat;

    private void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        maxHealth = health;
        originScale = transform.localScale;
        deathCountdown = deathCountdownTime;
        animator = GetComponent<Animator>();
        if (GameManager.Instance.weapon == 0)
        {
            animator.runtimeAnimatorController = playerSwordAnimator;
        }
        if (GameManager.Instance.weapon == 1)
        {
            animator.runtimeAnimatorController = playerHammerAnimator;
        }
    }

    private void Update()
    {
        if (repeat)
        {
            animator.SetBool("Dead", false);
            repeat = false;
        }
        Idle();
        if(!isDead)
        {
            if (damageBlockTime != 0)
            {
                damageBlockTime -= 1 * Time.deltaTime;
            }
            if (damageBlockTime < 0)
            {
                damageBlockTime = 0;
            }
            if (damageBlockTime == 0)
            {
                damageBlockParticleSystem.SetActive(false);
                damageBlock = false;
            }

            SprintCheck();
            InputCheck();
            CheckGrounded();
            if (!jumpBlock)
            {
                JumpCheck();
            }
        }
        else
        {
            if(deathCountdown <= 0)
            {
                SceneManager.LoadScene(7);
            }
            else
            {
                deathCountdown -= 1 * Time.deltaTime;
            }
        }
    }


    private void InputCheck()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            TurnTo("left");
            playerTempPos = transform.position;
            playerTempPos.x -= curSpeed * Time.deltaTime;
            transform.position = playerTempPos;
            Walk(curSpeed, normalSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            TurnTo("right");
            playerTempPos = transform.position;
            playerTempPos.x += curSpeed * Time.deltaTime;
            transform.position = playerTempPos;
            Walk(curSpeed, normalSpeed);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void TurnTo(string _direction)
    {
        if (_direction == "left")
        {
            transform.localScale = new Vector3(originScale.x * -1, originScale.y, originScale.z);
        }
        if (_direction == "right")
        {
            transform.localScale = originScale;
        }
    }

    public void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
            //animator.SetBool("jumping", true);
            Vector2 force = Vector2.zero;
            force.y += jumpForce;
            rB.AddForce(force, ForceMode2D.Impulse);
        }
    }
    public void SprintCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            curSpeed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            curSpeed = normalSpeed;
        }
    }

    public void SpeedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            curSpeed = sprintSpeed;
        }
        else
        {
            curSpeed = normalSpeed;
        }
    }
    public void DamageBlock(float _time)
    {
        damageBlock = true;
        damageBlockTime = _time;
        damageBlockParticleSystem.SetActive(true);
    }
    public void DealDamage(float _damage)
    {
        if (!damageBlock)
        {
            health -= _damage;
            if (health < 0)
            {
                health = 0;
                isDead = true;
                GameManager.Instance.OnPlayerDeath();
                Die();
            }
        }
        onHealthChange.Invoke(health, maxHealth);
    }
    public void HealHealth(float _healAmount)
    {
        health += _healAmount;
        if (health > maxHealth) health = maxHealth;
        onHealthChange.Invoke(health, maxHealth);
    }

    public float GetHealth()
    {
        return health;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (collision.CompareTag("Goblin") || collision.CompareTag("Oger") || collision.CompareTag("Ork"))
            {
                if (collision.isTrigger == false)
                {
                    collision.gameObject.GetComponent<Enemy>().DealDamage(damage);
                }
            }
        }
    }

    public void Walk(float _speed, float _walkSpeed)
    {
        animator.SetBool("Attacking", false);
        animator.SetFloat("Speed", _speed / _walkSpeed);
        animator.SetBool("Walking", true);
    }
    public void Idle()
    {
        animator.SetBool("Attacking", false);
        animator.SetBool("Walking", false);
    }
    public void Attack()
    {
        animator.SetBool("Attacking", true);
    }
    public void Die()
    {
        animator.SetBool("Dead", true);
        repeat = true;
    }

    public void Step()
    {
        //walkSound.Play();
    }

    public void Jump()
    {
        //System.Random rnd = new System.Random();
        //int temp = rnd.Next(0, 2);
        //if (temp == 0) jumpSound1.Play();
        //if (temp == 1) jumpSound2.Play();
    }

    private void CheckGrounded()
    {
        grounded = Physics2D.Raycast(groundRayCastOrigin.transform.position, Vector2.down, rayCastLeangth, groundLayer);
        Debug.DrawRay(groundRayCastOrigin.transform.position, Vector2.down);
    }
}
