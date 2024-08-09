using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI crystalTxt;
    public TextMeshProUGUI crystalEndTxt;
    public TextMeshProUGUI crystalHighscoreTxt;
    
    public GameObject startPanel;
    public GameObject restartPanel;

    private PlayerController _playerController;
    
    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }
    public void StartPanel()
    {
        PlayerPrefs.DeleteKey("Crystal");
        startPanel.SetActive(true);
        crystalHighscoreTxt.text = _playerController.highScore.ToString();
    }
    public void StartButton()
    {
        startPanel.SetActive(false);
        _playerController.isGameStart = true;
        Time.timeScale = 1;
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }
}