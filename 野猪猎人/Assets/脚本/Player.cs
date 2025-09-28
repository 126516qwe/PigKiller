using UnityEngine;

public class Player : Entity
{
    public static Player instance;
    public PlayerInputSet input { get; private set; }

    public Entity_Health health { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_RunState runState { get; private set; }
    public Player_AttackState attackState { get; private set; }
    public Player_DieState dieState { get; private set; }
    public Player_EnemyDetector playerEnemyDetector { get; private set; }

    public Vector2 moveInput { get; private set; }


    [Header("Movement details")]
    public float moveSpeed;


    [Header("��������")]
    public Vector3 attackVelocity;
    public float attackMoveSpeed=2f;
    public float attackVelocityDuration = .1f;

    protected override void Awake()
    {
        base.Awake();

        instance = this;

        health = GetComponent<Entity_Health>();

        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        runState = new Player_RunState(this, stateMachine, "move");
        attackState = new Player_AttackState(this, stateMachine, "attack");
        dieState = new Player_DieState(this, stateMachine, "die");

        playerEnemyDetector = GetComponentInChildren<Player_EnemyDetector>();
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        health.OnDeath += HandleDeath;
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();


    }

    private void HandleDeath()
    {
        // ֻ�е�ǰ��������״̬ʱ���л�
        if (stateMachine.currentState != dieState)
        {
            stateMachine.ChangeState(dieState);
        }
    }

    private void OnDestroy()
    {
        // ȡ�������¼�
        if (health != null)
            health.OnDeath -= HandleDeath;
    }
}
