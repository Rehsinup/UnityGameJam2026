using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject pressKey;
    [SerializeField] private GameObject optionMenu;
    public InputActionReference toggleMenu;
    public InputActionReference Quit1;
    public InputActionReference Quit2;
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnEnable()
    {
        toggleMenu.action.performed += ToggleMenu;
        Quit1.action.performed += OnQuit;
        Quit2.action.performed += OnQuit;
        toggleMenu.action.Enable();
        Quit1.action.Enable();
        Quit2.action.Enable();
    }

    private void OnDisable()
    {
        toggleMenu.action.performed -= ToggleMenu;
        toggleMenu.action.Disable();
        Quit1.action.performed -= OnQuit;
        Quit1.action.Disable();
        Quit2.action.performed -= OnQuit;
        Quit2.action.Disable();
    }

    public void ToggleMenu(InputAction.CallbackContext ctx)
    {
        Debug.Log("Menu Toggle Pressed");
        if (ctx.performed)
        menuPanel.SetActive(true);
        pressKey.SetActive(false);

    }

    public void OnOptionClicked()
    { 
        optionMenu.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

    public void OnQuit(InputAction.CallbackContext ctx)
    {
        Debug.Log("Quit Pressed");
        if (ctx.performed)
            menuPanel.SetActive(true);
            pressKey.SetActive(false);
            optionMenu.SetActive(false);

    }

    public void OnPlayClicked()

    {
        SceneManager.LoadScene("Game");
    }
}


