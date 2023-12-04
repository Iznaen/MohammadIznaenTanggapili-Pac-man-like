using UnityEngine;

public class ChaseState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        // trigger condition for animation from the controller
        // using [SetTrigger] because the parameter was a 'Trigger' type
        // not 'Bool' or 'Int' type
        enemy.animator.SetTrigger("ChaseState");

        Debug.Log("Starting chase");
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.player != null && enemy.currentDistance < enemy.chaseDistance)
        {
            // set new destination for agent is to go to player position
            enemy.navMeshAgent.destination = enemy.player.transform.position;
        }
        else
        {
            enemy.SwitchState(enemy.patrolState);
        }

        //$Debug.Log("Chasing");
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop chasing");
    }
}
