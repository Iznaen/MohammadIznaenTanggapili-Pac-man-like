using UnityEngine;

public class RetreatState : BaseState
{
    private Vector3 _retreatDistance;

    public void EnterState(Enemy enemy)
    {
        // trigger condition for animation from the controller
        // using [SetTrigger] because the parameter was a 'Trigger' type
        // not 'Bool' or 'Int' type
        enemy.animator.SetTrigger("RetreatState");

        Debug.Log("Starting retreat");
    }

    public void UpdateState(Enemy enemy)
    {
        _retreatDistance = enemy.transform.position - enemy.player.transform.position;

        if (enemy.player != null)
        {
            enemy.navMeshAgent.destination = _retreatDistance;
        }

        Debug.Log("Retreating");
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop retreating");
    }
}
