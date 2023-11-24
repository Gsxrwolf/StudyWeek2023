
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Oger : MonoBehaviour
{
    [SerializeField]
    public int speed = 2;
    public int health = 100;
    public int damage = 15;

    public static bool isStanding = false;

    private bool stayStill = false;

    public static bool isAlive = true;

    [SerializeField] public GameObject gameobject;

    private SpriteRenderer m_spriteRenderer;

    private Rigidbody2D boxcollider;

    private PlayerController m_playerController;


    float time = 0.0f;
    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();


        boxcollider = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        time += Time.deltaTime;
        if (health == 0 || health <= 0) // die 
        {
            Destroy(gameObject);
            isAlive = false;
        }


        if (gameObject.transform.position.x <= transform.position.x)
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 1f);
            //  m_spriteRenderer.flipX = true;
        }
        else
        {
            transform.localScale = new Vector3(-0.4f, 0.4f, 1f);
            //  m_spriteRenderer.flipX = false;
        }
        // is True if Enemy is on Bottom 
        if (isStanding)
        {
            AnimManager.Instance.OgerShouldWalk();
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(gameobject.transform.position.x, transform.position.y, gameobject.transform.position.z), speed * Time.deltaTime); // Follow the player                                                                                                                                //  isStanding = false;
        }

        if (transform.position.x == gameobject.transform.position.x)
        {
            isStanding = false;
            Invoke("OnCollisionEnter2D", 2f);
        }

    }

    public static void AfterAnim()
    {
        isStanding = true;
        AnimManager.Instance.GoblinShouldWalk();
    }
    public void GetDamage(int _damage) // get Damage from Player && Event !?
    {
        if (isAlive)
        {
            health = health - _damage;
        }
    }
    public void isTrue()
    {
        isStanding = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bottom"))
        {
            isStanding = true;
        }


        if (collision.gameObject.CompareTag("Player"))
        {
            isStanding = false;
            AnimManager.Instance.OgerShouldAttack();
            collision.gameObject.GetComponent<PlayerController>().DealDamage(damage);
            isStanding = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            //isStanding = false ( bleibt stehen ) und animation starten

            //  pC = collision.GetComponent<PlayerController>();


            //PlayerController.DealDamage(melee);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


    }
}

