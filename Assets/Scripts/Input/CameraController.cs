using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 1;
    private Vector3 horizontalDirectionVector = new Vector3(1, 0, 0);
    private Vector3 verticalDirectionVector = new Vector3(0, 1, 0);

    private void Update()
    {
        this.HandleInput();
    }

    private void HandleInput()
    {
        this.transform.localPosition += Input.GetAxis("Horizontal") * this.horizontalDirectionVector * Time.deltaTime * this.speed;
        this.transform.localPosition += Input.GetAxis("Vertical") * this.verticalDirectionVector * Time.deltaTime * this.speed;
    }
}