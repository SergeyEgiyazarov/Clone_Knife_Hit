using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLog : MonoBehaviour
{
    [SerializeField] private Vector2 forceDiraction;
    private Rigidbody2D rigidbody2;
    private float forceStrong = 10;

    private void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rigidbody2.AddForce(forceDiraction * forceStrong, ForceMode2D.Impulse);
        Destroy(gameObject, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, 60f * Time.deltaTime);
    }
}
