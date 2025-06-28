using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    private MapData data;

    [SerializeField]
    private TextMeshProUGUI titleName;
    [SerializeField]
    private Image image;

    public void Setup(MapData data)
    {
        this.data = data;
        titleName.text = data.TitleName;
        image.sprite = data.IconSprite;
    }

    public void LoadStage()
    {
        MapManager.Data = data;

        SceneManager.LoadScene(2);
    }
}
