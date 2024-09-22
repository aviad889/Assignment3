using UnityEngine;

public class CameraController : MonoBehaviour
{
    float x_mouse_input; // flaot var for the X axis of the mouse movement
    float y_mouse_input; // float var for the Y axis of the mouse movement
    float x_rotation; // float var for camera Rotation clamping
    [SerializeField] private float mouse_sens; // Changable float for the user camera sensetivity



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock the curser to the middle of the screen
    }


    // Update is called once per frame
    void Update()
    {
        x_mouse_input = Input.GetAxis("Mouse X") * mouse_sens * Time.deltaTime; // get input from mouse movement on the X axis, double by sens and TDT 
        y_mouse_input = Input.GetAxis("Mouse Y") * mouse_sens * Time.deltaTime; // get input from mouse movement on the Y axis, double by sens and TDT 

        x_rotation -= y_mouse_input; // subtract Y input from rotation new value
        x_rotation = Mathf.Clamp(x_rotation, -90f, 90f); // clamp rotation value betwen -90° and 90° 

        transform.localRotation = Quaternion.Euler(x_rotation, 0f, 0f); // rotate the camera according to new rotation value

        transform.parent.Rotate(Vector3.up * x_mouse_input); // rotate player body according to mouse input on X axis
    }
}
