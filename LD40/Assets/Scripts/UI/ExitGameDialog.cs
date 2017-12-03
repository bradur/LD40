// Date   : 03.12.2017 20:03
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitGameDialog : MonoBehaviour
{

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;
    private bool isOpen = false;

    [SerializeField]
    private Text pauseMenuTxt;

    [SerializeField]
    private Text gameOverMenuTxt;

    [SerializeField]
    private Animator animator;

    public void Open()
    {
        Time.timeScale = 0f;
        animator.SetTrigger("Open");
        isOpen = true;
    }

    public void Open(string text)
    {
        Time.timeScale = 0f;
        txtComponent.text = text;
        gameOverMenuTxt.enabled = true;
        pauseMenuTxt.enabled = false;
        animator.SetTrigger("Open");
        isOpen = true;
    }


    private void Update()
    {
        if (isOpen)
        {
            if (KeyManager.main.GetKeyUp(Action.Quit))
            {
                Time.timeScale = 1f;
                Application.Quit();
            }
            if (KeyManager.main.GetKeyUp(Action.Restart))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(0);
            }
        }
    }

    public void Close()
    {
        Time.timeScale = 1f;
        animator.SetTrigger("Close");
        isOpen = false;
    }


    public void Toggle()
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

}
