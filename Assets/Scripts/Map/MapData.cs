using UnityEngine;

[CreateAssetMenu(fileName = "New Map Data", menuName = "Map Data")]
public class MapData : ScriptableObject
{
    [SerializeField]
    private string titleName;
    public string TitleName => titleName;
    [SerializeField]
    private Sprite iconSprite;
    public Sprite IconSprite => iconSprite;

    [SerializeField]
    private StageData[] stages;
    public StageData[] Stages => stages;
}
