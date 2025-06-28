using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private Vector3 vector;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float waitTime;

    [SerializeField]
    private bool needCollision;
    private bool collision;

    private void Update()
    {
        if (needCollision && !collision)
        {
            return;
        }

        if (waitTime > 0f)
        {
            waitTime -= Time.deltaTime;

            return;
        }

        transform.position += vector * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.collision = true;
    }
}
