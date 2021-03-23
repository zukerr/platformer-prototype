using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public static Door Instance;

    [SerializeField]
    private GameObject closedDoorGameobject = null;
    [SerializeField]
    private Collider2D endLevelTrigger = null;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void Open()
    {
        closedDoorGameobject.SetActive(false);
        endLevelTrigger.enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //end level
            Debug.Log("Active scene build index: " + SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Scene count: " + SceneManager.sceneCountInBuildSettings);
            Debug.Log("Loading scene of index: " + (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        }
    }
}
