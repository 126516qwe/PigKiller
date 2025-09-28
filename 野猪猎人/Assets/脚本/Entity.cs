using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }
    protected StateMachine stateMachine;

    [Header("Æ½»¬×ªÏò")]
    public float rotationSmoothTime = 0.1f;
    private float currentTurnVelocity;



    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        stateMachine = new StateMachine();

    }



    protected virtual void Start()
    {
       
    }
    protected virtual void Update()
    {

        stateMachine.UpdateActiveState();
    }

    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
    }

    public void SetVelocity(float xVelocity, float yVelocity, float zVelocity, float moveSpeed=1)
    {
        Vector3 fixVelocity = new Vector3(xVelocity, yVelocity, zVelocity);

        rb.velocity = fixVelocity.normalized*moveSpeed;

        if (fixVelocity.magnitude > 0.1f)
            SetDirection(fixVelocity);
    }
    public void SetDirection(Vector3 inputDirection)
    {
        if (inputDirection.magnitude < 0.1f)
            return;

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentTurnVelocity, rotationSmoothTime);

        transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
    }
}
