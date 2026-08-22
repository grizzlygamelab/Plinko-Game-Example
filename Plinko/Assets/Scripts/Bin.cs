using UnityEngine;

// Attach this to a Collider2D that has "Is Trigger" checked (see the
// BinTrigger_0..5 objects under Bins in the scene). A trigger collider
// doesn't physically block anything - the ball passes right through it -
// but Unity still tells us when something entered it, which is exactly
// what we want for "detect the ball landed here" without the ball
// bouncing off the detector itself.
public class Bin : MonoBehaviour
{
    // [SerializeField] exposes this private field in the Inspector so
    // each of the 6 bin objects can have its own name ("Bin 1", "Bin 2",
    // ...) typed in by hand, without making the field public (which would
    // let other scripts change it too - we don't want that here).
    [SerializeField] private string binName = "Bin";

    // Unity calls this automatically whenever a Collider2D overlaps this
    // trigger. The "other" parameter is whatever collider just entered -
    // it could be the ball, but it could also be anything else that has
    // a collider, so we can't assume it's always a ball.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // TryGetComponent looks for a Ball component on the object that
        // entered the trigger. If it doesn't find one, it returns false
        // and we bail out immediately - this is how we ignore anything
        // that isn't a ball. The "out _" means "give me the result, but
        // I don't actually need to use the component itself, only know
        // whether it exists."
        if (!other.TryGetComponent<Ball>(out _)) return;

        // Debug.Log prints to the Console window at the bottom of the
        // Editor. This is the simplest way to see what your game is
        // doing without building any on-screen UI.
        Debug.Log($"Ball landed in {binName}");

        // Destroy removes a GameObject from the scene. Passing 0.5f as
        // the second argument tells Unity to wait half a second first,
        // so the ball is still visible settling into the bin for a
        // moment instead of vanishing the instant it touches the trigger.
        Destroy(other.gameObject, 0.5f);
    }
}
