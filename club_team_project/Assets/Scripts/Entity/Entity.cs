using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    protected StateMachine stateMachine;
    public Weapon weaponStat;

    public virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
    }

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
        stateMachine.UpdateActiveState();
    }

    public virtual void FixedUpdate()
    { 
       stateMachine.FixedUpdateActiveState();
    }



    public virtual void SetVelocity(float Xvelocity, float Yvelocity)
    {
        rb.linearVelocity = new Vector2(Xvelocity, Yvelocity);
    }

    public virtual void EntityDeath()
    { }

    public bool TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }

    public bool TakeDamage()
    {
        throw new NotImplementedException();
    }
}
