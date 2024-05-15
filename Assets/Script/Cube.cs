using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody), typeof(CubeColorChanger))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeColorChanger _colorChanger;

    private const int MinDelay = 2;
    private const int MaxDelay = 5;
    private bool _hasCollided = true;

    public event Action<Cube> OnReturnedToPool;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out CubeSpawner cubeSpawner) && _hasCollided)
        {
            _hasCollided = false;
            _colorChanger.ChangeColor();
            float delay = UnityEngine.Random.Range(MinDelay, MaxDelay);
            StartCoroutine(ReturnCubeWithDelay(this, delay));
        }
    }

    private IEnumerator ReturnCubeWithDelay(Cube cube, float delay)
    {
        yield return new WaitForSeconds(delay);
        OnReturnedToPool?.Invoke(this);
        _hasCollided = true;
        _colorChanger.ReturnColor();
    }  
}