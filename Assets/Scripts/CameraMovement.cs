using UnityEngine;

class CameraMovement : MonoBehaviour
{
    public Transform PlayerTr;
    public Vector3 Offset;
    private void Update()
    {
        transform.position = PlayerTr.position + Offset;
    }
}
