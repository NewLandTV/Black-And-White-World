using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageConfig : MonoBehaviour
{
    [SerializeField]
    private float rotationZ;
    [SerializeField]
    private float startWaitTime = 0.5f;
    [SerializeField]
    private float animationSpeed = 1f;
    public bool EndAnimation { get; private set; }

    private Block[] blocks;

    private void Awake()
    {
        SetupComponent();
    }

    private void Start()
    {
        StartRotationAnimation();
    }

    private void SetupComponent()
    {
        blocks = GetComponentsInChildren<Block>();

        List<Block> list = new List<Block>(blocks);

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].IsGroup)
            {
                list.RemoveAt(i);
            }
        }

        blocks = list.ToArray();
    }

    private void StartRotationAnimation()
    {
        if (rotationZ == 0f)
        {
            EndAnimation = true;

            return;
        }

        StartCoroutine(AnimationRoutine());
    }

    private IEnumerator AnimationRoutine()
    {
        yield return new WaitForSeconds(startWaitTime);

        Quaternion start = transform.rotation;
        Quaternion end = Quaternion.Euler(0f, 0f, rotationZ);

        for (float t = 0f; t < 1f; t += Time.deltaTime / animationSpeed)
        {
            transform.rotation = Quaternion.Lerp(start, end, t);

            yield return null;
        }

        transform.rotation = end;
        EndAnimation = true;
    }

    public void ChangeColor(Color color)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (!blocks[i].LockColor)
            {
                blocks[i].Color = color;
            }
        }
    }

    public void SetBlocksActive(Color backgroundColor)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].gameObject.SetActive(blocks[i].Color != backgroundColor);
        }
    }
}
