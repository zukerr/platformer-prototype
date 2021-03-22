using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    /*
    private float raycastDistance = 0.01f;

    private void FixedUpdate()
    {
        RaycastHit2D destructibleAhead = Physics2D.Raycast(transform.position, GetComponent<Rigidbody2D>().velocity.normalized, raycastDistance, GlobalVariables.DESTRUCTIBLE_LAYER);
        if(destructibleAhead)
        {
            if (destructibleAhead.collider.gameObject.layer == GlobalVariables.DESTRUCTIBLE_LAYER)
            {
                Destroy(destructibleAhead.collider.gameObject);
                Destroy(gameObject);
            }            
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet collided!");
        if(!collision.isTrigger)
        {
            if (collision.gameObject.layer == GlobalVariables.DESTRUCTIBLE_LAYER)
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
