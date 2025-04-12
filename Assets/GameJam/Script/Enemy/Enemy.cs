using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int EnemyHp = 10;
    public float Speed = 3f;
    public bool PlayerChack;
    public bool CanHit;
    public float HitDelay = 0.2f;
    public GameObject Partycle;


    public GameObject Soul;
    public GameObject Player;
    public Rigidbody2D rd;
    public SpriteRenderer sr;
    void Start()
    {
        PlayerChack = false;
        CanHit = true;
        rd = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

    void Move()
    {
        Player = GameObject.Find("Player");
        if (PlayerChack)
        {
            if (Player.transform.position.x < transform.position.x)
            {
                sr.flipX = true;
                rd.velocity = new Vector2(Speed, rd.velocity.y);
            }
                
            if (Player.transform.position.x > transform.position.x)
            {
                sr.flipX = false;
                rd.velocity = new Vector2(-Speed, rd.velocity.y);
            }
                
        }

    }

    public void PlayerCheck(bool can)
    {
        PlayerChack = can;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Player.GetComponent<Player>().isActtack == true && CanHit)
        {
            StartCoroutine(Hit());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Player.GetComponent<Player>().isActtack == true && CanHit)
        {
            StartCoroutine(Hit());
        }
    }

    IEnumerator Hit()
    {
        SoundManager.Instance.PlayEffect(2);
        CanHit = false;
        EnemyHp -= Player.GetComponent<Player>().PlayerDamage;
        if (EnemyHp <= 0)
            Death();
        yield return new WaitForSeconds(HitDelay);
        CanHit = true;
        
    }

    void DropSoul()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Soul,new Vector3(transform.position.x,transform.position.y,-6),Quaternion.identity);
        }
    }

    void Death()
    {
        Player.GetComponent<Player>().PlayerHp += 20;
        var obj = Instantiate(Partycle, new Vector3(transform.position.x, transform.position.y,-5), Quaternion.identity);
        DropSoul();
        Destroy(obj,3f);
        Destroy(gameObject);
    }

}
