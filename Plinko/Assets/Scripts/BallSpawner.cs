using UnityEngine;

// Attached to the "BallSpawner" GameObject, positioned above the peg
// grid. This script is responsible for creating ball instances - it
// never touches an existing ball's physics itself, it just decides
// when a new one should exist.
public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    // How far left/right of this object's position a new ball can spawn.
    // A wider range means more of the peg grid gets used across repeated
    // drops instead of the ball always taking the same path down the middle.
    [SerializeField] private float spawnXRange = 3f;

    // KeyCode is Unity's enum of every keyboard/controller key. Exposing
    // it as a field (instead of hardcoding KeyCode.R below) means you
    // could rebind the reset key from the Inspector without editing code.
    [SerializeField] private KeyCode resetKey = KeyCode.R;

    // A private field that remembers the ball we spawned most recently,
    // so a later call to SpawnBall() can find and remove it. This is
    // NOT [SerializeField] - it's runtime-only bookkeeping, not something
    // you'd ever want to set by hand in the Inspector.
    private GameObject currentBall;

    // Called once, automatically, when Play mode starts - this is what
    // gives you a ball immediately without pressing anything.
    private void Start()
    {
        SpawnBall();
    }

    // Unlike Start(), Update() runs every single frame (commonly 60+
    // times per second) for as long as the object exists. That makes it
    // the right place to check "is a key being pressed right now?".
    private void Update()
    {
        // GetKeyDown is true for exactly one frame - the frame the key
        // was first pressed down. Using GetKey instead would fire
        // SpawnBall() dozens of times per second while R is held, which
        // isn't what we want.
        if (Input.GetKeyDown(resetKey))
        {
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        // If a ball already exists from a previous drop, remove it first.
        // Without this check, pressing R repeatedly would pile up balls
        // on the board forever instead of giving you one fresh drop.
        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        // Random.Range(min, max) picks a random float between the two
        // values (inclusive). Every drop lands the ball at a slightly
        // different horizontal starting point.
        float randomX = Random.Range(-spawnXRange, spawnXRange);

        // We add the random offset to this object's own position, so
        // moving the BallSpawner in the Editor moves the whole spawn
        // area with it.
        Vector3 spawnPos = transform.position + new Vector3(randomX, 0f, 0f);

        // Instantiate both creates the new GameObject AND returns a
        // reference to it, which we store in currentBall so the next
        // SpawnBall() call knows what to clean up.
        currentBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
    }
}
