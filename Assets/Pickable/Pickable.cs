using System;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    // Attach the type PickableType to the Inspector
    [SerializeField]
    public PickableType pickableType;

    // using the 'Action' field type with generic <Pickable>
    // to call the method OnPickablePicked() inside this script
    // since this is the script that will detect if the player touch or not touch
    // the pickable object
    public Action<Pickable> onPicked;

    // this function called when two object touch (first/n-th touch)
    private void OnTriggerEnter(Collider other) // 'other' are the other object other than where this script is attached to
    {
        // check tag, only execute if an object tagged with "Player" are colliding
        if(other.gameObject.CompareTag("Player"))
        {
            //$Debug.Log($"Pickup: {pickableType}");

            // calling the field 'onPicked' that will also call the OnPickablePicked() method
            onPicked(this);

            // destroy the game object where this script attached to
            // (in this case is the Coin object)
            Destroy(gameObject);
        }
    }

    // this function called when two object are touching
    //$private void OnTriggerStay(Collider other)
    //${
    //$    Debug.Log("Staying");
    //$}

    // this function called when two object are no longer touching
    // (after the first/n-th touch) if two object never touch in the first place
    // this function is not called
    //$private void OnTriggerExit(Collider other)
    //${
    //$    Debug.Log("Exiting");
    //$}
}
