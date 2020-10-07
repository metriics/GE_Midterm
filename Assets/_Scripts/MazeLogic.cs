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
    Vector3 spawnPoint = new Vector3();
    public Camera player;

    // Start is called before the first frame update
    void Start()
    {
        secondsRemaining = 60.0f;
        currentCheckpoint = null;
        spawnPoint = player.transform.position;

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            Death();
        }
    }

    public void checkpointCallback(GameObject checkpoint)
    {
        currentCheckpoint = checkpoint;
        Debug.Log("collision with: " + checkpoint.name);

        // change checkpoint material to show activation
        checkpoint.GetComponent<MeshRenderer>().material = green;

        // reset countdown
        secondsRemaining = 60.0f;
    }

    // respawn the player at previous checkpoint, or at their spawn
    void Death()
    {
        if (currentCheckpoint == null)
        {
            player.transform.position = spawnPoint;
            Debug.Log("respawn at spawn");
        }
        else
        {
            Vector3 respawnPos = currentCheckpoint.transform.position;
            player.transform.position = new Vector3(respawnPos.x, 1.75f, respawnPos.z);
            Debug.Log("respawn at checkpoint");
        }
    }
}
