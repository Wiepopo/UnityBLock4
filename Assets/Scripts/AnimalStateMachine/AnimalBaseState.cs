using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AnimalBaseState : MonoBehaviour
{
    public abstract void EnterState(AnimalStateManager animal);
    public abstract void UpdateState(AnimalStateManager animal);
    public abstract void OnCollisionEnter(AnimalStateManager animal);
}
