using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Image farmHealthBar;

    [SerializeField]
    private Image farmShieldBar;

    public static UIManager Instance;

    [SerializeField] private GameObject GameOverCanvas;

    [SerializeField] private GameObject WinGameCanvas;

    [SerializeField] private GameObject MainCanvas;
    
    private GameController gameController;

    [SerializeField] private GameObject enemiesLeftGameObject;

    [SerializeField] private TMP_Text enemiesLeftNumber;

    [SerializeField] private TMP_Text roundNumber;

    private int totalEnemies;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        gameController = GameController.Instance;

        //ocultamos el Enemies Text Left en la UI en la primera ronda
        HideEnemiesLeft();
    }

    public void UpdateHealthBar(float fillAmount)
    {
        farmHealthBar.fillAmount = fillAmount;
        //si ha muerto la granja
        if(fillAmount<=0)
        {
            ShowGameOverCanvas();
        }
    }

    public void ShowGameOverCanvas()
    {
        GameOverCanvas.SetActive(true);
        //quitar canvas player
        MainCanvas.SetActive(true);
    }

    public void ShowYouWinCanvas()
    {
        WinGameCanvas.SetActive(true);
    }

    public void ShowEnemiesLeft()
    {
        enemiesLeftGameObject.SetActive(true);
    }

    public void HideEnemiesLeft()
    {
        enemiesLeftGameObject.SetActive(false);
    }

    public void UpdateShieldBar(float fillAmount)
    {
        farmShieldBar.fillAmount = fillAmount;

    }

    public void UpdateTotalEnemies(int totalEnemiesInGame)
    {
        totalEnemies = totalEnemiesInGame;
        enemiesLeftNumber.text = Convert.ToString(totalEnemies);
    }

    public void RemoveOneEnemy()
    {
        totalEnemies -= 1;
        enemiesLeftNumber.text = Convert.ToString(totalEnemies);
    }

   

    public void UpdateRoundNumber(int actualRound)
    {
        roundNumber.text = Convert.ToString(actualRound);
    }
}
