using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static System.Object lockObj = new object();
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();

                    if (instance == null)
                    {
                        Debug.LogError("GameManager not init!");
                    }

                }

                return instance;
            }
        }
    }    

    #endregion

    #region Managers
    [SerializeField]
    private SpawnManager spawnManager;

    [SerializeField]
    private UIManager uiManager;
    #endregion

    private AudioSource audioSource;

    private Dictionary<int, XmlFigure> figuresData;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SpawnRequest(int index)
    {
        spawnManager.SpawnRequest(index);
    }

    public void InitAll(Dictionary<int, XmlFigure> _figuresData)
    {
        figuresData = _figuresData;
        spawnManager.Init(figuresData);
        uiManager.Init(figuresData);
    }

    public void CountHit(int index, Figure f)
    {
        if(index % 2 == 0)
        {
            //play anim
            uiManager.ShowAnim(); 
        }
        else
        {
            //play particles
            ParticleController p = spawnManager.SpawnParticles();
            p.transform.position = f.transform.position;
        }
        uiManager.CountPoints(figuresData[index].points);

        spawnManager.AddFigureToPool(index, f);
        //play sound
        audioSource.Play();

        //count points
    }

    public void JustAddFigure(int index, Figure f)
    {
        spawnManager.AddFigureToPool(index, f);
    }

    public void HideParticles(ParticleController c)
    {
        spawnManager.AddParticlesToPool(c);
    }
}

namespace AmconNS
{
    public interface ISubManager
    {
        void Init(Dictionary<int, XmlFigure> _figuresData);
    }
}