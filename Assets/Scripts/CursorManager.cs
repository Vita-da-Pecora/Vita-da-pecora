using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    [Header("Scene settings")]
    public string[] gameplayScenes;

    [Header("Escape key unlock")]
    public bool allowEscapeUnlock = true;

    private bool cursorLocked = true;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        UpdateCursorState(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (allowEscapeUnlock && Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateCursorState(scene.name);
    }

    void UpdateCursorState(string sceneName)
    {
        if (IsGameplayScene(sceneName))
        {
            LockCursor();
        }
        else
        {
            UnlockCursor();
        }
    }

    bool IsGameplayScene(string name)
    {
        foreach (string scene in gameplayScenes)
        {
            if (scene == name)
                return true;
        }
        return false;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorLocked = true;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorLocked = false;
    }

    public void ResumeGameplay()
    {
        LockCursor();
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
