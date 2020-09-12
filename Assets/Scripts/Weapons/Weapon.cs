using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void Update()
    {
        fireRateTimer += Time.deltaTime;

        if (isReloading)
            reloadTimer += Time.deltaTime;
    }

    protected enum CannonType { Large, Short, Duplex }

    protected enum ShootType { Auto, Semi, Burst }
    protected CannonType cannonType;
    protected ShootType shootType;

    [Header("References")]
    [SerializeField] private Transform gunZone;

    [Header("Properties")]
    [SerializeField] protected string weaponName;
    [SerializeField] protected float damage;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float recoil;
    [SerializeField] protected float bulletDistance;

    [Header("Max's")]
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected int maxCartridge;

    [Header("Current's")]
    [SerializeField] protected int currentCartridge;
    private int CurrentCartridge
    {
        get
        {
            if (currentCartridge > maxCartridge)
                return currentCartridge = maxCartridge;
            else
                return currentCartridge;
        }
    }

    [SerializeField] protected int currentAmmo;
    private int CurrentAmmo
    {
        get
        {
            if (currentAmmo > maxAmmo)
                return currentAmmo = maxAmmo;
            else
                return currentAmmo;
        }
    }

    private float fireRateTimer;
    private float reloadTimer;
    protected float reloadTime;
    [SerializeField] private bool isReloading;
    [SerializeField] private bool CanShoot => FireRate() && this.currentCartridge > 0 && !isReloading;
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
        if (fireRateTimer > this.fireRate)
            return true;
        else
            return false;
    }

    public virtual void Shoot(Camera viewport, LayerMask layerToHit)
    {
        if (CanShoot)
        {
            fireRateTimer = 0.0f;
            Ray bullet = viewport.ViewportPointToRay(Vector3.one * 0.5f);
            RaycastHit hit;
            this.currentCartridge--;

            if (Physics.Raycast(bullet.origin, bullet.direction, out hit, this.bulletDistance, layerToHit))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                    enemy.TakeDamage(this.damage);
            }
        }
    }

    public virtual void Aim(Camera cam) { }

    public virtual void Reload()
    {
        if (currentAmmo > 0)
        {
            if (isReloading == false)
                Invoke("Reload", this.reloadTime);
        }
        else
            return;

        if (this.currentCartridge < this.maxCartridge)
        {
            this.isReloading = true;
            if (this.reloadTimer > this.reloadTime)
            {
                //? Debug
                Debug.Log("Reloading");

                int need =  this.maxCartridge - this.currentCartridge;
                if (need > this.currentAmmo)
                {
                    this.currentCartridge += this.currentAmmo;
                    this.currentAmmo = 0;
                }
                else
                {
                    this.currentAmmo -= this.maxCartridge - this.currentCartridge;
                    this.currentCartridge = need + this.currentCartridge;
                }

                this.reloadTimer = 0.0f;
                this.isReloading = false;
            }
        }
    }
}