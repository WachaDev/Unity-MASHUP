using System;
using UnityEngine;
using Enemies;

namespace Weapons
{
    public class Weapon
    {
        protected enum CannonType { Large, Short, Duplex }

        protected enum ShootType { Auto, Semi, Burst }
        protected Enum cannonType;
        protected Enum shootType;

        protected float fireRate;
        protected string name;
        protected float recoil;
        protected float bulletDistance;

        public float damage;
        public int maxAmmo;
        public int currentAmmo
        {
            get
            {
                if (currentAmmo >= maxAmmo)
                    return currentAmmo = maxAmmo;
                else
                    return currentAmmo;
            }
            set { }
        }

        protected bool CanShoot => input.fire && this.currentAmmo > 0 && FireRate();

        protected LayerMask enemyLayer = LayerMask.GetMask("Enemy");
        protected InputManager input;

        private bool FireRate()
        {
            float delay = 0.0f;
            delay += Time.deltaTime;

            if(delay > this.fireRate)
                return true;
            else
                return false;
        }

        public virtual void Shoot(Camera viewport)
        {
            if (CanShoot)
            {
                Ray bullet = viewport.ViewportPointToRay(Vector3.one * 0.5f);
                RaycastHit hit;
                if (Physics.Raycast(bullet.origin, bullet.direction, out hit, this.bulletDistance, enemyLayer))
                {
                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(this.damage);
                    }
                }
            }
        }
    }

    public class _9MM : Weapon
    {
        private _9MM()
        {
            this.name = "9MM";
            this.recoil = 0.5f;
            this.damage = 1.0f;
            this.fireRate = 0.5f;
            this.maxAmmo = 60;
            this.currentAmmo = this.maxAmmo;
            this.bulletDistance = 100.0f;
            this.cannonType = CannonType.Short;
            this.shootType = ShootType.Semi;
        }
        public override void Shoot(Camera viewport)
        {
            // ADD MODIFIERS OR DETAILS AS YOU WANT
            base.Shoot(viewport);
        }
    }
}