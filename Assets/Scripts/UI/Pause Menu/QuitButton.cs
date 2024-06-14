using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public AudioSource audioSource;

    public void OnButtonClick()
    {
        Time.timeScale=1f;
        audioSource.Play();
        StartCoroutine(LoadMainMenu(0.15f));
    }

    IEnumerator LoadMainMenu(float delay)
    {
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < startTime + delay)
        {
            yield return null;
        }

        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}