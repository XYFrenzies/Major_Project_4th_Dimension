using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private List<GameObject> m_menus;
    private InputAction m_optionsMenuAction;
    // Start is called before the first frame update
    void Start()
    {
        m_optionsMenuAction = playerInput.actions["OptionsMovement"];
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputStatus();
    }

    private void CheckInputStatus() 
    {

    }
    public void SetMenu(GameObject menuSelected) 
    {
        foreach (var item in m_menus)
        {
            if (item == menuSelected)
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }
}
