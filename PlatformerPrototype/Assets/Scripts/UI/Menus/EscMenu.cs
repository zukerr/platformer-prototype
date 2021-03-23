using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject escMenuGameObject = null;

    private PlatformerPrototype inputActions;

    private void OnEnable()
    {
        inputActions = new PlatformerPrototype();
        inputActions.Player.EscMenu.performed += x => Toggle();
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void Toggle()
    {
        if(!escMenuGameObject.activeSelf)
        {
            escMenuGameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            escMenuGameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
