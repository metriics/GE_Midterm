using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class MazeLogic : MonoBehaviour
{
    const string DLL_NAME = "Lec4Inclass";

    // references
    [SerializeField]
    public Text remainingTimeText;
    float secondsRemaining;
    public GameObject checkpointParent;
    List<GameObject> checkpoints = new List<GameObject>();
    GameObject currentCheckpoint;
    public Material green;
    public GameObject player;
    public GameObject platform1;
    public GameObject platform2;
    public GameObject wall;

    [DllImport(DLL_NAME)]
    private static extern void ResetLogger();

    // setters
    [DllImport(DLL_NAME)]
    private static extern void SaveCheckpointTime(float RTBC);

    // getters
    [DllImport(DLL_NAME)]
    private static extern float GetTotalTime();
    [DllImport(DLL_NAME)]
    private static extern float GetCheckpointTime(int index);
    [DllImport(DLL_NAME)]
    private static extern int GetNumCheckpoints();

    float lastTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        secondsRemaining = 30.0f;
        lastTime = Time.time;

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
            EndGame();
        }

        // round seconds from float to int, convert to string
        remainingTimeText.text = "Find a checkpoint in less than " + Mathf.RoundToInt(secondsRemaining).ToString() + " seconds.";

        platform1.GetComponent<PlatformMovement>().UpdateObstacle();
        platform2.GetComponent<PlatformMovement>().UpdateObstacle();
        wall.GetComponent<PlatformMovement>().UpdateObstacle();

        // check if player fell
        if (player.transform.position.y <= -20.0f)
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
        secondsRemaining = 30.0f;
        SaveCheckpointTime(lastTime - Time.time);
        lastTime = Time.time;

        if (checkpoint.name == "Finish")
        {
            EndGame();
        }
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

    private void OnDestroy()
    {
        ResetLogger();
    }

    private void EndGame()
    {
        // save score here, kick to stats menu
        PlayerPrefs.SetFloat("TRT", -GetTotalTime());
        PlayerPrefs.SetInt("NumCheckpoints", GetNumCheckpoints());
        for (int i = 0; i < GetNumCheckpoints(); i++)
        {
            string name = "RTBC" + i.ToString();
            PlayerPrefs.SetFloat(name, -GetCheckpointTime(i));
        }

        if (GetNumCheckpoints() == checkpoints.Count)
        {
            PlayerPrefs.SetString("win", "Success!");
        }
        else
        {
            PlayerPrefs.SetString("win", "Failure!");
        }

        SceneManager.LoadScene("End Scene");
    }
}
