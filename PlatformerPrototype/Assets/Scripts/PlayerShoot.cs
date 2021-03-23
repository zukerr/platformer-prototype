using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private GameObject bulletPrefab = null;
    [SerializeField]
    private Transform barrelEnd = null;
    [SerializeField]
    private PlayerMovement playerMovement = null;
    [SerializeField]
    private float bulletSpeed = 1f;
    [SerializeField]
    private List<AudioClip> gunShotAudioClips = null;
    [SerializeField]
    private AudioSource gunAudioSource = null;

    private PlatformerPrototype inputActions;

    private void OnEnable()
    {
        inputActions = new PlatformerPrototype();
        inputActions.Player.Fire.performed += HandleShooting;
        inputActions.Player.Fire.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Fire.Disable();
    }

    public void HandleShooting(InputAction.CallbackContext context)
    {
        GameObject createdBullet = Instantiate(bulletPrefab, barrelEnd.position, Quaternion.identity, null);

        Vector2 shootingVector = playerMovement.FacingRight ? Vector2.right : Vector2.left;

        createdBullet.GetComponent<Rigidbody2D>().AddForce(shootingVector * bulletSpeed);
        PlayGunshotSound();
        animator.SetTrigger("shot_trigger");
    }

    private void PlayGunshotSound()
    {
        int rng = Random.Range(0, gunShotAudioClips.Count);
        gunAudioSource.clip = gunShotAudioClips[rng];
        gunAudioSource.Play();
    }
}
