using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_AnimationTrigger : MonoBehaviour
{
    private Entity entity;
    private Entity_Combat entityCombat;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        entityCombat = GetComponentInChildren<Entity_Combat>();

    }
    private void CurrentStateTrigger()//ÓÃÓÚÍË³ö¹¥»÷×´Ì¬
    {
        entity.CallAnimationTrigger();
    }

    private void AttackTriggerOpen()
    {
        entityCombat.OpenAttackTrigger();
    }
    private void AttackTriggerClose()
    {
        entityCombat.CloseAttackTrigger();
    }
}
