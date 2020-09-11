using UnityEngine;

public class UseWeapon : MonoBehaviour
{
    [SerializeField] private InputManager input;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Weapon anyWeapon;

    private void Start()
    {
        input = GetComponent<InputManager>();
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    private void Update() 
    {
        anyWeapon = GetComponent<Weapon>();
        PickUpWeapon(anyWeapon);
    }

    private void PickUpWeapon(Weapon weapon)
    {
        if (weapon != null)
        {
            if (input.fire)
                weapon.Shoot(cam, enemyLayer);
            if (input.reload)
                weapon.Aim(cam);
            if (input.reload)    
                weapon.Reload();
        }
    }
}