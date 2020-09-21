using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour {
    private new Rigidbody rigidbody;
    public Rigidbody rb => rigidbody;
    public static float efectiveRadius = 2f;
    public static bool inUse;

    private void Start() => rigidbody = GetComponent<Rigidbody>();
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, efectiveRadius);
    }
}
