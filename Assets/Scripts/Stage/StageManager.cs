using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageData Data;

    [SerializeField]
    private StageUI stagePrefab;
    [SerializeField]
    private Transform stageParent;
    [SerializeField, Range(1, 100)]
    private int makeStageCount = 10;
    private List<StageUI> stagePooling;

    private void Awake()
    {
        SetupPooling();
        SetupUI(MapManager.Data.Stages);
    }

    private void SetupPooling()
    {
        stagePooling = new List<StageUI>(makeStageCount);

        for (int i = 0; i < makeStageCount; i++)
        {
            MakeStage();
        }
    }

    public void SetupUI(StageData[] stages)
    {
        for (int i = 0; i < stages.Length; i++)
        {
            int level = stages.Length - i;
            StageData data = stages[level - 1];

            AddStage(level, data);
        }
    }

    private void AddStage(int level, StageData data)
    {
        StageUI instance = GetStage();

        instance.Setup(level, data);
        instance.gameObject.SetActive(true);
    }

    private StageUI MakeStage()
    {
        StageUI instance = Instantiate(stagePrefab, stageParent);

        instance.gameObject.SetActive(false);

        stagePooling.Add(instance);

        return instance;
    }

    private StageUI GetStage()
    {
        for (int i = stagePooling.Count - 1; i >= 0; i--)
        {
            if (!stagePooling[i].gameObject.activeSelf)
            {
                return stagePooling[i];
            }
        }

        return MakeStage();
    }

    public void GoToLobby()
    {
        SceneManager.LoadScene(1);
    }
}
