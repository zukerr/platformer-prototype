using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody = null;
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float maxSpeed = 10f;

    private bool isWalking = false;
    private bool facingRight = true;

    public bool FacingRight => facingRight;

    // Update is called once per frame
    private void Update()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        //Vector2 movementVector = GlobalVariables.GetPlayerToMouseVector();

        movementVector.Normalize();

        if((movementVector.x > 0 && !facingRight) || (movementVector.x < 0 && facingRight))
        {
            Flip();
        }

        if (movementVector != Vector2.zero)
        {
            isWalking = true;
            animator.SetBool("is_walking", true);
        }
        else
        {
            isWalking = false;
            animator.SetBool("is_walking", false);
        }
        rigidBody.AddForce(movementVector * speed);
        //rigidBody.velocity = new Vector2(movementVector.x * speed, rigidBody.velocity.y);


        if (Mathf.Abs(rigidBody.velocity.x) > maxSpeed)
        {
            rigidBody.velocity = new Vector2(Mathf.Sign(rigidBody.velocity.x) * maxSpeed, rigidBody.velocity.y);
        }
    }

    private void Flip()
    {
        if(rigidBody.transform.rotation.eulerAngles.y == 0)
        {
            rigidBody.transform.rotation = Quaternion.Euler(rigidBody.transform.rotation.eulerAngles.x, -180f, rigidBody.transform.rotation.eulerAngles.z);
            facingRight = false;
        }
        else
        {
            rigidBody.transform.rotation = Quaternion.Euler(rigidBody.transform.rotation.eulerAngles.x, 0f, rigidBody.transform.rotation.eulerAngles.z);
            facingRight = true;
        }
    }
}
