using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour
{
    private const int TIME_TO_HIDE = 3;
    ParticleSystem system;

    public void StartCounting()
    {
        system.Play();
        Invoke("Hide", TIME_TO_HIDE);
    }

    private void Hide()
    {
        GameManager.Instance.HideParticles(this);
    }

    public void InitParticles()
    {
        system = GetComponent<ParticleSystem>();
    }

}
