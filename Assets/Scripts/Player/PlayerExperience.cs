using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    public int level = 1; // Initial level
    public float currentExperience = 0; // Current experience points
    public float experienceToNextLevel = 99.5f; // Experience required to level up

    public UnityEvent onLevelUp; // Event triggered when the player levels up

    public Slider expSlider; // Reference to the experience slider UI
    public GameObject levelUpCanvas; // Reference to the Level Up Canvas

    private bool isExpBarVisible = false; // Flag to track if the exp bar is visible

    private void Start()
    {
        if (onLevelUp == null)
            onLevelUp = new UnityEvent();

        // Hide the exp bar initially
        ToggleExpBar(false);
    }

    // Method to add experience points
    public void AddExperience(float amount)
    {
        float prevExperience = currentExperience;
        currentExperience += amount;
        Debug.Log("Experience added: " + amount + ". Total experience: " + currentExperience);

        // Update the exp bar smoothly
        UpdateExpBarSmoothly(prevExperience, currentExperience);

        // Check if the player has enough experience to level up
        while (currentExperience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    // Method to handle leveling up
    private void LevelUp()
    {
        level++;
        currentExperience -= experienceToNextLevel;
        experienceToNextLevel *= 1.2f; // Increase the experience required for the next level

        Debug.Log("Level up! New level: " + level);
        onLevelUp.Invoke(); // Trigger the level up event

        // Enable the level up canvas
        if (levelUpCanvas != null)
        {
            levelUpCanvas.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
    }

    // Method to update the exp bar smoothly
    private void UpdateExpBarSmoothly(float startValue, float endValue)
    {
        // Update the exp slider smoothly
        StartCoroutine(UpdateExpSliderSmoothly(startValue / experienceToNextLevel, endValue / experienceToNextLevel));
    }

    // Coroutine to smoothly update the exp slider
    private IEnumerator UpdateExpSliderSmoothly(float startValue, float endValue)
    {
        float elapsedTime = 0;
        float duration = 0.5f; // Change duration as needed

        // Show the exp bar
        ToggleExpBar(true);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float sliderValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);

            if (expSlider != null)
            {
                expSlider.value = sliderValue;
            }

            yield return null;
        }

        // Wait for 2 seconds after exp increase is completed
        yield return new WaitForSeconds(3f);

        // Hide the exp bar
        ToggleExpBar(false);
    }

    // Method to show/hide the exp bar
    private void ToggleExpBar(bool isVisible)
    {
        if (expSlider != null)
        {
            expSlider.gameObject.SetActive(isVisible);
            isExpBarVisible = isVisible;
        }
    }
}
