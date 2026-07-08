using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPCSystem : MonoBehaviour
{
    public enum NPCType { MissionCommander, AIAssistant, AstronomyProfessor, EngineeringMentor, PhysicsScientist }

    [SerializeField] private NPCType npcType;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float dialogueDisplayTime = 5f;

    private Dictionary<NPCType, string[]> npcDialogues = new Dictionary<NPCType, string[]>();
    private Queue<string> currentDialogueQueue = new Queue<string>();
    private bool isDialogueActive = false;

    private void Start()
    {
        InitializeDialogues();
    }

    private void InitializeDialogues()
    {
        npcDialogues[NPCType.MissionCommander] = new[]
        {
            "Welcome to the International Mars Academy, Astronaut!",
            "Your training begins now. Complete all missions to become a Mars Commander.",
            "Remember: Safety first, science always!",
            "Each mission will teach you valuable STEM knowledge.",
            "You're doing great! Keep pushing forward."
        };

        npcDialogues[NPCType.AIAssistant] = new[]
        {
            "Initializing AI Assistant... Ready to help!",
            "Need help? Press H for hints during missions.",
            "My systems are analyzing your progress...",
            "Educational content unlocked: Mars Geology!",
            "Warning: Fuel reserves at 25%. Consider refueling."
        };

        npcDialogues[NPCType.AstronomyProfessor] = new[]
        {
            "Did you know? Mars was named after the Roman god of war.",
            "The Martian atmosphere is 95% carbon dioxide.",
            "Mars has two small moons: Phobos and Deimos.",
            "Olympus Mons is a shield volcano, the largest in the Solar System!",
            "Valles Marineris is 4,000 km long and 7 km deep."
        };

        npcDialogues[NPCType.EngineeringMentor] = new[]
        {
            "All spacecraft systems nominal. Ready for launch!",
            "Pro tip: Manage your fuel consumption wisely.",
            "Damage detected in Sector 3. Initiate repairs!",
            "Satellite alignment complete. Communication restored.",
            "New propulsion system unlocked! Faster travel ahead."
        };

        npcDialogues[NPCType.PhysicsScientist] = new[]
        {
            "Did you know? Mars' gravity is only 38% of Earth's gravity.",
            "The escape velocity of Mars is 5.03 km/s.",
            "A day on Mars is 24 hours and 37 minutes long.",
            "Mars' atmospheric pressure is 0.6% of Earth's.",
            "Orbital mechanics fascinate me! Let's solve this together."
        };
    }

    public void DisplayDialogue()
    {
        if (!npcDialogues.ContainsKey(npcType))
            return;

        string[] dialogues = npcDialogues[npcType];
        string randomDialogue = dialogues[Random.Range(0, dialogues.Length)];
        StartCoroutine(ShowDialogueCoroutine(randomDialogue));
    }

    private System.Collections.IEnumerator ShowDialogueCoroutine(string dialogue)
    {
        isDialogueActive = true;
        dialogueText.text = dialogue;
        yield return new WaitForSeconds(dialogueDisplayTime);
        dialogueText.text = "";
        isDialogueActive = false;
    }

    public string GetNPCName()
    {
        return npcType switch
        {
            NPCType.MissionCommander => "Commander Sierra",
            NPCType.AIAssistant => "ARIA (Artificial Research Intelligence Assistant)",
            NPCType.AstronomyProfessor => "Dr. Chen",
            NPCType.EngineeringMentor => "Engineer Reeves",
            NPCType.PhysicsScientist => "Dr. Morrison",
            _ => "Unknown"
        };
    }

    public bool IsDialogueActive() => isDialogueActive;
}
