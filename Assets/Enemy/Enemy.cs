using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // this field stores all the transform of the Waypoints
    [SerializeField]
    public List<Transform> Waypoints = new List<Transform>();

    // storing what is the current state of the Enemy AI
    private BaseState _currentState;

    // instantiate fields to store all the state that the Enemy AI have
    [HideInInspector]
    public PatrolState patrolState = new PatrolState();

    [HideInInspector]
    public ChaseState chaseState = new ChaseState();

    [HideInInspector]
    public RetreatState retreatState = new RetreatState();

    // this field will stored the NavMeshAgent component from
    // the game object (enemy) specifically to store the
    // destination property from the NavMeshAgent component
    [HideInInspector]
    public NavMeshAgent navMeshAgent;

    
    private void Awake()
    {
        // defining what is the initial state when the GameObject loaded
        _currentState = patrolState;

        _currentState.EnterState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }
}
