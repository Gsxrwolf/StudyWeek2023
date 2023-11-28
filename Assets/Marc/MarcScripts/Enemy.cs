using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] public GameObject player; // player für position

    [SerializeField] private float speed;
    [SerializeField] public float health;
    [SerializeField] private float damage;
    [SerializeField] private float attackTimer;
    private float attackTimerTicks;

    [SerializeField] public float scorePoints;

    [SerializeField] public Collider2D upperBody;
    [SerializeField] public Collider2D lowerBody;
    [SerializeField] public Collider2D attackTrigger;

    private Rigidbody2D rB;
    private bool isAttacking = false;
    private bool left;
    private bool right;
    private Vector3 enemyTempPos;
    private Vector3 originScale;
    private bool pIsDead;
    private Collider2D collision;
    private bool pInRange;
    [SerializeField] private float deathCountdownTime;
    private float deathCountdown;
    private bool isDead;

    private Animator animator;
    private bool repeat;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rB = this.GetComponent<Rigidbody2D>();
        originScale = transform.localScale;
        deathCountdown = deathCountdownTime;
    }

    void Update()
    {
        if (repeat)
        {
            animator.SetBool("Dead", false);
            repeat = false;
        }
        Idle();
        if (!isDead)
        {
            if (pInRange)
            {
                pIsDead = collision.gameObject.GetComponent<PlayerController>().isDead;
                if (collision.isTrigger == false)
                {
                    isAttacking = true;
                }
            }
            if (!pIsDead)
            {
                if (!isAttacking)
                {
                    LeftOrRight();
                    WalkToPlayer();
                }
                if (isAttacking)
                {
                    if (attackTimerTicks < 0)
                    {
                        player.GetComponent<PlayerController>().DealDamage(damage);
                        attackTimerTicks = attackTimer;
                        Attack();
                    }
                    else
                    {
                        attackTimerTicks -= 1 * Time.deltaTime;
                    }
                }
            }
        }
        else
        {
            if (deathCountdown <= 0)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                deathCountdown -= 1 * Time.deltaTime;
            }
        }
    }


    private void WalkToPlayer()
    {
        if (left)
        {
            TurnTo("left");
            enemyTempPos = transform.position;
            enemyTempPos.x -= speed * Time.deltaTime;
            transform.position = enemyTempPos;
            Walk();
        }
        if (right)
        {
            TurnTo("right");
            enemyTempPos = transform.position;
            enemyTempPos.x += speed * Time.deltaTime;
            transform.position = enemyTempPos;
            Walk();
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

    private void LeftOrRight()
    {
        if (player.transform.position.x < transform.position.x)
        {
            left = true;
            right = false;
        }
        if (player.transform.position.x > transform.position.x)
        {
            right = true;
            left = false;
        }
    }

    public void DealDamage(float _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            health = 0;
            isDead = true;
            GameManager.Instance.OnMobDeath(scorePoints);
            Die();
        }
    }


    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            pInRange = true;
            collision = _collision;
        }
    }
    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            if (_collision.isTrigger == false)
            {
                isAttacking = false;
                pInRange = false;
                attackTimerTicks = attackTimer;
            }
        }
    }

    public void Idle()
    {
        animator.SetBool("Attacking", false);
        animator.SetBool("Walking", false);
        animator.SetBool("Dead", false);
    }
    public void Walk()
    {
        animator.SetBool("Attacking", false);
        animator.SetBool("Walking", true);
        animator.SetBool("Dead", false);
    }
    public void Attack()
    {
        animator.SetBool("Attacking", true);
        animator.SetBool("Dead", false);
    }
    public void Die()
    {
        animator.SetBool("Dead", true);
        repeat = true;
    }
}
