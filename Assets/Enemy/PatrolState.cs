using UnityEngine;

public class PatrolState : BaseState
{
    private bool _isMoving;
    private Vector3 _destination;

    public void EnterState(Enemy enemy)
    {
        _isMoving = false;

        // trigger condition for animation from the controller
        // using [SetTrigger] because the parameter was a 'Trigger' type
        // not 'Bool' or 'Int' type
        enemy.animator.SetTrigger("PatrolState");

        //$Debug.Log("Starting patrol");
    }

    public void UpdateState(Enemy enemy)
    {
        // switch to patrol state if conditions met
        if (enemy.currentDistance < enemy.chaseDistance)
        {
            enemy.SwitchState(enemy.chaseState);
        }

        if (!_isMoving)
        {
            _isMoving = true;

            // generate a random number between 0-6 and
            // store it into [int index]
            int index = UnityEngine.Random.Range(0, enemy.Waypoints.Count);

            // get a waypoint with the randomly generated index
            // store it into [_destination]
            _destination = enemy.Waypoints[index].position;

            enemy.navMeshAgent.destination = _destination;

            Debug.Log($"Going to: Waypoint {index + 1}");
        }
        else
        {
            // check if enemy position and destination position is LE 0.1
            // if true then enemy reach its destination set _isMoving to false
            if (Vector3.Distance(_destination, enemy.transform.position) <= 0.1)
            {
                _isMoving = false;
            }
        }

        //$Debug.Log("Patrolling");
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop patrolling");
    }
}
