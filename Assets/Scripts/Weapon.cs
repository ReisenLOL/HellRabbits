using System;
using Core.Extensions;
using TMPro;
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

    public virtual void TryAttack()
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
    public virtual void StartReload()
    {
        //nothing for melee
    }
}
