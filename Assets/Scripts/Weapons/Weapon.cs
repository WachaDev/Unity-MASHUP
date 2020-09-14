using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void Update()
    {
        fireRateTimer += Time.deltaTime;

        if (isReloading)
            reloadTimer += Time.deltaTime;
    }
    private float fireRateTimer;
    private float reloadTimer;

    protected enum CannonType { Large, Short, Duplex }
    protected enum ShootType { Auto, Semi, Burst }
    protected CannonType cannonType;
    protected ShootType shootType;

    [Header("References")]
    [SerializeField] private Transform gunZone = null;
    [SerializeField] private GameObject interactCrosshair = null;
    [SerializeField] private GameObject weaponCrosshair = null;

    [Header("Properties")]
    [SerializeField] protected string weaponName;
    [SerializeField] protected float damage;
    [SerializeField] public float reloadTime;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float recoil;
    [SerializeField] protected float bulletDistance;

    [Header("Max's")]
    [SerializeField] public int maxAmmo;
    [SerializeField] public int maxCartridge;

    [Header("Current's")]
    [SerializeField] private int currentCartridge;
    protected int CurrentCartridge
    {
        get { return currentCartridge; }
        set
        {
            currentCartridge = value;
            if (currentCartridge > maxCartridge)
                currentCartridge = maxCartridge;
        }
    }

    [SerializeField] private int currentAmmo;
    protected int CurrentAmmo
    {
        get { return currentAmmo; }
        set
        {
            currentAmmo = value;
            if (currentAmmo > maxAmmo)
                currentAmmo = maxAmmo;
        }
    }


    [Header("States")]
    [SerializeField] public bool isReloading;
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

            interactCrosshair.SetActive(false);
            weaponCrosshair.SetActive(true);
            // other.gameObject.AddComponent<>();
        }
    }

    protected virtual bool FireRate()
    {
        if (this.fireRateTimer > this.fireRate)
            return true;
        else
            return false;
    }

    public virtual void Shoot(Camera viewport, LayerMask layerToHit)
    {
        if (this.CanShoot)
        {
            this.fireRateTimer = 0.0f;
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

    // TODO:
    public virtual void Aim(Camera cam) 
    {   
        
    }

    public virtual void Reload()
    {
        if (this.CurrentAmmo > 0)
        {
            if (isReloading == false && (this.CurrentCartridge < this.maxCartridge))
            {
                Invoke("Reload", this.reloadTime);
                this.isReloading = true;
            }
        }
        else
            return;

        if (this.reloadTimer > this.reloadTime)
        {
            //? Debug
            Debug.Log("Reloading");

            int need = this.maxCartridge - this.CurrentCartridge;
            if (need > this.CurrentAmmo)
            {
                this.CurrentCartridge += this.CurrentAmmo;
                this.CurrentAmmo = 0;
            }
            else
            {
                this.CurrentAmmo -= this.maxCartridge - this.CurrentCartridge;
                this.CurrentCartridge = need + this.CurrentCartridge;
            }

            this.reloadTimer = 0.0f;
            this.isReloading = false;
        }
    }
}