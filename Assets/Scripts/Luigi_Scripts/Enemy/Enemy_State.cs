using UnityEngine;

[System.Serializable]
public abstract class EnemyState
{
    public abstract void OnEnter(Enemy_Controller _controller);
    public abstract void OnUpdate(Enemy_Controller _controller);
    public abstract void OnExit(Enemy_Controller _controller);
    public abstract void OnCollision(Enemy_Controller _controller, Collider _collision);

    public abstract void DrawGizmo(Enemy_Controller _controller);
}

