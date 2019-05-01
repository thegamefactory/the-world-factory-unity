using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 1;
    private Vector3 horizontalDirectionVector = new Vector3(1, 0, 0);
    private Vector3 verticalDirectionVector = new Vector3(0, 1, 0);

    void Update()
    {
        handleInput();
    }

    void handleInput()
    {
        this.transform.localPosition += Input.GetAxis("Horizontal") * horizontalDirectionVector * Time.deltaTime * speed;
        this.transform.localPosition += Input.GetAxis("Vertical") * verticalDirectionVector * Time.deltaTime * speed;
    }
}
