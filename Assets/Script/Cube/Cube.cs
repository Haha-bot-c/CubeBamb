using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody), typeof(CubeColorChanger))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeColorChanger _colorChanger;

    private const int MinDelay = 2;
    private const int MaxDelay = 5;
    private bool _hasNotCollided = true; 

    public event Action<Cube> OnReturnedToPool; 

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
        OnReturnedToPool?.Invoke(this);
        _hasNotCollided = true;
        _colorChanger.ReturnColor();
    }
}
