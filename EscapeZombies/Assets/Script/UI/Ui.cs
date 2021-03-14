using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui : MonoBehaviour
{
    public GameObject UiDead;
    public GameObject UiPlay;
    public GameObject GamePlay;
    // Start is called before the first frame update

    public void StartGameButton()
    {
        GamePlay.SetActive(true);
        UiPlay.SetActive(false);
        UiDead.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
