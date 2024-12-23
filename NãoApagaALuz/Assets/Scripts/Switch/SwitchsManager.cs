using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SwitchBehauviour;
using TMPro;
using System.Collections;

public class SwitchsManager : MonoBehaviour
{
    // Lista de objetos registrados
    public List<StateManager> registeredObjects = new List<StateManager>();

    // Singleton para acesso global (opcional)
    public static SwitchsManager Instance;
    private float total;

    #region Ui

    [SerializeField] Slider ligthSlider;
    [SerializeField] TMP_Text ligthScoreText;
    [SerializeField] TMP_Text darkScoreText;


    #endregion

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        registeredObjects = new List<StateManager>(FindObjectsOfType<StateManager>());
        total = registeredObjects.Count;
        //RandomizeStateSwitchs();
        //StartCoroutine(TurnOff());
    }
    public void CalculatePercent()
    {
        if (total == 0) return;

        float percent = 0;
        float countOn = 0;

        for (int i = 0; i < total; i++)
        {
            if (registeredObjects[i].IsAcesso())
            {
                countOn++;
            }
        }
        percent = (float)countOn / total * 100;
        UpdateScore(percent);
    }
    public void UpdateScore(float percent)
    {
        float decimalPercent = percent / 100;

        ligthSlider.value = decimalPercent;
        float ligthScore = Mathf.RoundToInt(percent); 
        float darkScore = 100 - ligthScore;

        ligthScoreText.text= ligthScore.ToString();
        darkScoreText.text= darkScore.ToString();
    }
    public void RandomizeStateSwitchs()
    {
        foreach (StateManager state in registeredObjects)
        {
            state.SetState(SwitchState.ON);
        }
        List<int> randomId = new List<int>();
        float average = registeredObjects.Count / 2;


        for (int i =0; i< average; i++)
        {
            int value = Random.Range(0, registeredObjects.Count);
            while(randomId.Contains(value))
            {
                value = Random.Range(0, registeredObjects.Count);
            }
            randomId.Add(value);
        }
        for(int j = 0; j< randomId.Count; j++)
        {
            IniciateOffEspecificSwitch(randomId[j]);
        }
    }

   public void IniciateOffEspecificSwitch(int id)
    {
        registeredObjects[id].SetState(SwitchState.OFF);
    }

    public void RegisterObject(StateManager obj)
    {
        if (!registeredObjects.Contains(obj))
        {
           // registeredObjects.Add(obj);
        }
    }

    
    public void UnregisterObject(StateManager obj)
    {
        if (registeredObjects.Contains(obj))
        {
           // registeredObjects.Remove(obj);
        }
    }

   /* private void OnGUI()
    {

        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (GUILayout.Button("Inicialize")) RandomizeStateSwitchs();
           // if (GUILayout.Button("Calculate")) CalculatePercent();
        }
        GUILayout.EndArea();
        
    }
   */
   private IEnumerator TurnOff()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            RandomizeStateSwitchs();
        }
    }
}
