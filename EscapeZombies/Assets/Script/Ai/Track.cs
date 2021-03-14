using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Track : MonoBehaviour
{
    //follow
    public GameObject[] waypoints;
    public float speed;
    public float trackerSpeed = 10;
    public float rotSpeed = 10;
    private int currentWP = 0;
    public float lookAhead = 5;
    GameObject tracker;
    //follow

    private void Start()
    {
        //ai zom
        tracker = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.position = transform.position;
        tracker.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        ProgessTracker();
        TrackerlookatWP();
    }

    void ProgessTracker()
    {
        float trackerDistance = Vector3.Distance(tracker.transform.position, transform.position);
        if (trackerDistance > lookAhead)
        {
            return;
        }

        float distance = Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position);
        if (distance < 1)
        {
            currentWP++;
        }

        if (currentWP >= waypoints.Length)
        {
            currentWP = 0;
        }

        tracker.transform.LookAt(waypoints[currentWP].transform);
        tracker.transform.Translate(0, 0, trackerSpeed * Time.deltaTime);
    }

    void TrackerlookatWP()
    {
        Quaternion lookatWP = Quaternion.LookRotation(tracker.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(tracker.transform.rotation, lookatWP, rotSpeed * Time.deltaTime);
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
