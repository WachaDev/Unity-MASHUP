using UnityEngine;
using Weapons;

public class Shoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputManager input;
    [SerializeField] private Camera cam;
    [SerializeField] private _9MM _9mm;

    void Start()
    {
        input = GetComponent<InputManager>();
        _9mm = GetComponent<_9MM>();
        GetComponent<Transform>().GetChild(0).GetComponent<Camera>();
    }

    void Update()
    {

    }
}
