using UnityEngine;

[System.Serializable]
public class Enemy_Chase : EnemyState
{
    [SerializeField] private float chaseRadius = 20f;
    [SerializeField] private float attackDistance = 2f;

    private bool isAttacking = false;

    public override void OnEnter(Enemy_Controller _controller)
    {
        isAttacking = false;
        _controller.Agent.SetDestination(_controller.PlayerTransform.position);
    }

    public override void OnUpdate(Enemy_Controller _controller)
    {
        float distanceToPlayer = Vector3.Distance(_controller.transform.position, _controller.PlayerTransform.position);

        // Controllo invisibilità
        Mimetismo mimetismo = _controller.PlayerTransform.GetComponent<Mimetismo>();
        if (mimetismo != null && mimetismo.isInvisible)
        {
            isAttacking = false;
            _controller.Animator.SetBool("isAttacking", false);
            _controller.Agent.ResetPath();
            _controller.SetState(_controller.PatrolState);
            return;
        }

        // Se è troppo lontano, torna in patrol
        if (distanceToPlayer > chaseRadius)
        {
            isAttacking = false;
            _controller.Animator.SetBool("isAttacking", false);
            _controller.SetState(_controller.PatrolState);
            return;
        }

        // Se è abbastanza vicino, attacca
        if (distanceToPlayer <= attackDistance)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                _controller.Agent.ResetPath(); // Ferma il movimento
                _controller.Animator.SetBool("isAttacking", true);
            }
        }
        else
        {
            // Insegue il giocatore
            isAttacking = false;
            _controller.Animator.SetBool("isAttacking", false);
            _controller.Agent.SetDestination(_controller.PlayerTransform.position);
        }
    }


    public override void OnExit(Enemy_Controller _controller)
    {
        isAttacking = false;
        _controller.Animator.SetBool("isAttacking", false);
    }

    public override void OnCollision(Enemy_Controller _controller, Collider _collision)
    {
        Enemy_Controller enemyController = _collision.GetComponent<Enemy_Controller>();
    }

    public override void DrawGizmo(Enemy_Controller _controller)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_controller.transform.position, chaseRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_controller.transform.position, attackDistance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(_controller.transform.position, _controller.PlayerTransform.position);
    }
}
