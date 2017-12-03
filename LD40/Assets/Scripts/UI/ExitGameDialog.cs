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
    private Animator animator;
    public void Open()
    {
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
        animator.SetTrigger("Close");
        isOpen = false;
    }


    public void Toggle()
    {
        if (isOpen)
        {
            Time.timeScale = 1f;
            Close();
        }
        else
        {
            Time.timeScale = 0f;
            Open();
        }
    }

}
