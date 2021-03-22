using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private GameObject bulletPrefab = null;
    [SerializeField]
    private Transform barrelEnd = null;
    [SerializeField]
    private KeyCode shootKeybind = KeyCode.Mouse0;
    [SerializeField]
    private PlayerMovement playerMovement = null;
    [SerializeField]
    private float bulletSpeed = 1f;
    [SerializeField]
    private List<AudioClip> gunShotAudioClips = null;
    [SerializeField]
    private AudioSource gunAudioSource = null;

    private void Update()
    {
        HandleShooting();
    }

    public void HandleShooting()
    {
        if(Input.GetKeyDown(shootKeybind))
        {
            GameObject createdBullet = Instantiate(bulletPrefab, barrelEnd.position, Quaternion.identity, null);
            //createdBullet.transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);

            Vector2 shootingVector = playerMovement.FacingRight ? Vector2.right : Vector2.left;

            createdBullet.GetComponent<Rigidbody2D>().AddForce(shootingVector * bulletSpeed);
            PlayGunshotSound();
            animator.SetTrigger("shot_trigger");
        }
    }

    private void PlayGunshotSound()
    {
        int rng = Random.Range(0, gunShotAudioClips.Count);
        gunAudioSource.clip = gunShotAudioClips[rng];
        gunAudioSource.Play();
    }
}
