using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // this field stores all the transform of the Waypoints
    [SerializeField]
    public List<Transform> Waypoints = new List<Transform>();

    // store distance to trigger ChaseState
    [SerializeField]
    public float chaseDistance;

    // store player transform position
    [SerializeField]
    public Player player;

    // store current distance between player and enemy
    [HideInInspector]
    public float currentDistance;

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

    // accessing animator component inside enemy object
    // through this field
    [HideInInspector]
    public Animator animator;

    // a method to switch state
    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    // kill enemy
    public void Dead()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        // get component from Animator store into animator field
        animator = GetComponent<Animator>();

        // defining what is the initial state when the GameObject loaded
        _currentState = patrolState;

        _currentState.EnterState(this);

        // get component from NavMeshAgent store into navMeshAgent field
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (player != null)
        {
            // accessing player power-up status via field type: Action
            // add each retreat condition based on the power-up status
            player.onPowerUpStart += StartRetreating;
            player.onPowerUpStop += StopRetreating;
        }
    }

    private void Update()
    {
        currentDistance = Vector3.Distance(transform.position, player.transform.position);

        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }

        //$Debug.Log(currentDistance);
    }

    private void StartRetreating()
    {
        SwitchState(retreatState);
    }

    private void StopRetreating()
    {
        SwitchState(patrolState);
    }

    // enemy kill player
    private void OnCollisionEnter(Collision collision)
    {
        if (_currentState != retreatState)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().Dead();
            }
        }
    }
}