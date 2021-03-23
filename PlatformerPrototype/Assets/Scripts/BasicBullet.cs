using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
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
