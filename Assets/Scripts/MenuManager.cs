using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MenuState
{
    SHOWN, HIDDEN
}

public class MenuManager : MonoBehaviour
{
    private MenuState state;
    public GameObject UI;
    public GameObject MenuBackground;
    public GameObject Menu;
    void Start()
    {
        this.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (state)
            {
                case MenuState.SHOWN:
                    Hide();
                    break;
                case MenuState.HIDDEN:
                    Show();
                    break;
            }
        }
    }

    public void Show()
    {
        Debug.Log("Show Menu");
        state = MenuState.SHOWN;
        UI.SetActive(false);
        MenuBackground.SetActive(true);
        Menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Hide()
    {
        Debug.Log("Hide Menu");
        state = MenuState.HIDDEN;
        UI.SetActive(true);
        MenuBackground.SetActive(false);
        Menu.SetActive(false);
        Time.timeScale = 1;
    }
}
