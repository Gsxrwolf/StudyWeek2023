using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    [SerializeField] public GameObject player; // player für position

    [SerializeField] private float speed = 50;
    [SerializeField] public float health = 50;
    [SerializeField] private float damage = 10;

    [SerializeField] public float scorePoints;

    private Rigidbody2D rB;
    private bool isAttacking = false;
    private bool left;
    private bool right;

    private void Start()
    {
        rB = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isAttacking)
        {
            LeftOrRight();
            WalkToPlayer();
        }
    }

    private void WalkToPlayer()
    {
        if (left)
        {
            rB.AddForce(Vector2.left * speed * Time.deltaTime);
        }
        if (right)
        {
            rB.AddForce(Vector2.right * speed * Time.deltaTime);
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
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            collision.gameObject.GetComponent<PlayerController>().DealDamage(damage);
            Attack();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
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
