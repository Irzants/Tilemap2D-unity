using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 5f;
    Rigidbody2D myRb;
    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }


    void Update()
    {
        myRb.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag ==  "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D cother)
    {
        Destroy(gameObject);
    }



}
