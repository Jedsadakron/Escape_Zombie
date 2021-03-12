using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;
    [HideInInspector]
    public GameObject axis1;
    [HideInInspector]
    public GameObject axis2;

    public Transform Player;


    //public float smoothSpeed = .125f;
    public float distance;
    private float limitAngleMin =0;
    private float limitAngleMax =40;
    public float sensitivity;

    private Vector3 currentEular;
    public Vector3 offset;
    private Vector2 inputAxis;
    private void Start()
    {
        axis1 = new GameObject("cam_axis1");
        axis2 = new GameObject("cam_axis2");

        axis2.transform.parent = axis1.transform;
        this.transform.parent = axis2.transform;
    }
    private void LateUpdate()
    {
        //transform.position = Player.position+offset;
        //axis1.transform.position = target.transform.position;
        //axis2.transform.localPosition = -Vector3.forward * distance;
        target.transform.eulerAngles = new Vector3(0, axis1.transform.eulerAngles.y, 0);

        axis1.transform.position = target.transform.position + offset;
        axis2.transform.localPosition = -Vector3.forward * distance;

        currentEular += Vector3.right * (-sensitivity * Input.GetAxis("Mouse Y")) * Time.deltaTime;
        currentEular += Vector3.up * (sensitivity * Input.GetAxis("Mouse X")) * Time.deltaTime;
        currentEular.x = Mathf.Clamp(currentEular.x, limitAngleMin, limitAngleMax);

        axis1.transform.localRotation = Quaternion.Euler(currentEular);
    }


}
