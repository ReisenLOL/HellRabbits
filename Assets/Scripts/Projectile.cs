using Core.Extensions;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float timeUntilAutoDestroy;
    public float knockbackForce;
    public Entity originEntity;

    public void RotateToTarget(Vector3 direction)
    {
        transform.Lookat2D(direction);
    }

    private void Start()
    {
        Destroy(gameObject, timeUntilAutoDestroy);
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }

    protected virtual void OnProjectileHit(Entity entityHit)
    {
        entityHit.TakeDamage(damage);
        Vector3 knockbackDirection = (entityHit.transform.position - originEntity.transform.position).normalized;
        entityHit.rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(tag) && other.TryGetComponent(out Entity isEntity))
        {
            OnProjectileHit(isEntity);
        }
    }
}