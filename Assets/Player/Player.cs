using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // using field type: Action to access the methods
    // from Enemy.cs
    public Action onPowerUpStart;
    public Action onPowerUpStop;

    [SerializeField]
    private float _speed;

    // accessing Camera method and reference it into Player object
    [SerializeField]
    private Camera _camera;

    private bool _isPowerUpActive = false;

    [SerializeField]
    private float _powerUpDuration;

    [SerializeField]
    private int _health;

    [SerializeField]
    private TMP_Text _healthText;

    [SerializeField]
    private Transform _respawnPoint;

    // using Coroutine field to call IEnumerator [StartPowerUp()] method
    private Coroutine _powerUpCoroutine;

    private Rigidbody _rigidBody;

    public void Dead()
    {
        _health--;

        if (_health > 0)
        {
            transform.position = _respawnPoint.transform.position;
        }
        else
        {
            _health = 0;
            Debug.Log("You lose :(");
        }

        UpdateHealth();
    }

    // method to determine if player has pick or has not pick
    // the power-up
    public void PickPowerUp()
    {
        Debug.Log("Pick Power-up!");

        if (_powerUpCoroutine != null)
        {
            StopCoroutine(_powerUpCoroutine);
        }

        _powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    // method to count the seconds of how long the power-up
    // should be active
    private IEnumerator StartPowerUp()
    {
        if (onPowerUpStart != null)
        {
            _isPowerUpActive = true;
            onPowerUpStart();
            Debug.Log("Start Power-up!");
        }

        yield return new WaitForSeconds(_powerUpDuration);

        if (onPowerUpStop != null)
        {
            _isPowerUpActive = false;
            onPowerUpStop();
            Debug.Log("Power-up has stopped.");
        }
    }

    // pre-load all the functions before the game start
    private void Awake()
    {
        UpdateHealth();
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

    // check collision with enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (_isPowerUpActive == true)
        {
            // call Dead() from enemy if collide with enemy
            // while power up active
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    private void UpdateHealth()
    {
        _healthText.text = "Health: " + _health;
    }
}