using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;   // El panel del men� principal
    public GameObject controlsPanel;   // El panel de los controles

    // M�todo para mostrar la pantalla de controles
    public void ShowControls()
    {
        mainMenuPanel.SetActive(false);  // Oculta el men� principal
        controlsPanel.SetActive(true);   // Muestra la pantalla de controles
    }

    // M�todo para volver al men� principal desde los controles
    public void BackToMenu()
    {
        controlsPanel.SetActive(false);  // Oculta la pantalla de controles
        mainMenuPanel.SetActive(true);   // Muestra el men� principal
    }
}
