using UnityEngine;

[CreateAssetMenu(fileName = "New Stage Data", menuName = "Stage Data")]
public class StageData : ScriptableObject
{
    public ulong stageID;
    public string message;

    [SerializeField]
    private Sprite icon;
    public Sprite Icon => icon;

    [SerializeField]
    private StageConfig config;
    public StageConfig Config => config;

    [SerializeField]
    private bool darkMode;
    public bool DarkMode => darkMode;
}
