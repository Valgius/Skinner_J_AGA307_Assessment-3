using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text enemyCountText;

    public GameObject inGamePanel;
    public GameObject gameOverPanel;

    private void Start()
    {
        UpdateEnemyCount(0);

        //Turn on our In Game Panel
        inGamePanel.SetActive(true);

        //Turn off our Game Over Panel
        gameOverPanel.SetActive(false);
    }

    public void UpdateEnemyCount(int _count)
    {
        enemyCountText.text = "Cultists: " + _count.ToString();
        if (_count == 0)
            EndGame();
    }

    public void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        //Turn on our Win Panel    
        gameOverPanel.SetActive(true);
        //Turn off our In Game Panel
        inGamePanel.SetActive(false);
    }
}
