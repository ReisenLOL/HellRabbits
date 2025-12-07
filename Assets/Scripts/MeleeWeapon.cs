using System;
using System.Collections.Generic;
using Core.Extensions;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    private List<Entity> enemiesInRange = new();
    public float knockbackForce;
    public bool canRotate = true;
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string animationTrigger;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(tag) && other.TryGetComponent(out Entity isEntity))
        {
            enemiesInRange.Add(isEntity);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (!other.CompareTag(tag) && other.TryGetComponent(out Entity isEntity) && enemiesInRange.Contains(isEntity))
        {
            enemiesInRange.Remove(isEntity);
        }
    }
    protected override void Update()
    {
        base.Update();
        transform.Lookat2D(thisPlayer.mousePos);
    }

    protected override void AttackEffects()
    {
        foreach (Entity entityFound in enemiesInRange.ToArray())
        {
            entityFound.TakeDamage(damage);
            Vector3 knockbackDirection = (entityFound.transform.position - thisPlayer.transform.position).normalized;
            entityFound.rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
        //animator.SetTrigger(animationTrigger);
    }
}
