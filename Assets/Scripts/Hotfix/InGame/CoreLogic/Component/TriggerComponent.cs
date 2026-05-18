using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

/// <summary>
/// 触发器检测组件
/// </summary>
public class TriggerComponent : MonoBehaviour
{
    private ArrowEntity selfEntity;

    private void OnEnable()
    {
        selfEntity = InGameRayUtils.GetEntity(gameObject.transform.GetComponentInChildren<Collider2D>());
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        var otherEntity = InGameRayUtils.GetEntity(other);
        if (otherEntity == null)
        {
            CLog.Error($"otherEntity is null");
            return;
        }
        if (selfEntity == null)
        {
            CLog.Error($"selfEntity is null");
            return;
        }
        if (selfEntity.ArrowData.State != EArrowState.MoveForward)
            return;
        if (otherEntity.ArrowData.State != EArrowState.Idle)
            return;
        if (otherEntity == selfEntity)
            return;

        selfEntity.ColliderToOther();
        otherEntity.BeCollider();
    }
}
