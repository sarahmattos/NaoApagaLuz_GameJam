using UnityEngine;
using UnityEngine.InputSystem;

public class Creature : Character
{
    public override void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    public override void Update()
    {
        /*
        if (Input.GetKey(KeyCode.UpArrow))
            moveInput.y = 1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            moveInput.y = -1f;
        else
            moveInput.y = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
            moveInput.x = -1f;
        else if (Input.GetKey(KeyCode.RightArrow))
            moveInput.x = 1f;
        else
            moveInput.x = 0f;
        */
        base.Update();
    }
    public override void OnFire()
    {
        Debug.Log("Player 2 disparou!");
    }
}
