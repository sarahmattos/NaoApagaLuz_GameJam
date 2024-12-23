using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("TesteArte");
    }
    public void ShowCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }
}
