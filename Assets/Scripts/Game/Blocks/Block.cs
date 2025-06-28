using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private bool isGroup;
    public bool IsGroup => isGroup;

    [SerializeField]
    private bool lockColor;
    public bool LockColor => lockColor;

    [SerializeField]
    private Vector2[] movePositions;
    [SerializeField]
    private float moveSpeed = 0.5f;
    [SerializeField]
    private float moveErrorValue = 0.01f;

    private int currentPositionIndex;

    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    public Color Color
    {
        get => spriteRenderer.color;
        set => spriteRenderer.color = value;
    }

    private void Awake()
    {
        SetupComponent();
    }

    private IEnumerator Start()
    {
        while (movePositions.Length > 1)
        {
            yield return UpdatePositionRoutine();
        }
    }

    private void SetupComponent()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator UpdatePositionRoutine()
    {
        if (currentPositionIndex >= movePositions.Length)
        {
            currentPositionIndex = 0;
        }

        Vector2 end = movePositions[currentPositionIndex++];

        bool arrived = false;

        while (!arrived)
        {
            Vector2 start = rigidbody.position;
            Vector2 direction = (end - start).normalized;
            Vector2 position = rigidbody.position + direction * moveSpeed * Time.fixedDeltaTime;

            rigidbody.MovePosition(position);

            yield return new WaitForFixedUpdate();

            arrived = (end - rigidbody.position).sqrMagnitude <= moveErrorValue * moveErrorValue;
        }
    }
}
