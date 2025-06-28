using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI versionText;

    private void Awake()
    {
        SetupUI();
    }

    private void Start()
    {
        SoundManager.Instance.PlayBGM("≈∏¿Ã∏”");
    }

    private void SetupUI()
    {
        versionText.text = $"v{Application.version}";
    }

    public void OnGameStartButtonClick()
    {
        SceneManager.LoadScene(1);
    }
}
