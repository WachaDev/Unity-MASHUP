using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void Update() => time += Time.deltaTime;

    protected enum CannonType { Large, Short, Duplex }

    protected enum ShootType { Auto, Semi, Burst }
    protected Enum cannonType;
    protected Enum shootType;

    [Header("Properties")]
    [SerializeField] protected string weaponName;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float recoil;
    [SerializeField] protected float bulletDistance;

    [SerializeField] protected float damage;
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected int currentAmmo;
    private int CurrentAmmo
    {
        get
        {
            if (currentAmmo >= maxAmmo)
                return currentAmmo = maxAmmo;
            else
                return currentAmmo;
        }
    }
    
    private float time = 0.0f;
    [SerializeField] private bool CanShoot => this.currentAmmo > 0 && FireRate();
    [SerializeField] private Transform gunZone;
    protected Vector3 alignPosition;

    //* Pick up Weapon
    protected virtual void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.transform.SetParent(gunZone);
            this.gameObject.transform.position = gunZone.transform.position;
            this.gameObject.transform.localEulerAngles = this.alignPosition;

            this.gameObject.GetComponent<BoxCollider>().enabled = false;

            // other.gameObject.AddComponent<>();
        }    
    }

    protected virtual bool FireRate()
    {
        if (time > this.fireRate)
            return true;
        else
            return false;
    }
    
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

    public virtual void Aim(Camera cam) {}

    public virtual void Reload() {}
}