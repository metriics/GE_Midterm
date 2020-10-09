using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // references
    [SerializeField]
    public GameObject playButton;
    public GameObject statsButton;

    // Start is called before the first frame update
    void Start()
    {
        // we can use this later to load in stats from previous sessions
        Cursor.lockState = CursorLockMode.None;
    }

    public void scenePlay()
    {
        SceneManager.LoadScene("Play Scene");
    }

    public void sceneStats()
    {
        SceneManager.LoadScene("End Scene");
    }
}
