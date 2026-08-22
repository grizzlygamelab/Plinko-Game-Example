using UnityEngine;
using UnityEngine.UI;

// Part 2: owns the running score total and keeps the on-screen Text in
// sync with it. Other scripts (see BinScorer) call AddPoints() whenever
// the player earns points - this is the one place in the whole project
// that knows what the current score actually is.
public class ScoreManager : MonoBehaviour
{
    // A static reference to the one ScoreManager in the scene, set below
    // in Awake(). This lets any other script reach it with just
    // "ScoreManager.Instance" instead of every script needing its own
    // Inspector reference wired up by hand. This pattern is called a
    // singleton - it only works cleanly because we only ever have ONE
    // ScoreManager in the scene at a time.
    public static ScoreManager Instance;

    [SerializeField] private Text scoreText;

    private int totalScore;

    // Awake() runs even earlier than Start() - before any other script's
    // Start(). Setting Instance here guarantees it's ready before a Bin
    // trigger could possibly fire and try to use it.
    private void Awake()
    {
        Instance = this;
        UpdateScoreText();
    }

    public void AddPoints(int amount)
    {
        totalScore += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        // Guard against a missing reference so this never throws if the
        // Text object isn't wired up in the Inspector yet.
        if (scoreText != null)
        {
            scoreText.text = $"Score: {totalScore}";
        }
    }
}
