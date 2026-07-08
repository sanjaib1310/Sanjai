using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private TextMeshProUGUI oxygenText;
    [SerializeField] private TextMeshProUGUI fuelText;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI starsText;
    [SerializeField] private TextMeshProUGUI missionObjectiveText;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider energySlider;
    [SerializeField] private Slider oxygenSlider;
    [SerializeField] private Slider fuelSlider;

    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject missionCompletePanel;
    [SerializeField] private GameObject achievementNotificationPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += HandleGameStateChange;
    }

    private void HandleGameStateChange(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Paused:
                pauseMenuPanel.SetActive(true);
                break;
            case GameManager.GameState.Playing:
                pauseMenuPanel.SetActive(false);
                break;
            case GameManager.GameState.MissionComplete:
                missionCompletePanel.SetActive(true);
                break;
        }
    }

    public void UpdatePlayerStats(float health, float energy, float oxygen, float fuel)
    {
        healthSlider.value = health / 100f;
        healthText.text = $"Health: {health:F0}%";

        energySlider.value = energy / 100f;
        energyText.text = $"Energy: {energy:F0}%";

        oxygenSlider.value = oxygen / 100f;
        oxygenText.text = $"Oxygen: {oxygen:F0}%";

        fuelSlider.value = fuel / 100f;
        fuelText.text = $"Fuel: {fuel:F0}%";
    }

    public void UpdateCurrency(int xp, int coins, int stars)
    {
        xpText.text = $"XP: {xp}";
        coinText.text = $"Coins: {coins}";
        starsText.text = $"Stars: {stars}";
    }

    public void SetMissionObjective(string objective)
    {
        missionObjectiveText.text = objective;
    }

    public void ShowAchievementNotification(string achievementName, string description)
    {
        GameObject notification = Instantiate(achievementNotificationPrefab, transform);
        TextMeshProUGUI notificationText = notification.GetComponent<TextMeshProUGUI>();
        notificationText.text = $"Achievement Unlocked!\n{achievementName}\n{description}";
        Destroy(notification, 5f);
    }

    public void ShowPauseMenu()
    {
        pauseMenuPanel.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenuPanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
    }

    public void HideMainMenu()
    {
        mainMenuPanel.SetActive(false);
    }
}
