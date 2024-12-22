using UnityEngine;
using static SwitchBehauviour;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;

    private CharacterController characterController;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    public float interactionRange = 8f; // Dist�ncia m�xima para interagir
    public LayerMask interactableLayerDoors; // Layer dos objetos interativos
    public LayerMask interactableLayerSwitchs; // Layer dos objetos interativos
    public Camera playerCamera;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Trava o cursor no centro da tela
    }
   

    void HandleInteraction()
    {
        // Verifica se o bot�o esquerdo do mouse foi pressionado
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            

            if (Physics.Raycast(ray, out RaycastHit doorHit, interactionRange, interactableLayerDoors))
            {
                Door currentDoor = doorHit.collider.GetComponent<Door>();
                if (currentDoor != null)
                {
                    currentDoor.OnInteract(); // Executa a intera��o
                }
            }

            if (Physics.Raycast(ray, out RaycastHit switchHit, interactionRange, interactableLayerSwitchs))
            {
                StateManager currentSwitch = switchHit.collider.GetComponent<StateManager>();
                if (currentSwitch != null)
                {
                    currentSwitch.SetState(SwitchState.ON); // Executa a intera��o
                }
            }
        }
    }
    void Update()
    {
        MovePlayer();
        RotatePlayerWithMouse();

        HandleInteraction();
    }

    void MovePlayer()
    {
        // Movimento do jogador
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Verifica se o Shift est� pressionado para correr
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Move o personagem
        characterController.Move(move * speed * Time.deltaTime);

        // Aplica gravidade
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reseta a velocidade quando no ch�o
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void RotatePlayerWithMouse()
    {
        // Rota��o do jogador com o mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rota��o horizontal (eixo Y do jogador)
        transform.Rotate(Vector3.up * mouseX);

        // Rota��o vertical (eixo X da c�mera)
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Limita a rota��o vertical
        playerCamera.gameObject.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
