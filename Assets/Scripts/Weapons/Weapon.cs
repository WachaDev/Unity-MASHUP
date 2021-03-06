﻿using UnityEngine;

public class Weapon : MonoBehaviour {
    private void Start() {
        this.cannonType.CannonProperties();
        this.shootType.ShootTypeProperties();
    }

    private void Update() => this.fireRateTimer += Time.deltaTime;

    private float fireRateTimer;
    [SerializeField] protected CannonType cannonType;
    [SerializeField] protected ShootType shootType;

    [Header("References")]
    [SerializeField] private Transform gunZone = null;
    [SerializeField] private GameObject interactCrosshair = null;
    [SerializeField] private GameObject weaponCrosshair = null;

    [Header("Properties")]
    [SerializeField] protected string weaponName;
    [SerializeField] protected float damage;
    [SerializeField] public float reloadTime;
    [SerializeField] public float fireRate;
    [SerializeField] protected float recoil;
    [SerializeField] protected float bulletDistance;
    [SerializeField] protected Vector3 aim;

    [Header("Max's")]
    [SerializeField] public int maxAmmo;
    [SerializeField] public int maxCartridge;

    [Header("Current's")]
    [SerializeField] private int currentCartridge;
    public int CurrentCartridge {
        get { return currentCartridge; }
        set {
            currentCartridge = value;
            if (currentCartridge > maxCartridge)
                currentCartridge = maxCartridge;
        }
    }

    [SerializeField] private int currentAmmo;
    protected int CurrentAmmo {
        get { return currentAmmo; }
        set {
            currentAmmo = value;
            if (currentAmmo > maxAmmo)
                currentAmmo = maxAmmo;
        }
    }

    [Header("States")]
    [SerializeField] public bool isReloading;
    [SerializeField] public bool isShooting;
    [SerializeField] public bool CanShoot => FireRate() && this.CurrentCartridge > 0 && !isReloading;
    protected Vector3 alignPosition;

    //* Pick up Weapon
    protected virtual void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            this.gameObject.transform.SetParent(gunZone);
            this.gameObject.transform.position = gunZone.transform.position;
            this.gameObject.transform.localEulerAngles = this.alignPosition;

            this.gameObject.GetComponent<BoxCollider>().enabled = false;

            interactCrosshair.SetActive(false);
            weaponCrosshair.SetActive(true);
        }
    }

    protected virtual bool FireRate() {
        if (this.fireRateTimer >= this.fireRate)
            return true;
        else
            return false;
    }

    public virtual void Shoot(Camera viewport, LayerMask layerToHit) {
        if (this.CanShoot) {
            this.fireRateTimer = 0.0f;
            this.isShooting = true;

            Ray bullet = viewport.ViewportPointToRay(Vector3.one * 0.5f);
            RaycastHit hit;
            this.currentCartridge--;

            if (Physics.Raycast(bullet.origin, bullet.direction, out hit, this.bulletDistance, layerToHit)) {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                    enemy.TakeDamage(this.damage);
            }

        } else
            this.isShooting = false;
    }

    public virtual void Aim(Camera cam) {
        cam.transform.localPosition += this.aim;
    }

    public virtual void Reload() {
        if ((this.CurrentAmmo > 0) && (this.CurrentCartridge < this.maxCartridge)) {
            Invoke("ReloadCalculations", this.reloadTime);
            this.isReloading = true;
        }
    }

    private void ReloadCalculations() {
        Debug.Log("Reloaded");

        int need = this.maxCartridge - this.CurrentCartridge;
        if (need > this.CurrentAmmo) {
            this.CurrentCartridge += this.CurrentAmmo;
            this.CurrentAmmo = 0;
        } else {
            this.CurrentAmmo -= this.maxCartridge - this.CurrentCartridge;
            this.CurrentCartridge = need + this.CurrentCartridge;
        }

        this.isReloading = false;
    }
}

public enum CannonType { Large, Short, Duplex }
public enum ShootType { Auto, Semi, Burst }

public static class CannonAndShootExtension {
    public static void CannonProperties(this CannonType cannon) {
        switch (cannon) {
            case CannonType.Large:
                Debug.Log("Is large cannon type");
                break;
            case CannonType.Short:
                Debug.Log("Is short cannon type");
                break;
            case CannonType.Duplex:
                Debug.Log("Is duplex cannon type");
                break;
            default:
                Debug.Log("I don't know what is this");
                break;
        }
    }

    public static void ShootTypeProperties(this ShootType shootType) {
        switch (shootType) {
            case ShootType.Auto:
                Debug.Log("Is auto shoot type");
                break;
            case ShootType.Semi:
                Debug.Log("Is semi shoot type");
                break;
            case ShootType.Burst:
                Debug.Log("Is burst shoot type");
                break;
            default:
                Debug.Log("I don't know what is this");
                break;
        }
    }
}