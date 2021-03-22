using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement = null;
    [SerializeField]
    private PlayerJump playerJump = null;
    [SerializeField]
    private PlayerShoot playerShoot = null;

    public PlayerMovement PlayerMovementModule => playerMovement;
    public PlayerJump PlayerJumpModule => playerJump;
    public PlayerShoot PlayerShootModule => playerShoot;
}
