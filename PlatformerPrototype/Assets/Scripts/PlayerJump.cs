using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody = null;
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private float jumpForce = 1f;
    [SerializeField]
    private float jumpTimer = 1f;
    [SerializeField]
    private LayerMask groundLayer = 0;
    [SerializeField]
    private float groundDistance = 0.6f;
    [SerializeField]
    private float groundDiagonalDetectionFactor = 0.2f;
    [SerializeField]
    private KeyCode jumpKeybind = KeyCode.Space;
    [SerializeField]
    private float fallMultiplier = 5f;
    [SerializeField]
    private float gravity = 1f;
    [SerializeField]
    private float linearDrag = 5f;

    public bool IsFalling { get; private set; } = false;

    private bool isGrounded = true;
    private bool isJumping = false;
    private float currentJumpTime = 0f;

    public bool IsJumping => isJumping;

    private void Update()
    {
        HandleJump();
    }

    public void HandleJump()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);
        if(!isGrounded)
        {
            bool rightGround = Physics2D.Raycast(transform.position, new Vector2(groundDiagonalDetectionFactor, -1), groundDistance, groundLayer);
            bool leftGround = Physics2D.Raycast(transform.position, new Vector2(-groundDiagonalDetectionFactor, -1), groundDistance, groundLayer);
            if (leftGround || rightGround)
            {
                isGrounded = true;
            }
        }


        if (isGrounded && Input.GetKeyDown(jumpKeybind))
        {
            Debug.Log("jumping.");
            isJumping = true;
            animator.SetBool("is_jumping", true);
            currentJumpTime = jumpTimer;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if(currentJumpTime > 0)
        {
            currentJumpTime -= Time.deltaTime;
        }

        if(isGrounded)
        {
            if(currentJumpTime <= 0)
            {
                isJumping = false;
                animator.SetBool("is_jumping", false);
                IsFalling = false;
                /*
                if(rigidBody.gameObject.layer == GlobalVariables.FALLING_LAYER)
                {
                    if(rigidBody.gameObject.transform.parent == null)
                    {
                        rigidBody.gameObject.layer = GlobalVariables.PLAYER_LAYER;
                    }
                }
                */
            }
            rigidBody.gravityScale = 1;
            rigidBody.drag = linearDrag;
        }
        else
        {
            rigidBody.gravityScale = gravity;
            rigidBody.drag = 0.8f * linearDrag;
            if(rigidBody.velocity.y < 0)
            {
                rigidBody.gravityScale = gravity * fallMultiplier;
                IsFalling = true;
                //rigidBody.gameObject.layer = GlobalVariables.FALLING_LAYER;
            }
            /*
            else if(rigidBody.velocity.y > 0 && !Input.GetKey(jumpKeybind))
            {
                rigidBody.gravityScale = gravity * (fallMultiplier / 2);
            }
            */
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundDistance);
        Gizmos.DrawLine(transform.position, transform.position + (new Vector3(groundDiagonalDetectionFactor, -1)) * groundDistance);
        Gizmos.DrawLine(transform.position, transform.position + (new Vector3(-groundDiagonalDetectionFactor, -1)) * groundDistance);
    }

}
