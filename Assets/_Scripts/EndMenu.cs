using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    // references
    [SerializeField]
    public GameObject mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        // we can use this later to load in stats from previous sessions
    }

    public void sceneMenu()
    {
        SceneManager.LoadScene("Start Scene");
    }
}
