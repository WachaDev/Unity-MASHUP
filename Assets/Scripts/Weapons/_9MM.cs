using UnityEngine;

public class _9MM : Weapon
{
    private _9MM()
    {
        this.weaponName = "9MM";
        this.recoil = 0.5f;
        this.damage = 5.0f;
        this.fireRate = 0.5f;

        this.maxAmmo = 60;
        this.currentAmmo = this.maxAmmo;
        this.maxCartridge = 6;
        this.currentCartridge = this.maxCartridge;

        this.reloadTime = 1.5f;
        this.bulletDistance = 100.0f;

        this.cannonType = CannonType.Short;
        this.shootType = ShootType.Semi;

        this.alignPosition = new Vector3(-4.5f, 1f, 90f);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        other.gameObject.AddComponent<_9MM>();
    }

    // ADD SOME STUFF OR DETAILS AS YOU WANT
    public override void Shoot(Camera viewport, LayerMask layerToHit) => base.Shoot(viewport, layerToHit);

    // ADD SOME STUFF OR DETAILS AS YOU WANT
    public override void Aim(Camera cam) => base.Aim(cam);

    // ADD SOME STUFF OR DETAILS AS YOU WANT
    public override void Reload() => base.Reload();
}
