using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // storing what is the current state of the Enemy AI
    private BaseState _currentState;

    // instantiate fields to store all the state that the Enemy AI have
    [HideInInspector]
    public PatrolState patrolState = new PatrolState();

    [HideInInspector]
    public ChaseState chaseState = new ChaseState();
    
    [HideInInspector]
    public RetreatState retreatState = new RetreatState();

    
    private void Awake()
    {
        // defining what is the initial state when the GameObject loaded
        _currentState = patrolState;

        _currentState.EnterState(this);
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }
}
