using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AmconNS;

public class SpawnManager : MonoBehaviour, ISubManager
{
    private Vector3 spawnPoint = new Vector3(0, 1, 0);

    [SerializeField]
    private Pool pool;

    public void Init(Dictionary<int, XmlFigure> _figuresData)
    {
        //init pool
        pool.InitPool(_figuresData);
    }

    public void SpawnRequest(int index)
    {
        Figure f = pool.GetFigure(index);
        f.transform.position = spawnPoint;
    }

    public ParticleController SpawnParticles()
    {
        return pool.GetParticles();
    }

    public void AddParticlesToPool(ParticleController c)
    {
        pool.AddParticles(c);
    }

    public void AddFigureToPool(int index, Figure f)
    {
        pool.AddFigure(index, f);
    }

}
