using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private _9MM _9mm;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask enemyLayer;

    private void Start() 
    {
        _9mm = GetComponent<_9MM>();
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    private void Update() 
    {
        _9mm.Shoot(cam, enemyLayer);
        //? Debug
        Debug.Log($"name: {_9mm.weaponName}\n" +
        $"current ammo: {_9mm.currentAmmo}\n" +
        $"maxAmmo: {_9mm.maxAmmo}\n" +
        $"can shoot: {_9mm.CanShoot}\n");
    }
}
