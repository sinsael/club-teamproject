using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : Entity, IPointerClickHandler
{
    public Transform PlayerCheck;
    public Transform ShootRangeCheck;
    public float PlayerCheckRadius;
    public float ShootRangeCheckRadius;
    public LayerMask WhatIsPlayer;

    public override void Start()
    {
        base.Start();

    }

    public override void Update()
    {
        base.Update();

        PlayerTracking();
        ShootPlayer();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

    }


    public void PlayerTracking()
    {
        if (PlayerInSight())
        {
            // Implement player tracking behavior here
        }
    }

    public void ShootPlayer()
    {
        if (PlayerInShootRange())
        {
            Attack();
        }
    }
    public void Attack()
    {
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PlayerCheck.position, PlayerCheckRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(ShootRangeCheck.position, ShootRangeCheckRadius);
    }

    public bool PlayerInSight()
    {
        Collider2D cols = Physics2D.OverlapCircle(PlayerCheck.position, PlayerCheckRadius, WhatIsPlayer);

        Player playerscripts = cols.GetComponent<Player>();

        if (playerscripts == null)
        {
            return false;
        }

        Vector2 origin = (Vector2)PlayerCheck.position;
        Vector2 targetPos = (Vector2)cols.transform.position;
        Vector2 direction = (targetPos - origin).normalized;
        float distanceToPlayer = Vector2.Distance(origin, targetPos);

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distanceToPlayer, WhatIsPlayer);

        return hit.collider == null;
    }

    public bool PlayerInShootRange()
    {
        Collider2D cols = Physics2D.OverlapCircle(ShootRangeCheck.position, ShootRangeCheckRadius, WhatIsPlayer);

        Player playerscripts = cols.GetComponent<Player>();

        if (playerscripts == null)
        {
            return false;
        }

        Vector2 origin = (Vector2)ShootRangeCheck.position;
        Vector2 targetPos = (Vector2)cols.transform.position;
        Vector2 direction = (targetPos - origin).normalized;
        float distanceToPlayer = Vector2.Distance(origin, targetPos);

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distanceToPlayer, WhatIsPlayer);

        return hit.collider == null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}
