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

    private bool movingUp = true;

    private void Update()
    {
        MoveElevator();
    }

    private void MoveElevator()
    {
        if(movingUp)
        {
            elevatorPlatform.transform.Translate(Vector3.up * elevationSpeed * Time.deltaTime);
            if(Vector3.Distance(elevatorPlatformSurface.position, elevatorTop.position) < minTargetDistance)
            {
                movingUp = false;
            }
        }
        else
        {
            elevatorPlatform.transform.Translate(Vector3.down * elevationSpeed * Time.deltaTime);
            if (Vector3.Distance(elevatorPlatformSurface.position, elevatorBottom.position) < minTargetDistance)
            {
                movingUp = true;
            }
        }
    }
}
