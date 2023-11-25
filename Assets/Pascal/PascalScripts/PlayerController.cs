using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] private GameObject damageBlockParticleSystem;

    [SerializeField] private UnityEvent<float, float> onHealthChange;




    private void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        maxHealth = health;
    }

    private void Update()
    {
        AnimManager.Instance.PlayerShouldIdle();

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


    private void InputCheck()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            playerTempPos = transform.position;
            playerTempPos.x -= curSpeed * Time.deltaTime;
            transform.position = playerTempPos;
            AnimManager.Instance.PlayerShouldWalk(curSpeed, normalSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1, 1);
            playerTempPos = transform.position;
            playerTempPos.x += curSpeed * Time.deltaTime;
            transform.position = playerTempPos;
            AnimManager.Instance.PlayerShouldWalk(curSpeed, normalSpeed);
        }
        if(Input.GetMouseButtonDown(0))
        {
            AnimManager.Instance.PlayerShouldAttack();
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
                AnimManager.Instance.PlayerShouldDie();
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
        if(Input.GetMouseButtonDown(0))
        {
            if (collision.CompareTag("Goblin") || collision.CompareTag("Oger") || collision.CompareTag("Ork"))
            {
                collision.gameObject.GetComponent<Enemy>().DealDamage(damage);
            }
        }
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
