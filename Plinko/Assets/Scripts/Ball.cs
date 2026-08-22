using UnityEngine;

// A "marker" component: it has no fields and no logic. Its only job is
// to sit on a GameObject and say "this GameObject is a Plinko ball."
//
// Why do this instead of checking the object's name or a string tag?
// Bin.cs can ask "does this collider have a Ball component?" (see
// TryGetComponent in Bin.cs). That check only succeeds for objects that
// actually have this script attached, no matter what they're named or
// tagged - it's a more robust way to identify "one of my game objects"
// in code.
//
// MonoBehaviour is the base class every Unity script component inherits
// from. Attaching a script to a GameObject in the Inspector only works
// if the script's class extends MonoBehaviour, like this one does.
public class Ball : MonoBehaviour
{
}
