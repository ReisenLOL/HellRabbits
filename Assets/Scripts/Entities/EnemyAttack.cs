using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Enemy thisEnemy;
    public float currentAttackTime;
    public float attackSpeed;
    public bool TryAttack()
    {
        currentAttackTime += Time.deltaTime;
        if (currentAttackTime > attackSpeed && thisEnemy.closestTarget)
        {
            currentAttackTime = 0f;
            Attack();
            return true;
        }

        return false;
    }

    public virtual void Attack()
    {
        //insert attack code here
    }
}
