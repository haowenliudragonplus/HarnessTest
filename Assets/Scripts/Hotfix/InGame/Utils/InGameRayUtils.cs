using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class InGameRayUtils
{
    private static Dictionary<Collider2D, ArrowEntity> collider2EntityDict = new Dictionary<Collider2D, ArrowEntity>(); //碰撞器 - 实体

    public static void Add(Collider2D collider, ArrowEntity entity)
    {
        if (collider == null)
        {
            CLog.Error("不能添加null的collider");
            return;
        }
        if (entity == null)
        {
            CLog.Error("不能添加null的entity");
            return;
        }
        if (collider2EntityDict.TryGetValue(collider, out var _entity))
        {
            return;
        }
        collider2EntityDict.Add(collider, entity);
    }

    public static bool Remove(Collider2D collider)
    {
        if (collider == null)
            return false;

        var ret = collider2EntityDict.Remove(collider);
        return ret;
    }

    public static ArrowEntity GetEntity(Collider2D collider)
    {
        if (collider2EntityDict.TryGetValue(collider, out var _entity))
            return _entity;
        return null;
    }
}
