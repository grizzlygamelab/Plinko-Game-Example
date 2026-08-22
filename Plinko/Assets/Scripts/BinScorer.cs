using UnityEngine;

// Part 2: sits next to the existing Bin component on the same trigger
// collider. Bin.cs still does its original job (log to console, clean
// up the ball) completely unchanged - this script's only job is
// reporting points to the ScoreManager. Keeping this separate means
// Part 1's Bin script never had to be touched or even know scoring
// exists, so the Part 1 scene keeps working exactly as it did before.
//
// Unity allows any number of components on the same GameObject to each
// have their own OnTriggerEnter2D - when the ball enters the trigger,
// BOTH Bin.OnTriggerEnter2D and this script's OnTriggerEnter2D run.
public class BinScorer : MonoBehaviour
{
    [SerializeField] private int pointValue = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<Ball>(out _)) return;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPoints(pointValue);
        }
    }
}
