using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody), typeof(CubeColorChanger))]
public class Cube : MonoBehaviour
{
    private const int MinDelay = 2;
    private const int MaxDelay = 5;

    private CubeColorChanger _colorChanger;
    private bool _hasNotCollided = true;

    public event Action<Cube> ReturnedToPool;

    private void Awake()
    {
        _colorChanger = GetComponent<CubeColorChanger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasNotCollided && collision.collider.TryGetComponent(out Platform platform))
        {
            _hasNotCollided = false;
            _colorChanger.ChangeColor();
            float delay = UnityEngine.Random.Range(MinDelay, MaxDelay);
            StartCoroutine(ReturnCubeWithDelay(delay));
        }
    }

    private IEnumerator ReturnCubeWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnedToPool?.Invoke(this);
        _hasNotCollided = true;
        _colorChanger.ReturnColor();
    }
}
