using System;
using UnityEngine;

public class Player : Entity
{
    private Vector3 moveDirection;
    public Vector3 mousePos;
    [SerializeField] private Camera cam;
    [Header("Weapons")] 
    [SerializeField] private Transform weaponFolder; 
    public Weapon primary;
    private Weapon primaryWeaponInstance;
    public Weapon secondary;
    private Weapon secondaryWeaponInstance;
    private Weapon currentlyEquipped;
    private void Start()
    {
        primaryWeaponInstance = Instantiate(primary, weaponFolder);
        primaryWeaponInstance.thisPlayer = this;
        //secondaryWeaponInstance = Instantiate(secondary, weaponFolder);
        currentlyEquipped = primaryWeaponInstance;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }
    private void HandleMovement()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,cam.nearClipPlane));
    }

    private void Update()
    {
        HandleMovement();
        if (Input.GetMouseButton(0))
        {
            currentlyEquipped.TryAttack();
        }
    }
}
