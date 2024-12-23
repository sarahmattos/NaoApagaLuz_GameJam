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
    public void SetState(SwitchState newState, bool isPlayer=false)
    {
        Debug.Log("chegou aqui");
        if (newState == currentState) return;
        currentState = newState;
        HandleStateChange(isPlayer);
        Debug.Log(currentState +""+ newState);
    }
    public void SwitchCurrentState()
    {
        HandleStateSwitch();
       // HandleStateChange();
    }

    public bool IsAcesso()
    {
        return currentState == SwitchState.ON;
    }

    private void HandleStateChange(bool isPlayer)
    {
        switch (currentState)
        {
            case SwitchState.ON:

                buttonSwitch.SetLigthOn(true, isPlayer);
                SwitchsManager.Instance.CalculatePercent();
                break;

            case SwitchState.OFF:

                buttonSwitch.SetLigthOn(false, isPlayer);
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
   

}
