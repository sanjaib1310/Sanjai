using UnityEngine;
using System.Collections.Generic;

public class AstronomyQuizSystem : MonoBehaviour
{
    public class AstronomyQuestion
    {
        public string question;
        public string correctAnswer;
        public string[] options;
        public string category; // Solar System, Planets, etc.
        public string educationalContent;
        public int xpReward;
    }

    private List<AstronomyQuestion> solarSystemQuestions = new List<AstronomyQuestion>();
    private List<AstronomyQuestion> planetQuestions = new List<AstronomyQuestion>();
    private List<AstronomyQuestion> spaceExplorationQuestions = new List<AstronomyQuestion>();

    private void Start()
    {
        InitializeQuestions();
    }

    private void InitializeQuestions()
    {
        // Solar System Questions
        solarSystemQuestions.Add(new AstronomyQuestion
        {
            question = "How many planets are in our Solar System?",
            correctAnswer = "8",
            options = new[] { "7", "8", "9", "10" },
            category = "Solar System",
            educationalContent = "Our Solar System contains 8 planets: Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, and Neptune. Pluto was reclassified as a dwarf planet in 2006.",
            xpReward = 50
        });

        solarSystemQuestions.Add(new AstronomyQuestion
        {
            question = "What is the closest planet to the Sun?",
            correctAnswer = "Mercury",
            options = new[] { "Venus", "Mercury", "Earth", "Mars" },
            category = "Solar System",
            educationalContent = "Mercury is the smallest planet and closest to the Sun. It has extreme temperature variations due to its thin atmosphere.",
            xpReward = 50
        });

        // Planet Questions
        planetQuestions.Add(new AstronomyQuestion
        {
            question = "Which planet is known as the Red Planet?",
            correctAnswer = "Mars",
            options = new[] { "Venus", "Mars", "Jupiter", "Saturn" },
            category = "Planets",
            educationalContent = "Mars appears reddish due to iron oxide (rust) on its surface. It's the fourth planet from the Sun and our destination in this game!",
            xpReward = 75
        });

        planetQuestions.Add(new AstronomyQuestion
        {
            question = "What is Mars' largest volcano?",
            correctAnswer = "Olympus Mons",
            options = new[] { "Arsia Mons", "Pavonis Mons", "Olympus Mons", "Elysium Mons" },
            category = "Planets",
            educationalContent = "Olympus Mons is the largest volcano in the Solar System, nearly 3 times taller than Mount Everest!",
            xpReward = 75
        });

        planetQuestions.Add(new AstronomyQuestion
        {
            question = "What is the deepest canyon on Mars?",
            correctAnswer = "Valles Marineris",
            options = new[] { "Valles Marineris", "Candor Chasma", "Ius Chasma", "Melas Chasma" },
            category = "Planets",
            educationalContent = "Valles Marineris is a massive system of canyons on Mars, longer and deeper than Earth's Grand Canyon!",
            xpReward = 75
        });

        // Space Exploration Questions
        spaceExplorationQuestions.Add(new AstronomyQuestion
        {
            question = "Which space agency first successfully landed on Mars?",
            correctAnswer = "USSR (Soviet Union)",
            options = new[] { "NASA", "ESA", "USSR (Soviet Union)", "ISRO" },
            category = "Space Exploration",
            educationalContent = "The Soviet Union's Mars 3 was the first spacecraft to land on Mars in 1972, though it operated briefly.",
            xpReward = 100
        });

        spaceExplorationQuestions.Add(new AstronomyQuestion
        {
            question = "What does ISS stand for?",
            correctAnswer = "International Space Station",
            options = new[] { "International Space Station", "International Solar Station", "Interstellar Space Ship", "International Satellite System" },
            category = "Space Exploration",
            educationalContent = "The ISS is a collaborative space station orbiting Earth, hosting astronauts from multiple nations conducting scientific research.",
            xpReward = 50
        });

        spaceExplorationQuestions.Add(new AstronomyQuestion
        {
            question = "What is escape velocity for Earth?",
            correctAnswer = "11.2 km/s",
            options = new[] { "5.0 km/s", "8.5 km/s", "11.2 km/s", "15.0 km/s" },
            category = "Space Exploration",
            educationalContent = "Escape velocity is the minimum speed needed to break free from a planet's gravitational pull. For Earth, it's 11.2 km/s (40,300 km/h)!",
            xpReward = 100
        });
    }

    public AstronomyQuestion GetRandomQuestionByCategory(string category)
    {
        List<AstronomyQuestion> selectedList = category switch
        {
            "Solar System" => solarSystemQuestions,
            "Planets" => planetQuestions,
            "Space Exploration" => spaceExplorationQuestions,
            _ => solarSystemQuestions
        };

        return selectedList[Random.Range(0, selectedList.Count)];
    }

    public bool ValidateAnswer(string userAnswer, string correctAnswer)
    {
        return userAnswer.Equals(correctAnswer, System.StringComparison.OrdinalIgnoreCase);
    }
}
