using System;
using TMPro;
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
    [Header("UI")]
    public TextMeshProUGUI weaponText;
    private void Start()
    {
        primaryWeaponInstance = Instantiate(primary, weaponFolder);
        primaryWeaponInstance.thisPlayer = this;
        //secondaryWeaponInstance = Instantiate(secondary, weaponFolder);
        //secondaryWeaponInstance.thisPlayer = this;
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

    private void WeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            primaryWeaponInstance.gameObject.SetActive(true);
            currentlyEquipped = primaryWeaponInstance;
            secondaryWeaponInstance.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            secondaryWeaponInstance.gameObject.SetActive(true);
            currentlyEquipped = secondaryWeaponInstance;
            primaryWeaponInstance.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        HandleMovement();
        WeaponSwitch();
        if (Input.GetMouseButton(0))
        {
            currentlyEquipped.TryAttack();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentlyEquipped.StartReload();
        }
    }
}
