using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject UiPlay;
    public GameObject GamePlay;
    public GameObject UiDead;

    public void OnHit()
    {
        UiDead.SetActive(true);
        GamePlay.SetActive(false);
        UiPlay.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);

        UiDead.SetActive(true);
        GamePlay.SetActive(false);
        UiPlay.SetActive(false);

    }

    public void LeaveButton()
    {
        UiPlay.SetActive(true);
        GamePlay.SetActive(false);
        UiDead.SetActive(false);
    }

}
