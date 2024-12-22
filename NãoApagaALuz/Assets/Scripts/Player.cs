
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    public override void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    public override void Update()
    {
        /*
        if (Input.GetKey(KeyCode.W))
            moveInput.y = 1f;
        else if (Input.GetKey(KeyCode.S))
            moveInput.y = -1f;
        else
            moveInput.y = 0f;

        if (Input.GetKey(KeyCode.A))
            moveInput.x = -1f;
        else if (Input.GetKey(KeyCode.D))
            moveInput.x = 1f;
        else
            moveInput.x = 0f;
        */
        base.Update();
    }
    public override void OnFire()
    {
        Debug.Log("Player 1 disparou!");
    }
}
