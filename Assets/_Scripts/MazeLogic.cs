using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MazeLogic : MonoBehaviour
{
    // references
    [SerializeField]
    public Text remainingTimeText;
    float secondsRemaining;

    // Start is called before the first frame update
    void Start()
    {
        secondsRemaining = 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (secondsRemaining > 0.0f)
        {
            secondsRemaining -= Time.deltaTime;
        }
        else
        {
            // save score here, kick to stats menu
            SceneManager.LoadScene("End Scene");
        }

        // round seconds from float to int, convert to string
        remainingTimeText.text = "Find a checkpoint in less than " + Mathf.RoundToInt(secondsRemaining).ToString() + " seconds.";


    }
}
