using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : GameBehaviour
{
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    bool isPaused = false;

    private void Start()
    {
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ToggleInventory()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            inventoryPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            inventoryPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
