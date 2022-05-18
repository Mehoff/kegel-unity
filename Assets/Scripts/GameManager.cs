using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // REF TO CANVASES
    public GameObject winScreen;

    public static GameManager Instance;

    public event Action onGameStateChange;

    private GameStats stats;

    public Text statsText;

    void Awake()
    {
        Instance = this;
    }

    private GameState _state;

    private static List<GameObject> kegelsToDelete;

    void Start()
    {
        winScreen.SetActive(false);
        kegelsToDelete = new List<GameObject>();
        ChangeState(GameState.ChooseThrowDirection);
        Setup();
    }

    void Setup()
    {
        stats = new GameStats();
    }

    public void IncrementThrowsCount()
    {
        this.stats.IncrementThrowsCount();
        this.UpdateStatsUI();
    }

    public GameState GetState()
    {
        return _state;
    }

    public void ChangeState(GameState newState)
    {
        _state = newState;
        onGameStateChange?.Invoke();
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnKegelFall(GameObject kegel)
    {
        Destroy(kegel, 3);
        stats.KegelFall();
        UpdateStatsUI();
        if (this.stats.IsWin())
        {
            ShowWinScreen();
            Invoke("Reset", 5);
        }
    }

    public void UpdateStatsUI()
    {
        statsText.text = stats.ToString();
    }

    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
    }
}


public enum GameState
{
    ChooseThrowDirection,
    ChooseThrowPower,
    BallThrown,
}

public class GameStats
{
    private const int BASE_POINTS = 500;

    private int throwsCount;
    private int kegelsFallenCount;
    private int score;

    public GameStats()
    {
        Reset();
    }

    public void Reset()
    {
        throwsCount = 0;
        kegelsFallenCount = 0;
        score = 0;
    }

    public void IncrementThrowsCount()
    {
        throwsCount++;
    }

    public void KegelFall()
    {
        kegelsFallenCount++;
        score += BASE_POINTS / throwsCount;
    }

    public bool IsWin()
    {
        return this.kegelsFallenCount == 10;
    }

    public override string ToString()
    {
        return $"Score: {score}\nThrows: {throwsCount}\nKegels fallen: {kegelsFallenCount}";
    }
}
