using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Character : MonoBehaviour
{
    protected Vector2 moveInput;
    public float moveSpeed = 5f;

    public abstract void OnMove(InputValue value);

    public virtual void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    public virtual void OnFire()
    {
        Debug.Log($"{gameObject.name} disparou!");
    }
}
