using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMover : MonoBehaviour
{
    public Vector3[] scenePositions;
    private static PlayerMover instance;
    private Damagable damagable;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        damagable = GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.OnDeath.AddListener(OnHealthDepleted);
        }

        UpdatePosition(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDestroy()
    {
        if (damagable != null)
        {
            damagable.OnDeath.RemoveListener(OnHealthDepleted);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        if (scene.buildIndex == 0 || scene.buildIndex == sceneCount - 1)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(this.gameObject);
        }
        else
        {
            UpdatePosition(scene.buildIndex);
        }
    }

    void UpdatePosition(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < scenePositions.Length)
        {
            Debug.Log($"Updating position for scene index: {sceneIndex}");
            transform.position = scenePositions[sceneIndex];
        }
        else
        {
            Debug.LogWarning("Scene index is out of range of the defined positions.");
        }
    }

    void OnHealthDepleted()
    {
        StartCoroutine(RespawnAfterDelay(3f));
    }

    IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UpdatePosition(SceneManager.GetActiveScene().buildIndex);
        if (damagable != null)
        {
            damagable.Health = damagable.MaxHealth;
            damagable.IsAlive = true;
        }
    }
}
