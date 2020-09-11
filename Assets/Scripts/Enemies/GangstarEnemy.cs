using UnityEngine;

public class GangstarEnemy : MonoBehaviour
{
   [SerializeField] private Gangstar gangstar;

   private void Start() => gangstar = GetComponent<Gangstar>();
}
