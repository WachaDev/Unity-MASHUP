using UnityEngine;

public class UseWeapon : MonoBehaviour {
    [Header("References")]
    [SerializeField] private InputManager input = null;
    [SerializeField] private Camera cam = null;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Weapon anyWeapon;
    [SerializeField] private Animator weaponAnimations;
    public static bool WeaponOnHand;

    private void Start() => enemyLayer = LayerMask.GetMask("Enemy");

    private void Update() {
        anyWeapon = GetComponentInChildren<Weapon>();
        if (anyWeapon != null)
            weaponAnimations = GetComponentInChildren<Animator>();

        PickUpWeapon(anyWeapon);
    }

    private void PickUpWeapon(Weapon weapon) {
        if (weapon != null) {
            WeaponOnHand = true;
            if (input.fire) {
                weapon.Shoot(cam, enemyLayer);
                if (weapon.isShooting) {
                    weaponAnimations.SetBool("isShooting", true);
                    weaponAnimations.SetFloat("fireRate", weapon.fireRate);
                }
            }  
            if (input.fire == false || weapon.CurrentCartridge <= 0)
                weaponAnimations.SetBool("isShooting", false);


            if (input.aim) {
                weapon.Aim(cam);
                weaponAnimations.SetBool("isAiming", true);

            } else
                weaponAnimations.SetBool("isAiming", false);

            if (input.reload) {
                weapon.Reload();

                if (weapon.isReloading) {
                    weaponAnimations.SetFloat("reloadTime", weapon.reloadTime);
                    weaponAnimations.SetBool("isReloading", true);
                }
            }
            if (weapon.isReloading == false)
                weaponAnimations.SetBool("isReloading", false);
        }
    }
}