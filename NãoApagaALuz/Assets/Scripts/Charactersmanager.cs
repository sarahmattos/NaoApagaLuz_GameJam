using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharactersManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject creaturePrefab;

    private Camera player1Camera;
    private Camera player2Camera;

    void Start()
    {
        // Instancia os jogadores
        var player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Key1", pairWithDevice: Keyboard.current);
        var player2 = PlayerInput.Instantiate(creaturePrefab, controlScheme: "Key2", pairWithDevice: Keyboard.current);

        // Atribui as câmeras dos jogadores
        player1Camera = player1.GetComponentInChildren<Camera>();
        player2Camera = player2.GetComponentInChildren<Camera>();

        // Configura a divisão de tela
        SetupSplitScreen();
    }

    void SetupSplitScreen()
    {
        if (player1Camera != null && player2Camera != null)
        {
             player1Camera.rect = new Rect(0f, 0.5f, 1f, 0.5f); // Jogador 1 na parte inferior
             player2Camera.rect = new Rect(0f, 0f, 1f, 0.5f); // Jogador 2 na parte superior
        }
    }

    void Update()
    {
        // Aqui você pode adicionar mais lógica, se necessário, mas por enquanto a configuração está no Start.
    }
}
