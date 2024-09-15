using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;   // El panel del menú principal
    public GameObject controlsPanel;   // El panel de los controles

    // Método para mostrar la pantalla de controles
    public void ShowControls()
    {
        mainMenuPanel.SetActive(false);  // Oculta el menú principal
        controlsPanel.SetActive(true);   // Muestra la pantalla de controles
    }

    // Método para volver al menú principal desde los controles
    public void BackToMenu()
    {
        controlsPanel.SetActive(false);  // Oculta la pantalla de controles
        mainMenuPanel.SetActive(true);   // Muestra el menú principal
    }
}
