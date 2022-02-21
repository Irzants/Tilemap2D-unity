using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    Rigidbody2D myRb;


    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        myRb.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRb.velocity.x)), 1f);
    }
}
