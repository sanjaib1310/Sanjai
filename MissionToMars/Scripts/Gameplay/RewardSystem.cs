using UnityEngine;
using System.Collections.Generic;

public class RewardSystem : MonoBehaviour
{
    public class Reward
    {
        public int xpAmount;
        public int coinAmount;
        public int starAmount;
        public List<string> unlockedItems = new List<string>();
    }

    private Dictionary<string, int> xpMultipliers = new Dictionary<string, int>
    {
        { "Easy", 1 },
        { "Medium", 2 },
        { "Hard", 3 }
    };

    private Dictionary<string, bool> achievements = new Dictionary<string, bool>();

    private void Start()
    {
        InitializeAchievements();
    }

    private void InitializeAchievements()
    {
        achievements["First Steps"] = false; // Complete World 1
        achievements["Space Traveler"] = false; // Complete World 5
        achievements["Mars Pioneer"] = false; // Complete World 7
        achievements["Red Planet Master"] = false; // Complete All 100 Missions
        achievements["Math Genius"] = false; // Solve 50 math puzzles
        achievements["Astronomy Expert"] = false; // Answer 50 astronomy questions
        achievements["Speed Runner"] = false; // Complete mission in half the time
        achievements["Perfect Score"] = false; // Complete mission with perfect accuracy
        achievements["Equipment Collector"] = false; // Unlock all equipment
        achievements["Student Commander"] = false; // Complete all 100 missions
    }

    public Reward CalculateReward(string difficulty, bool perfect, bool speedRun)
    {
        Reward reward = new Reward();

        int baseXP = 100;
        int baseCoins = 50;
        int baseStars = 1;

        // Apply difficulty multiplier
        int multiplier = xpMultipliers[difficulty];
        reward.xpAmount = baseXP * multiplier;
        reward.coinAmount = baseCoins * multiplier;
        reward.starAmount = baseStars;

        // Bonus for perfect completion
        if (perfect)
        {
            reward.xpAmount = (int)(reward.xpAmount * 1.5f);
            reward.coinAmount = (int)(reward.coinAmount * 1.5f);
            reward.starAmount = 3;
        }

        // Bonus for speed run
        if (speedRun)
        {
            reward.xpAmount = (int)(reward.xpAmount * 1.25f);
            reward.coinAmount = (int)(reward.coinAmount * 1.25f);
            reward.starAmount += 1;
        }

        return reward;
    }

    public bool TryUnlockAchievement(string achievementName)
    {
        if (achievements.ContainsKey(achievementName) && !achievements[achievementName])
        {
            achievements[achievementName] = true;
            Debug.Log($"Achievement Unlocked: {achievementName}");
            return true;
        }
        return false;
    }

    public bool IsAchievementUnlocked(string achievementName)
    {
        return achievements.ContainsKey(achievementName) && achievements[achievementName];
    }

    public Dictionary<string, bool> GetAllAchievements() => achievements;
}
