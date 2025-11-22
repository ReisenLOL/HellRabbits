using System;
using Core.Extensions;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Identification")] 
    public string weaponName;
    public Sprite icon;
    public GameObject weaponSprite;
    [Header("Weapon Stats")]
    public float damage;
    public float fireRate;
    protected float currentFiringTime;
    public float range;
    public Player thisPlayer;

    protected virtual void Update()
    {
        currentFiringTime += Time.deltaTime;
        weaponSprite.transform.Lookat2D(thisPlayer.mousePos);
    }

    public void TryAttack()
    {
        if (currentFiringTime > fireRate)
        {
            currentFiringTime = 0;
            AttackEffects();
        }
    }

    protected virtual void AttackEffects()
    {
        
    }
}
