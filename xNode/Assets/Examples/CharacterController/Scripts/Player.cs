using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 MovementInput;

    GraphOwner _graph;

    void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Horizontal");

        MovementInput = new Vector2(x, y);
    }
}