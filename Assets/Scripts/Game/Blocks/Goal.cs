using System.Collections;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private new CircleCollider2D collider;

    [SerializeField]
    private float waitVisibleTIme;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
    }

    private IEnumerator Start()
    {
        if (waitVisibleTIme <= 0f)
        {
            yield break;
        }

        spriteRenderer.enabled = false;
        collider.enabled = false;

        yield return new WaitForSeconds(waitVisibleTIme);

        spriteRenderer.enabled = true;
        collider.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        GameManager.clearStage();
    }
}
