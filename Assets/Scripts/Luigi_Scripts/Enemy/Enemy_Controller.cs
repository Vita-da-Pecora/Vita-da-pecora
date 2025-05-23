using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy_Controller : MonoBehaviour
{
    [field: SerializeField, Header("Patrol Settings")]
    public Transform[] PatrolPoints { get; private set; }
    [field: SerializeField] public Enemy_Patrol PatrolState { get; private set; }

    [field: SerializeField, Header("Chase Settings")]
    public Transform PlayerTransform { get; private set; }
    [field: SerializeField] public Enemy_Chase ChaseState { get; private set; }

    [field: SerializeField, Header("Animator")]
    public Animator Animator { get; private set; }

    public NavMeshAgent Agent { get; private set; }

    private EnemyState _currentState;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();

        if (Animator == null)
            Animator = GetComponentInChildren<Animator>(); // Prova ad assegnarlo automaticamente se non settato

        SetState(PatrolState);
    }

    private void Update()
    {
        if (_currentState != null)
            _currentState.OnUpdate(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_currentState != null)
            _currentState.OnCollision(this, other);
    }

    public void SetState(EnemyState _state)
    {
        if (_state == null) return;

        if (_currentState != null)
            _currentState.OnExit(this);

        _currentState = _state;

        _currentState.OnEnter(this);
    }

    private void OnDrawGizmos()
    {
        if (_currentState != null)
            _currentState.DrawGizmo(this);
    }
}
