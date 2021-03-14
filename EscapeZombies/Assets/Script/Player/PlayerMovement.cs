using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;

    public GameObject[] enemy;
    public GameObject UiPlay;
    public GameObject GamePlay;
    public GameObject UiDead;

    public bool grounded;

    [SerializeField] private Transform NPC;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform respawnNewpos;
    [SerializeField] private Transform respawnNewNPC;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Spike"))
        {

            Player.transform.position = respawnNewpos.transform.position;
            NPC.transform.position = respawnNewNPC.transform.position;

            UiDead.SetActive(true);
            GamePlay.SetActive(false);
            UiPlay.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void LeaveButton()
    {
        UiPlay.SetActive(true);
        GamePlay.SetActive(false);
        UiDead.SetActive(false);
    }

    public void PlayAgain()
    {
        GamePlay.SetActive(true);
        UiPlay.SetActive(false);
        UiDead.SetActive(false);
    }
}

