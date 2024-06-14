using System.Collections;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public PauseMenu pauseMenu;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonClick()
    {
        audioSource.Play();
        StartCoroutine(ResumeWithDelayCoroutine(0.2f));
    }

    IEnumerator ResumeWithDelayCoroutine(float delay)
    {
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < startTime + delay)
        {
            yield return null;
        }

        pauseMenu.Resume();
    }
}
