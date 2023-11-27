using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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

    private void Start()
    {
        rB = this.GetComponent<Rigidbody2D>();
        originScale = transform.localScale;
    }

    void Update()
    {
        Idle();
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

    public void DealDamage(float _damage) // get Damage from Player && Event !?
    {
        health -= _damage;
        if (health <= 0)
        {
            health = 0;
            GameManager.Instance.curScore += scorePoints;
            Die();
            this.gameObject.SetActive(false);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().attackTrigger != collision)
            {
                isAttacking = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
            attackTimerTicks = attackTimer;
        }
    }

    public void Idle()
    {
        if (CompareTag("Goblin"))
        {
            AnimManager.Instance.GoblinShouldIdle();
        }
        if (CompareTag("Oger"))
        {
            AnimManager.Instance.OgerShouldIdle();
        }
        if (CompareTag("Ork"))
        {
            AnimManager.Instance.OrkShouldIdle();
        }
    }
    public void Walk()
    {
        if (CompareTag("Goblin"))
        {
            AnimManager.Instance.GoblinShouldWalk();
        }
        if (CompareTag("Oger"))
        {
            AnimManager.Instance.OgerShouldWalk();
        }
        if (CompareTag("Ork"))
        {
            AnimManager.Instance.OrkShouldWalk();
        }
    }
    public void Attack()
    {
        if (CompareTag("Goblin"))
        {
            AnimManager.Instance.GoblinShouldAttack();
        }
        if (CompareTag("Oger"))
        {
            AnimManager.Instance.OgerShouldAttack();
        }
        if (CompareTag("Ork"))
        {
            AnimManager.Instance.OrkShouldAttack();
        }
    }
    public void Die()
    {
        if (CompareTag("Goblin"))
        {
            AnimManager.Instance.GoblinShouldDie();
        }
        if (CompareTag("Oger"))
        {
            AnimManager.Instance.OgerShouldDie();
        }
        if (CompareTag("Ork"))
        {
            AnimManager.Instance.OrkShouldDie();
        }
    }
}
