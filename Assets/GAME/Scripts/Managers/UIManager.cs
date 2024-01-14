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

    [SerializeField] private GameObject MainCanvas;
    
    private GameController gameController;

    [SerializeField] private TMP_Text enemiesLeftNumber;


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

    public void UpdateShieldBar(float fillAmount)
    {
        farmShieldBar.fillAmount = fillAmount;
        Debug.Log("FarmShield UI " + farmShieldBar.fillAmount);
    }

    public void UpdateEnemiesLeft(int totalEnemiesInGame)
    {
        enemiesLeftNumber.text = Convert.ToString(totalEnemiesInGame);
    }
}
