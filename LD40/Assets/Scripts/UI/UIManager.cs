// Date   : 29.07.2017 16:18
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{

    public static UIManager main;

    void Awake()
    {
        main = this;
    }

    [SerializeField]
    private GameObject infoDialog;
    public void ToggleInfoDialog()
    {
        infoDialog.gameObject.SetActive(!infoDialog.gameObject.activeSelf);
    }

    public void Victory()
    {
        gameOver = true;
        if (!exitGameDialog.isActiveAndEnabled)
        {
            exitGameDialog.gameObject.SetActive(true);
        }
        exitGameDialog.Open("The END!", true);
    }

    [SerializeField]
    private ExitGameDialog exitGameDialog;
    void Update()
    {
        if (KeyManager.main.GetKeyUp(Action.Pause) && !gameOver) {
            if(!exitGameDialog.isActiveAndEnabled)
            {
                exitGameDialog.gameObject.SetActive(true);
            }
            exitGameDialog.Toggle();
        }
        if (KeyManager.main.GetKeyUp(Action.TogglePointers))
        {
            pointerContainer.gameObject.SetActive(!pointerContainer.gameObject.activeSelf);
            radarText.gameObject.SetActive(!radarText.gameObject.activeSelf);
        }
        if (KeyManager.main.GetKeyUp(Action.ToggleMusic))
        {
            ToggleMusic();
        }
        if (KeyManager.main.GetKeyUp(Action.ToggleSounds))
        {
            ToggleSfx();
        }
    }

    [SerializeField]
    private GameObject radarText;

    private bool gameOver = false;
    public void OpenGameOverDialog()
    {
        gameOver = true;
        if (!exitGameDialog.isActiveAndEnabled)
        {
            exitGameDialog.gameObject.SetActive(true);
        }
        exitGameDialog.Open("Game over!");
    }

    public void OpenOutofFuelDialog()
    {
        if (!exitGameDialog.isActiveAndEnabled)
        {
            exitGameDialog.gameObject.SetActive(true);
        }
        exitGameDialog.Open("OUT OF FUEL!", false);
    }

    /*[SerializeField]
    private MessageDisplay messageDisplay;*/

    [SerializeField]
    private Sprite defaultMessageSprite;

    [SerializeField]
    private RectTransform defaultNotificationPosition;

    /*public void ShowNotification(string message)
    {
        messageDisplay.SpawnMessage(defaultNotificationPosition.anchoredPosition, defaultMessageSprite, message);
    }

    public void ShowNotification(string message, bool stay) {
        if (stay)
        {
            messageDisplay.SpawnMessage(defaultNotificationPosition.anchoredPosition, defaultMessageSprite, message, stay);
        } else
        {
            ShowNotification(message);
        }
    }*/

    [SerializeField]
    private HUDHealth hudHealth;
    public void PlayerTakeDamage(int damage)
    {
        hudHealth.TakeDamage(damage);
    }

    public void SetInitialHealth(int health)
    {
        hudHealth.SetInitialHealth(health);
    }

    public void RepairHealth(int health)
    {
        hudHealth.Repair(health);
    }


    MessageDisplay messageDisplay;
    public void ShowDefaultMessage(Vector2 position, string message)
    {
        messageDisplay.Init(position, defaultMessageSprite, message);
    }

    [SerializeField]
    private HomePointer genericPointerPrefab;

    [SerializeField]
    private Transform pointerContainer;
    public void CreatePointer(Transform target, Color color, SpriteRenderer renderer, Sprite icon, float ypos)
    {
        HomePointer homePointer = Instantiate(genericPointerPrefab);
        if (!homePointer.isActiveAndEnabled)
        {
            homePointer.gameObject.SetActive(true);
        }
        RectTransform rt = homePointer.GetComponent<RectTransform>();
        rt.SetParent(pointerContainer);
        rt.localPosition = Vector2.zero;
        /*homePointer.transform.SetParent(pointerContainer);
        homePointer.transform.position = Vector2.zero;*/
        homePointer.Init(target, color, renderer, icon, ypos);
    }

    [SerializeField]
    private HUDToggle hudMusic;

    [SerializeField]
    private HUDToggle hudSfx;

    public void ToggleMusic()
    {
        //hudMusic.Toggle();
        SoundManager.main.ToggleMusic();
    }

    public void ToggleSfx()
    {
        //hudSfx.Toggle();
        SoundManager.main.ToggleSfx();
    }

    [SerializeField]
    private HUDOre hudOre;

    public void SetOre(int value)
    {
        hudOre.SetValue(value);
    }

    [SerializeField]
    private FlyToTarget oreEffectPrefab;

    [SerializeField]
    private RectTransform oreEffectTarget;

    [SerializeField]
    private RectTransform oreEffectContainer;

    public void AddOre(int value)
    {
        hudOre.AddToValue(value);
    }

    public void AddOreEffect(int value, Vector3 position)
    {
        FlyToTarget oreEffect = Instantiate(oreEffectPrefab);
        oreEffect.GetComponent<RectTransform>().SetParent(oreEffectContainer);
        oreEffect.Init(oreEffectTarget, Camera.main.WorldToScreenPoint(position), value);
    }

    public void WithdrawOre(int value)
    {
        hudOre.Withdraw(value);
    }

    [SerializeField]
    private HUDMoney hudMoney;

    public void SetMoney(int value)
    {
        hudMoney.SetValue(value);
    }

    public void Topup(int value)
    {
        hudMoney.AddToValue(value);
    }

    public void Withdraw(int value)
    {
        hudMoney.Withdraw(value);
    }


    [SerializeField]
    private HUDPower hudPower;

    public void AddPower(float amount)
    {
        hudPower.AddToValue(amount);
    }

    public void DrainPower(float amount)
    {
        hudPower.Withdraw(amount);
    }

    public void SetPower(float amount, float maxAmount)
    {
        hudPower.SetValue(amount, maxAmount);
    }


    [SerializeField]
    private GameObject gameOverScreen;

    public void ShowGameOverScreen ()
    {
        //ClearStaticMessage();
        gameOverScreen.SetActive(true);
    }

    [SerializeField]
    private GameObject theEndScreen;

    public void ShowTheEndScreen()
    {
        //ClearStaticMessage();
        theEndScreen.SetActive(true);
    }
}
