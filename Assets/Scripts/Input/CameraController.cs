using UnityEngine;

public class CameraController : MonoBehaviour
{
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable SA1401 // Fields should be private
    public float Speed = 1;
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore CA1051 // Do not declare visible instance fields

    private Vector3 horizontalDirectionVector = new Vector3(1, 0, 0);
    private Vector3 verticalDirectionVector = new Vector3(0, 1, 0);

    private void Update()
    {
        this.HandleInput();
    }

    private void HandleInput()
    {
        this.transform.localPosition += Input.GetAxis("Horizontal") * this.horizontalDirectionVector * Time.deltaTime * this.Speed;
        this.transform.localPosition += Input.GetAxis("Vertical") * this.verticalDirectionVector * Time.deltaTime * this.Speed;
    }
}