using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.PackageManager;
using UnityEngine;

public class PluginTester : MonoBehaviour
{
    const string DLL_NAME = "Lec4Inclass";

    // methods
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

    public void SaveTime(float checkpointTime)
    {
        SaveCheckpointTime(checkpointTime);
    }

    public float LoadTime(int index)
    {
        if (index >= GetNumCheckpoints())
        {
            return -1.0f; // we know this is impossible, it signals out of bounds
        }
        return GetCheckpointTime(index);
    }

    public float LoadTotalTime()
    {
        return GetTotalTime();
    }

    public void LoadResetLogger()
    {
        ResetLogger();
    }

    void Start()
    {
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float currentTime = Time.time;
            float checkpointTime = currentTime - lastTime;
            lastTime = currentTime;

            SaveTime(checkpointTime);
        }

        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0+i))
            {
                Debug.Log(LoadTime(i));
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(LoadTotalTime());
        }
    }

    void OnDestroy()
    {
        ResetLogger();
    }
}
