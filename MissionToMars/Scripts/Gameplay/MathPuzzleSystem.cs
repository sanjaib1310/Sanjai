using UnityEngine;
using System.Collections.Generic;

public class MathPuzzleSystem : MonoBehaviour
{
    public class MathPuzzle
    {
        public string question;
        public float correctAnswer;
        public string[] multipleChoices;
        public string difficulty; // Easy, Medium, Hard
        public string mathematicsTopic; // Algebra, Geometry, etc.
        public int xpReward;
    }

    private List<MathPuzzle> easyPuzzles = new List<MathPuzzle>();
    private List<MathPuzzle> mediumPuzzles = new List<MathPuzzle>();
    private List<MathPuzzle> hardPuzzles = new List<MathPuzzle>();

    private void Start()
    {
        GeneratePuzzles();
    }

    private void GeneratePuzzles()
    {
        // Easy Puzzles - Basic Arithmetic
        easyPuzzles.Add(new MathPuzzle
        {
            question = "What is the distance traveled if a rocket moves at 100 km/h for 2 hours? (Distance = Speed × Time)",
            correctAnswer = 200f,
            multipleChoices = new[] { "150", "200", "250", "300" },
            difficulty = "Easy",
            mathematicsTopic = "Distance Formula",
            xpReward = 50
        });

        easyPuzzles.Add(new MathPuzzle
        {
            question = "Calculate the orbital period: Earth orbits sun in 365 days. What fraction of year is this?",
            correctAnswer = 1f,
            multipleChoices = new[] { "0.5", "1.0", "1.5", "2.0" },
            difficulty = "Easy",
            mathematicsTopic = "Fractions",
            xpReward = 50
        });

        // Medium Puzzles - Algebra & Geometry
        mediumPuzzles.Add(new MathPuzzle
        {
            question = "Mars is at distance 225 million km. Express in scientific notation. (Answer in millions km)",
            correctAnswer = 225f,
            multipleChoices = new[] { "2.25 × 10^8", "2.25 × 10^6", "2.25 × 10^7", "2.25 × 10^9" },
            difficulty = "Medium",
            mathematicsTopic = "Scientific Notation",
            xpReward = 100
        });

        mediumPuzzles.Add(new MathPuzzle
        {
            question = "Calculate escape velocity: v = √(2GM/r). If G=6.67×10⁻¹¹, M=5.97×10²⁴, r=6.37×10⁶. Solve for v (m/s)",
            correctAnswer = 11186f,
            multipleChoices = new[] { "5000", "8000", "11186", "15000" },
            difficulty = "Medium",
            mathematicsTopic = "Physics Equations",
            xpReward = 100
        });

        // Hard Puzzles - Advanced Mathematics
        hardPuzzles.Add(new MathPuzzle
        {
            question = "Using Kepler's Third Law: T² ∝ a³. Mars orbital period = 687 days, Earth = 365 days. Find Mars semi-major axis ratio to Earth.",
            correctAnswer = 1.524f,
            multipleChoices = new[] { "1.2", "1.324", "1.524", "1.724" },
            difficulty = "Hard",
            mathematicsTopic = "Orbital Mechanics",
            xpReward = 200
        });

        hardPuzzles.Add(new MathPuzzle
        {
            question = "Gravitational force: F = G(m₁m₂)/r². Calculate force between Earth (5.97×10²⁴ kg) and Moon (7.34×10²² kg) at distance 3.84×10⁸ m.",
            correctAnswer = 1.98e20f,
            multipleChoices = new[] { "1.98e20", "2.54e20", "1.34e20", "3.45e20" },
            difficulty = "Hard",
            mathematicsTopic = "Newtonian Physics",
            xpReward = 200
        });
    }

    public MathPuzzle GetRandomPuzzleByDifficulty(string difficulty)
    {
        List<MathPuzzle> selectedList = difficulty switch
        {
            "Easy" => easyPuzzles,
            "Medium" => mediumPuzzles,
            "Hard" => hardPuzzles,
            _ => easyPuzzles
        };

        return selectedList[Random.Range(0, selectedList.Count)];
    }

    public bool ValidateAnswer(float userAnswer, float correctAnswer, float tolerance = 0.1f)
    {
        return Mathf.Abs(userAnswer - correctAnswer) <= tolerance;
    }
}
