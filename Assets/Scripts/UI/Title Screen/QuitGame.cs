using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    public AudioSource clickSound;
    public void OnButtonClick() 
    {
        clickSound.Play();
        Invoke("Quit",0.25f);
    }

    void Quit()
    {
        Application.Quit();
    }
}
