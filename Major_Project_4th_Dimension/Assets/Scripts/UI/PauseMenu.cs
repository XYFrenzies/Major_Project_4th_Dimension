using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction pauseMenuAction;


    private float buttonDelay = 0.0f;
    private float maxButtonDelay = 1.0f;
    [SerializeField] private GameObject m_pauseMenu = null;
    [SerializeField] private GameObject m_gameUI = null;
    [SerializeField] private GameObject m_optionsUI = null;
    private bool pause = false;
    private bool isPaused = false;
    private void Awake()
    {
        pauseMenuAction = playerInput.actions["PauseMenu"];
        m_pauseMenu.SetActive(false);
    }

    private void OnEnable()
    {
        pauseMenuAction.performed += _ => Pause();
    }

    private void OnDisable()
    {
        pauseMenuAction.performed -= _ => Pause();
    }


    public void Pause()
    {
        if (isPaused && maxButtonDelay < buttonDelay)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    
    private void Update()
    {
        buttonDelay += Time.unscaledDeltaTime;
    }
    
    private void PauseGame() 
    {
        Time.timeScale = 0;
        m_pauseMenu.SetActive(true);
        m_gameUI.SetActive(false);
        isPaused = true;
        buttonDelay = 0;
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        m_pauseMenu.SetActive(false);
        m_gameUI.SetActive(true);
        buttonDelay = 0;
    }
    public void OptionsMenuBack()
    {
        m_optionsUI.SetActive(false);
        m_pauseMenu.SetActive(true);
        //Need to fill this in when the options menu is ready to be used.
    }
    public void OptionsMenu()
    {
        m_optionsUI.SetActive(true);
        m_pauseMenu.SetActive(false);
        //Need to fill this in when the options menu is ready to be used.
    }
    public void ReturnToMenu(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}

// Update is called once per frame
//public void Pause(InputAction.CallbackContext context)
//{
//    if (context.phase != InputActionPhase.Performed)
//    {
//        pause = false;
//        return;
//    }
//    else
//    {
//        pause = true;
//    }
//}

//private void Update()
//{
//    buttonDelay += Time.unscaledDeltaTime;
//    if (pause && maxButtonDelay < buttonDelay)
//    {
//        switch (isPaused)
//        {
//            case true:
//                ResumeGame();
//                break;
//            case false:
//                PauseGame();
//                break;
//        }
//    }
//}