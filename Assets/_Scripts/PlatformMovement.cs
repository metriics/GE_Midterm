using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed = 1.0f;

    Vector3 startPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    public void UpdateObstacle()
    {
        if (transform.position.z >= startPos.z + 10)
        {
            speed = -speed;
            Debug.Log("Switching directions");
        }
        else if (transform.position.z <= startPos.z - 10)
        {
            speed = -speed;
            Debug.Log("Switching directions");
        }

        transform.position += new Vector3(0.0f, 0.0f, speed * Time.deltaTime);
    }
}
