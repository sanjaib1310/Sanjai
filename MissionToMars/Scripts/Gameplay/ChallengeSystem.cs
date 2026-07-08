using UnityEngine;
using System.Collections.Generic;

public class ChallengeSystem : MonoBehaviour
{
    public enum ChallengeType { SpaceDebris, RadiationZone, DustStorm, EquipmentFailure, NavigationError, FuelShortage, CommunicationBlackout, TimePressure }

    [System.Serializable]
    public class Challenge
    {
        public ChallengeType type;
        public string name;
        public string description;
        public float difficulty; // 0-1 scale
        public int damageOnFailure;
        public float timeToComplete;
        public bool isActive;
    }

    private List<Challenge> activeChallenges = new List<Challenge>();
    private float timeRemaining;

    public void InitializeChallenge(ChallengeType challengeType)
    {
        Challenge challenge = new Challenge
        {
            type = challengeType,
            isActive = true,
            difficulty = Random.Range(0.3f, 0.9f)
        };

        switch (challengeType)
        {
            case ChallengeType.SpaceDebris:
                challenge.name = "Space Debris Field";
                challenge.description = "Navigate through asteroid field without collisions";
                challenge.damageOnFailure = 15;
                challenge.timeToComplete = 120f;
                break;

            case ChallengeType.RadiationZone:
                challenge.name = "Radiation Exposure";
                challenge.description = "Quickly traverse high-radiation area";
                challenge.damageOnFailure = 25;
                challenge.timeToComplete = 60f;
                break;

            case ChallengeType.DustStorm:
                challenge.name = "Martian Dust Storm";
                challenge.description = "Navigate through poor visibility";
                challenge.damageOnFailure = 10;
                challenge.timeToComplete = 90f;
                break;

            case ChallengeType.EquipmentFailure:
                challenge.name = "System Malfunction";
                challenge.description = "Repair broken equipment before critical failure";
                challenge.damageOnFailure = 20;
                challenge.timeToComplete = 180f;
                break;

            case ChallengeType.NavigationError:
                challenge.name = "Course Correction";
                challenge.description = "Recalculate trajectory to reach destination";
                challenge.damageOnFailure = 10;
                challenge.timeToComplete = 120f;
                break;

            case ChallengeType.FuelShortage:
                challenge.name = "Low Fuel Crisis";
                challenge.description = "Optimize fuel consumption to reach destination";
                challenge.damageOnFailure = 30;
                challenge.timeToComplete = 150f;
                break;

            case ChallengeType.CommunicationBlackout:
                challenge.name = "Communication Loss";
                challenge.description = "Restore communications within time limit";
                challenge.damageOnFailure = 5;
                challenge.timeToComplete = 90f;
                break;

            case ChallengeType.TimePressure:
                challenge.name = "Time-Critical Mission";
                challenge.description = "Complete objective before time runs out";
                challenge.damageOnFailure = 15;
                challenge.timeToComplete = 60f;
                break;
        }

        activeChallenges.Add(challenge);
        timeRemaining = challenge.timeToComplete;
        Debug.Log($"Challenge Started: {challenge.name}");
    }

    private void Update()
    {
        for (int i = activeChallenges.Count - 1; i >= 0; i--)
        {
            if (activeChallenges[i].isActive)
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining <= 0)
                {
                    FailChallenge(activeChallenges[i]);
                }
            }
        }
    }

    public void CompleteChallenge(Challenge challenge)
    {
        challenge.isActive = false;
        activeChallenges.Remove(challenge);
        Debug.Log($"Challenge Completed: {challenge.name}");
    }

    private void FailChallenge(Challenge challenge)
    {
        challenge.isActive = false;
        Debug.Log($"Challenge Failed: {challenge.name} - {challenge.damageOnFailure} damage taken");
        // Apply damage to player/spacecraft
    }

    public List<Challenge> GetActiveChallenges() => activeChallenges;
    public float GetTimeRemaining() => timeRemaining;
}
