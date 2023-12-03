using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    // To be able to access the methods inside Player.cs
    // specifically accessing PickPowerUp() method
    [SerializeField]
    private Player _player;

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

        //$Debug.Log($"Pickable List: {_pickableList.Count}");
    }

    private void OnPickablePicked(Pickable pickable)
    {
        // remove the pickable object from the _pickableList
        _pickableList.Remove(pickable);

        // update the pickable count inside the log if a pickableObject has been picked
        //$Debug.Log($"Pickable List: {_pickableList.Count}");

        // check if the player pick-up a power up type or not
        // if true, call PickPowerUp()
        if (pickable.pickableType == PickableType.PowerUp)
        {
            _player?.PickPowerUp();
        }

        if (_pickableList.Count <= 0)
        {
            Debug.Log("You win!");
        }
    }
}
