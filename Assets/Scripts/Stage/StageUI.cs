using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    private StageData data;

    [SerializeField]
    private TextMeshProUGUI level;
    private Image image;

    private void Awake()
    {
        SetupComponent();
    }

    private void SetupComponent()
    {
        image = GetComponent<Image>();
    }

    public void Setup(int level, StageData data)
    {
        this.data = data;

        string clearDateString = DataManager.Data.GetClearDateStringWithStageID(data.stageID);
        string levelText = $"{level}";

        if (clearDateString != null)
        {
            levelText += $"\n<color=#c7afea>¡î</color><size=16>{clearDateString}</size>";
        }

        this.level.text = levelText;

        image.sprite = data.Icon;
    }

    public void Play()
    {
        StageManager.Data = data;

        SceneManager.LoadScene(3);
    }
}
