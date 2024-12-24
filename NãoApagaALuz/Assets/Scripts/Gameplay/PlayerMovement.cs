using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SwitchBehauviour;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;

    private CharacterController characterController;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    [Header("Interact")]
    public float interactionRange = 8f; // Distância máxima para interagir
    public float interactionGunRange = 12f; // Distância máxima para interagir
    public LayerMask interactableLayerDoors; // Layer dos objetos interativos
    public LayerMask interactableLayerSwitchs; // Layer dos objetos interativos
    public LayerMask interactableLayerIA; // Layer dos objetos interativos
    public Camera playerCamera;

    [Header("Stun")]
    private bool canStunn = false;
    private bool canGetGun = false;
    public Slider sliderStunn; 
    public Image imageEstun; 
    public TMP_Text textEstun; 
    public float totalTime = 15f; 
    public float stunTime = 6f; 
    private float timeElapsed = 0f;
    [SerializeField] GameObject gun;

    [Header("TP")]
    int idSort, oldSort;

    [Header("Stamina")]
    public Slider staminaBar; // Referência à barra de estamina na UI
    public float maxStamina = 100f; // Máxima estamina
    public float stamina = 100f; // Estamina atual
    public float staminaConsumptionRate = 30f; // Consumo de estamina por segundo ao correr
    public float staminaRegenRate = 10f; // Regeneração de estamina por segundo
    public float minStaminaToRun = 10f; // Estamina mínima para correr
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Trava o cursor no centro da tela
        sliderStunn.value = 0;
        if (staminaBar != null)
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = stamina;
        }
    }
   
    public void TeleportPlayer()
    {
        idSort = Random.Range(0, RoomsControl.Instance.rooms.Length);
        while (idSort == oldSort)
        {
            idSort = Random.Range(0, RoomsControl.Instance.rooms.Length);
        }
        oldSort = idSort;
        RoomController currentRoom = RoomsControl.Instance.ReturnTargetRoom(idSort);
        characterController.enabled = false;
        transform.position = RoomsControl.Instance.ReturnTargetRoom(idSort).target.position;
        characterController.enabled = true;
    }

    void HandleInteraction()
    {
        // Verifica se o botão esquerdo do mouse foi pressionado
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            

            if (Physics.Raycast(ray, out RaycastHit doorHit, interactionRange, interactableLayerDoors))
            {
                Door currentDoor = doorHit.collider.GetComponent<Door>();
                if (currentDoor != null)
                {
                    currentDoor.OnInteract(); // Executa a interação
                    Soundmanager.Instance.DoorSound();
                }
            }

            if (Physics.Raycast(ray, out RaycastHit switchHit, interactionRange, interactableLayerSwitchs))
            {
                StateManager currentSwitch = switchHit.collider.GetComponent<StateManager>();
                if (currentSwitch != null)
                {
                    currentSwitch.SetState(SwitchState.ON,true); // Executa a interação
                    //currentSwitch.SwitchCurrentState();
                }
            }

            if (canStunn)
            {
                if (Physics.Raycast(ray, out RaycastHit IaHit, interactionGunRange, interactableLayerIA))
                {
                    IaController ia = IaHit.collider.GetComponent<IaController>();
                    if (ia != null)
                    {
                        ia.CallGetStunned(stunTime);
                        ResetSlider();
                        
                    }
                }
                else
                {
                    ResetSlider();
                }
                Soundmanager.Instance.HitSound();
            }
            
        }
    }
    void Update()
    {
        MovePlayer();
        RotatePlayerWithMouse();

        HandleInteraction();
        if (!GameManager.instance.startedGame) return;
        //gun system
        if (sliderStunn.value < 1f)
        {
            timeElapsed += Time.deltaTime;  
            sliderStunn.value = timeElapsed / totalTime;  
        }

        if (sliderStunn.value >= 1f && !canGetGun)
        {
            canGetGun = true;
            imageEstun.gameObject.SetActive(true);
            textEstun.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canGetGun && !canStunn)
            {
                gun.SetActive(true);
                canStunn = true;
                imageEstun.gameObject.SetActive(false);
                textEstun.gameObject.SetActive(false);
            }
        }
 
    }

    // Função para reiniciar o slider
    void ResetSlider()
    {
        timeElapsed = 0f;  
        sliderStunn.value = 0; 
        canStunn = false;
        canGetGun = false;
        gun.SetActive(false);
        imageEstun.gameObject.SetActive(false);
        textEstun.gameObject.SetActive(true);
    }

    void MovePlayer()
    {
        // Movimento do jogador

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        if (!GameManager.instance.startedGame)
        {
            float speed = 0;
            characterController.Move(move * speed * Time.deltaTime);
        }
        else
        {
           

            bool isRunning = Input.GetKey(KeyCode.LeftShift) && stamina > minStaminaToRun;

            // Define a velocidade de movimento com base na corrida e estamina
            float speed = isRunning ? runSpeed : walkSpeed;

            if (isRunning)
            {
                ConsumeStamina();
            }
            else
            {
                RegenerateStamina();
            }

            // Move o personagem
            characterController.Move(move * speed * Time.deltaTime);

            // Aplica gravidade
            if (characterController.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Reseta a velocidade quando no chão
            }

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);

            // Atualiza a barra de estamina
            UpdateStaminaBar();
        }
           
    }

    void ConsumeStamina()
    {
        stamina -= staminaConsumptionRate * Time.deltaTime;
        if (stamina < 0) stamina = 0; // Garante que a estamina não fique negativa
    }

    void RegenerateStamina()
    {
        stamina += staminaRegenRate * Time.deltaTime;
        if (stamina > maxStamina) stamina = maxStamina; // Garante que a estamina não ultrapasse o máximo
    }

    void UpdateStaminaBar()
    {
        if (staminaBar != null)
        {
            staminaBar.value = stamina;
        }
    }

    void RotatePlayerWithMouse()
    {
        // Rotação do jogador com o mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotação horizontal (eixo Y do jogador)
        transform.Rotate(Vector3.up * mouseX);

        // Rotação vertical (eixo X da câmera)
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Limita a rotação vertical
        playerCamera.gameObject.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
  
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TP"))
        {
            if (other.gameObject.GetComponent<Teleport>().canPass)
            {
                TeleportPlayer();
                other.gameObject.GetComponent<Teleport>().ResetTp();
            }
            
        }
        if (other.gameObject.CompareTag("ScaryTrigger"))
        {
            Debug.Log("triggou");
            other.gameObject.GetComponent<ScaryTrigger>().ActiveScary();


        }
    }
}
