using UnityEngine;

// Attached to the "PegSpawner" GameObject in the scene. Instead of
// hand-placing dozens of pegs in the Editor, this script builds the
// entire peg grid in code when the game starts - change the numbers
// below in the Inspector and the whole board updates without moving a
// single peg by hand.
//
// Notice pegs don't have a Rigidbody2D - only the ball does. In Unity's
// 2D physics, a Collider2D with NO Rigidbody2D attached is treated as
// static: it can be collided with, but it will never move, no matter
// how hard something hits it. That's exactly what a peg should do, and
// it's cheaper for the physics engine than giving every peg a Rigidbody2D
// and telling it not to move.
public class PegSpawner : MonoBehaviour
{
    // All five of these fields show up in the Inspector because they're
    // marked [SerializeField], so you (or your students) can tweak the
    // board's shape without touching this code at all.
    [SerializeField] private GameObject pegPrefab;
    [SerializeField] private int rows = 7;
    [SerializeField] private int pegsPerRow = 8;
    [SerializeField] private float horizontalSpacing = 1.2f;
    [SerializeField] private float verticalSpacing = 1.0f;

    // Start() is a Unity message method: it's called automatically once,
    // right before the first frame this object is active. That only
    // happens when you press Play - it will NOT run just from editing
    // the scene, which is why the peg grid isn't visible until you hit Play.
    private void Start()
    {
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        // The outer loop builds one row at a time, from the top (row 0)
        // down to the bottom (row = rows - 1).
        for (int row = 0; row < rows; row++)
        {
            // The classic Plinko/Galton-board look comes from staggering
            // alternating rows: every other row has one fewer peg and is
            // re-centered, so pegs form a zig-zag rather than a plain
            // grid. That matters physically too - a ball resting exactly
            // on top of a peg in a straight grid could balance forever,
            // but in a zig-zag it always has two neighboring pegs to
            // fall between.
            bool isOffsetRow = row % 2 == 1; // % is the modulo operator: true for rows 1, 3, 5...
            int pegsInThisRow = isOffsetRow ? pegsPerRow - 1 : pegsPerRow;

            // To keep every row centered on x = 0 regardless of how many
            // pegs are in it, we calculate the row's total width and then
            // shift the starting point left by half of that.
            float rowWidth = (pegsInThisRow - 1) * horizontalSpacing;
            float startX = -rowWidth * 0.5f;

            // The inner loop places each peg in the current row, walking
            // left to right.
            for (int col = 0; col < pegsInThisRow; col++)
            {
                float x = startX + col * horizontalSpacing;
                // Rows count downward in world space, so we subtract as
                // "row" increases - row 0 is at the top, higher row
                // numbers are further down (more negative y).
                float y = -row * verticalSpacing;

                // Instantiate creates a new copy of a prefab in the
                // scene. Passing "transform" as the second argument makes
                // the new peg a child of this PegSpawner object, which
                // keeps the Hierarchy window tidy (all pegs nested under
                // one parent instead of scattered at the top level).
                GameObject peg = Instantiate(pegPrefab, transform);

                // localPosition is relative to the parent (PegSpawner),
                // not the whole world - so if you move PegSpawner itself,
                // the entire grid moves with it as one unit.
                peg.transform.localPosition = new Vector3(x, y, 0f);

                // Giving each peg a unique, readable name (e.g. "Peg_2_5")
                // makes the Hierarchy easier to scan while debugging -
                // Unity would otherwise call every copy "Peg" with no way
                // to tell them apart at a glance.
                peg.name = $"Peg_{row}_{col}";
            }
        }
    }
}
