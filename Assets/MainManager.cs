using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int level;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
            level = GameStateManager.level;
    }
}
