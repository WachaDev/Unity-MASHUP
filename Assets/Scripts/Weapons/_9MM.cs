using UnityEngine;

public class _9MM : Weapon
{
    private _9MM()
    {
        this.weaponName = "9MM";
        this.recoil = 0.5f;
        this.damage = 1.0f;
        this.fireRate = 0.5f;
        this.maxAmmo = 60;
        this.currentAmmo = this.maxAmmo;
        this.bulletDistance = 100.0f;
        this.cannonType = CannonType.Short;
        this.shootType = ShootType.Semi;
    }

    // ADD MODIFIERS OR DETAILS AS YOU WANT
    public override void Shoot(Camera viewport, LayerMask layerToHit) => base.Shoot(viewport, layerToHit);

    // ADD MODIFIERS OR DETAILS AS YOU WANT
    public override void Reload() => base.Reload();
}
