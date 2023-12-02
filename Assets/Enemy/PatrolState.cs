using UnityEngine;

public class PatrolState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Starting patrol");
    }

    public void UpdateState(Enemy enemy)
    {
        Debug.Log("Patrolling");
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop patrolling");
    }
}
