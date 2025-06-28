using UnityEngine;

public class RotateBlock : MonoBehaviour
{
    [SerializeField]
    private float rotatePerSecond = 0.1f;
    private float angle;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float x = transform.localEulerAngles.x;
        float y = transform.localEulerAngles.y;

        angle += rotatePerSecond * 360f * Time.deltaTime;

        Vector3 euler = new Vector3(x, y, angle);
        Quaternion rotation = Quaternion.Euler(euler);

        transform.rotation = rotation;
    }
}
