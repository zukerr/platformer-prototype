using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlatformController : MonoBehaviour
{
    [SerializeField]
    private GameObject elevatorPlatform = null;
    [SerializeField]
    private Transform elevatorPlatformSurface = null;
    [SerializeField]
    private Transform elevatorTop = null;
    [SerializeField]
    private Transform elevatorBottom = null;
    [SerializeField]
    private float elevationSpeed = 1f;
    [SerializeField]
    private float minTargetDistance = 0.1f;
    [SerializeField]
    private bool vertical = true;

    private bool movingUp = true;
    private Vector3 toVector;
    private Vector3 fromVector;


    private void Awake()
    {
        if(vertical)
        {
            toVector = Vector3.up;
            fromVector = Vector3.down;
        }
        else
        {
            toVector = Vector3.right;
            fromVector = Vector3.left;
        }
    }

    private void Update()
    {
        MoveElevator();
    }

    private void MoveElevator()
    {
        if(movingUp)
        {
            elevatorPlatform.transform.Translate(toVector * elevationSpeed * Time.deltaTime);
            if(Vector3.Distance(elevatorPlatformSurface.position, elevatorTop.position) < minTargetDistance)
            {
                movingUp = false;
            }
        }
        else
        {
            elevatorPlatform.transform.Translate(fromVector * elevationSpeed * Time.deltaTime);
            if (Vector3.Distance(elevatorPlatformSurface.position, elevatorBottom.position) < minTargetDistance)
            {
                movingUp = true;
            }
        }
    }
}
