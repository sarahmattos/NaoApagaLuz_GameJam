using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNetwork : NetworkBehaviour
{
    private readonly NetworkVariable<Vector3> _netPos = new(writePerm: NetworkVariableWritePermission.Owner);
    private readonly NetworkVariable<Quaternion> _netRot = new(writePerm: NetworkVariableWritePermission.Owner);
    Rigidbody rb;
    Vector3 moveInput;
    bool isMine = true;
    float moveSpeed = 5;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            Destroy(GetComponent<Player>());
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(GetComponentInChildren<PlayerInput>());
            // Destroy(this);
            isMine = false;
        }
    }

    public  void Update()
    {
        if (IsOwner)
        {
            _netPos.Value =transform.position;
            _netRot.Value =transform.rotation;

        }
        else
        {
            transform.position = _netPos.Value;
            transform.rotation = _netRot.Value;

        }
        /*
        if (!isMine) return;
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

            Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
            transform.Translate(movement * moveSpeed * Time.deltaTime);
            
           moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
    */
    }
    private void FixedUpdate()
    {
       // if (isMine) HandleMovement();
    }
    public void HandleMovement()
    {
        rb.velocity += moveInput.normalized * (80 *  Time.deltaTime);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, 10f);
    }
}
