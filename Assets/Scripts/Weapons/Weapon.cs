using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void Start() => input.GetComponent<InputManager>();
    private void Update() => time += Time.deltaTime;

    protected enum CannonType { Large, Short, Duplex }

    protected enum ShootType { Auto, Semi, Burst }
    protected Enum cannonType;
    protected Enum shootType;

    protected float fireRate;
    public string weaponName;
    protected float recoil;
    protected float bulletDistance;

    [HideInInspector] public float damage;
    [HideInInspector] public int maxAmmo;
    public int currentAmmo;
    private int CurrentAmmo
    {
        get
        {
            if (currentAmmo >= maxAmmo)
                return currentAmmo = maxAmmo;
            else
                return currentAmmo;
        }
        set {}
    }
    
    [SerializeField] protected InputManager input;
    private float time = 0.0f;

    protected virtual bool FireRate()
    {
        float delay = 0.0f;
        delay += Time.deltaTime;

        if (time > this.fireRate)
            return true;
        else
            return false;
    }

    public bool CanShoot => input.fire && this.currentAmmo > 0 && FireRate();
    public virtual void Shoot(Camera viewport, LayerMask layerToHit)
    {
        if (CanShoot)
        {
            time = 0.0f;
            Ray bullet = viewport.ViewportPointToRay(Vector3.one * 0.5f);
            RaycastHit hit;
            //? Debug
            Debug.DrawRay(bullet.origin, bullet.direction, Color.red, 3f);
            this.currentAmmo--;
            if (Physics.Raycast(bullet.origin, bullet.direction, out hit, this.bulletDistance, layerToHit))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(this.damage);
                }
            }
        }
    }

    public virtual void Reload() {}
}