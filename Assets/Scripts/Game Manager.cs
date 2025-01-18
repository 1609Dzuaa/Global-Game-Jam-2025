using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState GameState { get; set; }
    public float ElapsedTime => _elapsedTime;

    private float _elapsedTime = 0f;
    private GameState _previousState;

    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (HandleGameOver())
            return;

        if (HandlePaused())
            return;

        if (HandlePlaying())
            return;

        if (HandlePreparing())
            return;
    }

    private bool HandlePlaying()
    {
        if (GameState is not GameState.Playing)
            return false;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _previousState = GameState;
            GameState = GameState.Paused;
            Time.timeScale = 0;
            return true;
        }

        _elapsedTime += Time.deltaTime;
        return true;
    }

    private bool HandlePreparing()
    {
        if (GameState is not GameState.Preparing)
            return false;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _previousState = GameState;
            GameState = GameState.Paused;
            Time.timeScale = 0;
            return true;
        }

        return true;
    }

    private bool HandlePaused()
    {
        if (GameState is not GameState.Paused)
            return false;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            GameState = _previousState;
            return true;
        }

        return true;
    }

    private bool HandleGameOver()
    {
        if (GameState is not GameState.GameOver)
            return false;

        return true;
    }
}

public enum GameState
{
    Preparing,
    Playing,
    GameOver,
    Paused,
}
