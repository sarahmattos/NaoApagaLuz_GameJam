using Unity.Netcode;
using UnityEngine;
using static SwitchBehauviour;

public class StateManager : MonoBehaviour
{
    public SwitchState currentState;
    private SwitchBehauviour buttonSwitch;
    private void Start()
    {
        buttonSwitch = gameObject.GetComponent<SwitchBehauviour>();
        SetState(SwitchState.ON);
    }
    private void OnEnable()
    {
        if (SwitchsManager.Instance != null)
        {
            SwitchsManager.Instance.RegisterObject(this);
        }
    }

    private void OnDisable()
    {
        if (SwitchsManager.Instance != null)
        {
            SwitchsManager.Instance.UnregisterObject(this);
        }
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

    public bool IsAcesso()
    {
        return currentState == SwitchState.ON;
    }

    private void HandleStateChange()
    {
        switch (currentState)
        {
            case SwitchState.ON:

                buttonSwitch.SetLigthOn(IsAcesso());
                Debug.Log("Objeto está no estado: Acesso");
                SwitchsManager.Instance.CalculatePercent();
                break;

            case SwitchState.OFF:

                buttonSwitch.SetLigthOn(IsAcesso());
                Debug.Log("Objeto está no estado: Apagado");
                SwitchsManager.Instance.CalculatePercent();
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
    private void OnMouseDown()
    {
        SwitchCurrentState();
    }

}
