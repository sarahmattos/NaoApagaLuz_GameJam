using Unity.Netcode;
using UnityEngine;
using static SwitchBehauviour;

public class StateManager : MonoBehaviour
{
    public SwitchState currentState = SwitchState.OFF; // Estado inicial
    private SwitchBehauviour buttonSwitch;
    private void Start()
    {
        buttonSwitch = gameObject.GetComponent<SwitchBehauviour>();
    }
    public void SetState(SwitchState newState)
    {
        if (newState == currentState) return;
        currentState = newState;
        HandleStateChange();
    }
    public void SwitchCurrentState()
    {
        HandleStateSwitch();
        HandleStateChange();
    }

    // Verifica se está no estado "Acesso"
    public bool IsAcesso()
    {
        return currentState == SwitchState.ON;
    }

    // Executa ações baseadas no estado atual
    private void HandleStateChange()
    {
        switch (currentState)
        {
            case SwitchState.ON:

                buttonSwitch.SetLigthOn(IsAcesso());
                Debug.Log("Objeto está no estado: Acesso");
                break;

            case SwitchState.OFF:

                buttonSwitch.SetLigthOn(IsAcesso());
                Debug.Log("Objeto está no estado: Apagado");
                break;
        }
    }
    private void HandleStateSwitch()
    {
        switch (currentState)
        {
            case SwitchState.ON:
                currentState = SwitchState.OFF;
                break;

            case SwitchState.OFF:
                currentState = SwitchState.ON;
                break;
        }
    }
    private void OnGUI()
    {

        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (GUILayout.Button("Switch")) SwitchCurrentState();
            if (GUILayout.Button("Acender"))SetState(SwitchState.ON);
            if (GUILayout.Button("Apagar")) SetState(SwitchState.OFF);
        }
        GUILayout.EndArea();
    }

}
