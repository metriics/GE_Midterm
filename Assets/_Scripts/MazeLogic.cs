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
    public GameObject checkpointParent;
    List<GameObject> checkpoints = new List<GameObject>();
    GameObject currentCheckpoint;
    public Material green;
    public GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        secondsRemaining = 60.0f;

        // get all children of checkpointParent and store them in checkpoints list
        foreach (Transform child in checkpointParent.transform)
        {
            if (child.gameObject.tag == "checkpoint")
            {
                checkpoints.Add(child.gameObject);
            }
        }
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


        // REMOVE LATER
        if (Input.GetKeyDown(KeyCode.R))
        {
            Death();
        }
    }

    public void checkpointCallback(GameObject checkpoint)
    {
        Debug.Log("collision with: " + checkpoint.name);
        Debug.Log(checkpoint.GetComponent<MeshRenderer>().material.name);

        if (checkpoint.GetComponent<MeshRenderer>().material.name == green.name) // make sure checkpoint not active
        {
            return;
        }

        // if not active, set to current checkpoint
        currentCheckpoint = checkpoint;

        // change checkpoint material to show activation
        checkpoint.GetComponent<MeshRenderer>().material = green;
        checkpoint.GetComponent<MeshRenderer>().material.name = green.name;

        // reset countdown
        secondsRemaining = 60.0f;
    }

    // respawn the player at previous checkpoint, or at their spawn
    void Death()
    {
        // disabling and reenabling Character controller allows for manual placement of character
        player.GetComponent<CharacterController>().enabled = false;

        // reset player velocity
        player.GetComponent<PlayerMovement>().ResetVelocity();

        // spawn at previous cehckpoint
        if (currentCheckpoint == null)
        {
            currentCheckpoint = checkpoints[0];
        }

        player.transform.position = currentCheckpoint.transform.position;
        Debug.Log("respawn at checkpoint");

        player.GetComponent<CharacterController>().enabled = true;
    }
}
