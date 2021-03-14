using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public GameObject[] waypoints;
    public float speed;
    public float trackerSpeed = 10;
    public float rotSpeed = 10;
    private int currentWP = 0;
    public float lookAhead = 5;

    GameObject tracker;

    
    void TrackerTest()
    {
        float trackerDistance = Vector3.Distance(transform.position, transform.position);
        if (trackerDistance > lookAhead)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, waypoints[currentWP].transform.position);
        if (distance < 1)
        {
            currentWP++;
        }

        if (currentWP >= waypoints.Length)
        {
            currentWP = 0;
        }

        transform.LookAt(waypoints[currentWP].transform);
        transform.Translate(0, 0, trackerSpeed * Time.deltaTime);
    }

    void TrackerlookatWPTest()
    {
        Quaternion lookatWP = Quaternion.LookRotation(transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookatWP, rotSpeed * Time.deltaTime);
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void Update()
    {
        TrackerTest();
        TrackerlookatWPTest();
    }
}
