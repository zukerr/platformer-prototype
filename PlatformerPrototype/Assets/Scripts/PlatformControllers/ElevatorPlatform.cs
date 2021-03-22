using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            /*
            PlayerJump playerJump = collision.gameObject.GetComponent<PlayerController>().PlayerJumpModule;
            
            if (playerJump.IsFalling || playerJump.IsJumping)
            {
                collision.transform.SetParent(transform);
            }
            */
            //GetComponent<PlatformEffector2D>().
            //if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
            //{
                
            //}
            collision.transform.SetParent(transform);
            //collision.gameObject.layer = GlobalVariables.ON_PLATFORM_LAYER;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
            //collision.gameObject.layer = GlobalVariables.PLAYER_LAYER;
        }
    }
}
