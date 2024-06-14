using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject optionsCanvas;
    public Slider musicSlider;
    public Slider fxSlider;
    private SoundManager soundManager;

    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        UpdateSliders();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        optionsCanvas.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        optionsCanvas.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void UpdateSliders()
    {
        if (soundManager != null)
        {
            musicSlider.value = soundManager.GetMusicVolume();
            fxSlider.value = soundManager.GetFXVolume();
        }
    }
}
