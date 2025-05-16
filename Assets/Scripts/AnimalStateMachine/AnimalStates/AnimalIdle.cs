using UnityEngine;

public class AnimalIdle : AnimalBaseState
{
    public override void EnterState(AnimalStateManager animal)
    {
        print("Idling");
    }
    public override void UpdateState(AnimalStateManager animal)
    {

    }
    public override void OnCollisionEnter(AnimalStateManager animal)
    {

    }
}