using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidBody;

    [SerializeField]
    private float _speed;

    // accessing Camera method and reference it into Player object
    [SerializeField]
    private Camera _camera;

    // pre-load all the functions before the game start
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        HideAndLockCursor();
    }

    private void HideAndLockCursor()
    {
        // call the method Cursor and set the visibility to false
        Cursor.visible = false;
        // lock the cursor to the center of the player object
        Cursor.lockState = CursorLockMode.Locked;

        // click on the 'Game' window to hide and lock cursor when
        // the game is running, hit ESC to reveal the cursor again
    }

    void Update()
    {
        // horizontal inputs 'a' 'd' 'arrowLeft' 'arrowRight'
        float horizontal = Input.GetAxis("Horizontal");

        // vertical inputs 'w' 's' 'arrowUp' 'arrowDown'
        float vertical = Input.GetAxis("Vertical");

        // get direction of the camera in horizontal axis
        Vector3 horizontalDirection = horizontal * _camera.transform.right;
        // get direction of the camera in vertical axis
        Vector3 verticalDirection = vertical * _camera.transform.forward;

        verticalDirection.y = 0;
        horizontalDirection.y = 0;

        Vector3 movementDirection = verticalDirection + horizontalDirection;

        _rigidBody.velocity = _speed * Time.deltaTime * movementDirection;

        //>Debug.Log("Horizontal: " + horizontal);
        //>Debug.Log("Vertical: " + vertical);
    }
}