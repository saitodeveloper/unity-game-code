using UnityEngine;

public abstract class CombactableAbstractBehaviour : MonoBehaviour
{

    public abstract void OnCombatStart();
    public abstract void OnCombatPause();
    public abstract void OnCombatFinished();
    public abstract void OnForceStop();
    public abstract void OnReleaseObject();
    public abstract bool IsEnemyAlive();
}