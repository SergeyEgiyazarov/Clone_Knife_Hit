using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    private BoxCollider2D appleCollider;
    private SpriteRenderer appleRenderer;
    

    private void Start()
    {
        appleCollider = GetComponent<BoxCollider2D>();
        appleRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            appleRenderer.enabled = false;
            appleCollider.enabled = false;
            GameManager.Instance.AppleUppdate();

            particle.Play();
            particle.gameObject.transform.SetParent(null);
            Destroy(gameObject, 1.5f);
        }
    }
}
