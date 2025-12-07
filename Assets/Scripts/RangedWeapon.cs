using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangedWeapon : Weapon
{
    [Header("Ranged Stats")] 
    public int currentBullets;
    public int bulletCapacity;
    public int currentMagazine;
    public int magazineCapacity;
    public bool canReload = true;
    public float reloadLength;
    public Projectile projectile;
    [Header("Cache")]
    private float currentReloadTime;

    [SerializeField] private Transform firePoint;

    private void Start()
    {
        UpdateUI();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButton(0))
        {
            TryAttack();
        }
        if (currentReloadTime > 0 && canReload)
        {
            currentReloadTime -= Time.deltaTime;
            if (currentReloadTime <= 0)
            {
                Reload();
            }
        }
    }

    protected override void AttackEffects()
    {
        UpdateUI();
        Projectile newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
        newProjectile.tag = "Friendly";
        newProjectile.RotateToTarget(thisPlayer.mousePos);
        newProjectile.damage = damage;
        newProjectile.originEntity = thisPlayer;
    }

    public override void TryAttack()
    {
        if (currentBullets > 0)
        {
            base.TryAttack();
        }
    }

    public override void StartReload()
    {
        currentReloadTime = reloadLength;
    }
    protected virtual void Reload()
    {
        if (currentMagazine > 0)
        {
            currentMagazine--;
            currentBullets = bulletCapacity;
        }
    }

    protected virtual void UpdateUI()
    {
        thisPlayer.weaponText.text = $"Mags: {currentMagazine}/{magazineCapacity}\nBullets: {currentBullets}/{bulletCapacity}";
    }
}
