using System;
using UnityEngine;

public class EnemyRangeCollider : MonoBehaviour
{
    public Entity entity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(entity.tag))
        {
            other.TryGetComponent(out Entity entityFound);
            entity.targetsInRange.Add(entityFound);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(entity.tag) && other.TryGetComponent(out Entity entityFound) && entity.targetsInRange.Contains(entityFound))
        {
            entity.targetsInRange.Remove(entityFound);
        }
    }

    private void Update()
    {
        entity.closestTarget = FindClosestTarget();
    }
    public Entity FindClosestTarget()
    {
        Entity foundEntity = null;
        foreach (Entity entityToCheck in entity.targetsInRange.ToArray())
        {
            if (!entityToCheck)
            {
                entity.targetsInRange.Remove(entityToCheck);
                continue;
            }
            if (!foundEntity || Vector3.Distance(entityToCheck.transform.position, transform.position) <
                Vector3.Distance(foundEntity.transform.position, transform.position))
            {
                foundEntity = entityToCheck;
            }
        }
        return foundEntity;
    }
}