using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    // To be able to access the methods inside Player.cs
    // specifically accessing PickPowerUp() method
    [SerializeField]
    private Player _player;

    // accessing ScoreManager.cs
    [SerializeField]
    private ScoreManager _scoreManager;

    private List<Pickable> _pickableList = new List<Pickable>();

    void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        // get all the pickable objects inside the scene that contains the #Pickable script
        Pickable[] pickableObjects = GameObject.FindObjectsOfType<Pickable>();

        // add all the pickable objects into _pickableList
        for (int i = 0; i < pickableObjects.Length; i++)
        {
            _pickableList.Add(pickableObjects[i]);

            // referencing the onPicked field type from Pickable script
            // at the current index inside the _pickableList
            pickableObjects[i].onPicked += OnPickablePicked;
        }

        // call SetMaxScore() from ScoreManager.cs using the
        // [private ScoreManager _scoreManager] field type
        // passing the argument [_pickableList.count] to get
        // all the pickable inside the scene
        _scoreManager.SetMaxScore(_pickableList.Count);

        Debug.Log($"Pickable List: {_pickableList.Count}");
    }

    private void OnPickablePicked(Pickable pickable)
    {
        // remove the pickable object from the _pickableList
        _pickableList.Remove(pickable);

        // update the pickable count inside the log if a pickableObject has been picked
        //$Debug.Log($"Pickable List: {_pickableList.Count}");

        // add score +1 if player pickup pickable
        if (_scoreManager != null)
        {
            _scoreManager.AddScore(1);
        }

        // check if the player pick-up a power up type or not
        // if true, call PickPowerUp()
        if (pickable.pickableType == PickableType.PowerUp)
        {
            _player?.PickPowerUp();
        }

        if (_pickableList.Count <= 0)
        {
            // Debug.Log("You win!");

            SceneManager.LoadScene("WinScreen");
        }
    }
}
