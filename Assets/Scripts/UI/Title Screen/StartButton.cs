using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public AudioSource clickSound;

    public void OnButtonClick() 
    {
        clickSound.Play();
        Invoke("LoadNextScene",0.25f);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}