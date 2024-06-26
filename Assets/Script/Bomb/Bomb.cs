using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 1000f;
    [SerializeField] private float _fadeDuration = 3f;

    private Renderer _renderer;
    private Material _material;
    public event Action<Bomb> Exploded;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
    }

    public IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (true)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / _fadeDuration);

            Color color = _material.color;
            color.a = alpha;
            _material.color = color;

            if (color.a <= 0)
            {
                Explode();
                Exploded?.Invoke(this);
                yield break;
            }

            yield return null;
        }
    }




    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rigidbody = nearbyObject.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }

    public void ReloadBomb()
    {
        Color color = _material.color;
        color.a = 1;
        _material.color = color;
    }
}
