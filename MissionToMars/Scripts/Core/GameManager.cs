using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private bool isGamePaused = false;
    
    private PlayerProgressManager playerProgress;
    private MissionManager missionManager;
    private AstronomyEducationSystem educationSystem;

    public delegate void GameStateChanged(GameState newState);
    public event GameStateChanged OnGameStateChanged;

    public enum GameState { MainMenu, Playing, Paused, GameOver, LevelComplete, MissionComplete }
    private GameState currentGameState = GameState.MainMenu;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeGame();
    }

    private void InitializeGame()
    {
        playerProgress = gameObject.AddComponent<PlayerProgressManager>();
        missionManager = gameObject.AddComponent<MissionManager>();
        educationSystem = gameObject.AddComponent<AstronomyEducationSystem>();

        Debug.Log("Game Manager Initialized - Mission to Mars");
    }

    public void StartGame(int worldNumber, int missionNumber)
    {
        SetGameState(GameState.Playing);
        missionManager.LoadMission(worldNumber, missionNumber);
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        SetGameState(GameState.Paused);
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        SetGameState(GameState.Playing);
    }

    public void CompleteMission(int xpReward, int coinReward, int starsEarned)
    {
        playerProgress.AddExperience(xpReward);
        playerProgress.AddCoins(coinReward);
        playerProgress.AddStars(starsEarned);
        SetGameState(GameState.MissionComplete);
    }

    public void SetGameState(GameState newState)
    {
        currentGameState = newState;
        OnGameStateChanged?.Invoke(newState);
    }

    public GameState GetCurrentGameState() => currentGameState;
    public bool IsGamePaused() => isGamePaused;
    public PlayerProgressManager GetPlayerProgress() => playerProgress;
    public MissionManager GetMissionManager() => missionManager;
    public AstronomyEducationSystem GetEducationSystem() => educationSystem;
}

public class PlayerProgressManager : MonoBehaviour
{
    private int totalXP = 0;
    private int coins = 0;
    private int stars = 0;
    private int currentLevel = 1;
    private int currentWorld = 1;
    private int completedMissions = 0;
    private List<int> unlockedAchievements = new List<int>();

    public void AddExperience(int amount)
    {
        totalXP += amount;
        CheckLevelUp();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public void AddStars(int amount)
    {
        stars += amount;
    }

    private void CheckLevelUp()
    {
        int requiredXP = currentLevel * 1000;
        if (totalXP >= requiredXP)
        {
            currentLevel++;
            Debug.Log($"Level Up! Now Level {currentLevel}");
        }
    }

    public int GetTotalXP() => totalXP;
    public int GetCoins() => coins;
    public int GetStars() => stars;
    public int GetCurrentLevel() => currentLevel;
}

public class MissionManager : MonoBehaviour
{
    private Mission[] allMissions = new Mission[100];
    private int currentMissionIndex = 0;

    public void LoadMission(int worldNumber, int missionNumber)
    {
        int missionIndex = (worldNumber - 1) * 10 + (missionNumber - 1);
        currentMissionIndex = missionIndex;
        Debug.Log($"Loading Mission: World {worldNumber}, Mission {missionNumber}");
    }

    public Mission GetCurrentMission()
    {
        return allMissions[currentMissionIndex];
    }
}

public class Mission
{
    public int worldNumber;
    public int missionNumber;
    public string missionName;
    public string description;
    public int baseXPReward;
    public int baseCoinReward;
    public float timeLimit;
}

public class AstronomyEducationSystem : MonoBehaviour
{
    public string GetPlanetFact(string planetName)
    {
        return $"Educational fact about {planetName}";
    }

    public bool CheckMathAnswer(float userAnswer, float correctAnswer, float tolerance = 0.01f)
    {
        return Mathf.Abs(userAnswer - correctAnswer) <= tolerance;
    }
}
