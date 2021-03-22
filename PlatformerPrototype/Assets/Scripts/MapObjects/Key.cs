using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private GameObject spriteHolderGameObject = null;
    [SerializeField]
    private float rotationSpeed = 1f;
    [SerializeField]
    private AudioSource doorOpeningSFX = null;

    private void Update()
    {
        Vector3 spriteRotation = spriteHolderGameObject.transform.rotation.eulerAngles;
        spriteHolderGameObject.transform.rotation = Quaternion.Euler(spriteRotation.x, spriteRotation.y + Time.deltaTime * rotationSpeed, spriteRotation.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //open the door
            Door.Instance.Open();
            doorOpeningSFX.Play();
            doorOpeningSFX.gameObject.transform.SetParent(null);
            Destroy(gameObject);            
        }
    }
}
