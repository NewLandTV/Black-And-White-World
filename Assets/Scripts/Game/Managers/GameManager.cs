using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private KeyCode changeColorKey = KeyCode.K;
    private bool darkMode;

    [SerializeField]
    private Player blackPlayer;
    [SerializeField]
    private Player whitePlayer;
    [SerializeField]
    private Image goMapButtonImage;
    private Camera mainCamera;

    private StageConfig stageConfig;

    private UIManager uiManager;

    // Static functions
    public static Action clearStage;
    public static Action dieAction;
    public static Func<bool> getClear;

    private bool clear;
    private bool die;
    private bool visiblePlayer;

    private void Awake()
    {
        SetupStaticFunctions();
        SetupComponent();
        SetupStage();
    }

    private IEnumerator Start()
    {
        while (!stageConfig.EndAnimation)
        {
            yield return null;
        }

        visiblePlayer = true;

        SpawnPlayer();
    }

    private void Update()
    {
        if (clear || die)
        {
            return;
        }

        InputChangeColor();
    }

    private void SetupStaticFunctions()
    {
        clearStage = ClearStage;
        dieAction = DieAction;
        getClear = GetClear;
    }

    private void SetupComponent()
    {
        mainCamera = Camera.main;

        uiManager = FindObjectOfType<UIManager>();
    }

    private void SetupStage()
    {
        StageData data = StageManager.Data;

        uiManager.NotifyMessage(data.message);

        MakeStage(data.Config);

        darkMode = data.DarkMode;

        ApplyMode();
    }

    private void MakeStage(StageConfig config)
    {
        if (stageConfig != null)
        {
            Destroy(stageConfig.gameObject);
        }

        stageConfig = Instantiate(config, Vector3.zero, Quaternion.identity, transform);
    }

    private void InputChangeColor()
    {
        // Check key input
        bool pressedKey = Input.GetKeyDown(changeColorKey);

        if (!pressedKey)
        {
            return;
        }

        // Check cooldown
        if (!uiManager.CanChangeMode)
        {
            return;
        }

        darkMode = !darkMode;

        ApplyMode();
    }

    private void ApplyMode()
    {
        // Change player (Activate only the player of the opposite color from the mode)
        Vector3 position = Vector3.zero;

        if (darkMode)
        {
            position = blackPlayer.transform.position;
        }
        else
        {
            position = whitePlayer.transform.position;
        }

        blackPlayer.transform.position = position;
        whitePlayer.transform.position = position;

        if (visiblePlayer)
        {
            SpawnPlayer();
        }

        // Change object and background color
        Color backgroundColor = darkMode ? Color.black : Color.white;
        Color objectColor = darkMode ? Color.white : Color.black;

        goMapButtonImage.color = objectColor;
        mainCamera.backgroundColor = backgroundColor;

        stageConfig.ChangeColor(objectColor);
        stageConfig.SetBlocksActive(backgroundColor);
        uiManager.OnChangedColor();
    }

    public void ClearStage()
    {
        if (clear)
        {
            return;
        }

        clear = true;

        uiManager.ShowClearBackgroundImage();

        StageData data = StageManager.Data;

        DataManager.Data.RecordStageClearLog(data.stageID);
        DataManager.Save();
    }

    public bool GetClear()
    {
        return clear;
    }

    public void GoMap()
    {
        SceneManager.LoadScene(2);
    }

    public void DieAction()
    {
        die = true;

        blackPlayer.gameObject.SetActive(false);
        whitePlayer.gameObject.SetActive(false);

        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        float time = UnityEngine.Random.Range(2f, 3f);

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(3);
    }

    public void SpawnPlayer()
    {
        blackPlayer.gameObject.SetActive(!darkMode);
        whitePlayer.gameObject.SetActive(darkMode);
    }
}
