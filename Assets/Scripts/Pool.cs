using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {

    private const int ITEMS_FOR_SPAWN = 10;
    private const int ADDITIONAL_ITEMS = 5;

    private const string PREFAB_FOLDER = "Figures/Prefabs/";
    private Figure[] figuresPrefab;
    private List<Figure> [] pool;

    [SerializeField]
    private ParticleController particleObj;

    private List<ParticleController> particles = new List<ParticleController>();

	
	void Start () {
		
	}
	
	
	void Update () {
		
	}

    public void InitPool(Dictionary<int, XmlFigure> dic)
    {
        int length = dic.Count;
        figuresPrefab = new Figure[length];
        pool = new List<Figure>[length];

        for (int i = 0; i < length; i++)
        {
            string path = PREFAB_FOLDER + dic[i].prefab;

            GameObject go = Resources.Load(path) as GameObject;
            figuresPrefab[i] = go.GetComponent<Figure>();

            List<Figure> fList = new List<Figure>();

            for (int j = 0; j < ITEMS_FOR_SPAWN; j++)
            {
                Figure temp = Instantiate(figuresPrefab[i], transform.position, Quaternion.identity, transform);
                temp.InitComponent(i);
                temp.gameObject.SetActive(false);
                fList.Add(temp);
            }

            pool[i] = fList;
        }

        for (int i = 0; i < ITEMS_FOR_SPAWN; i++)
        {
            ParticleController temp = Instantiate(particleObj, transform.position, Quaternion.identity, transform);
            temp.InitParticles();
            temp.gameObject.SetActive(false);
            particles.Add(temp);
        }

    }

    public Figure GetFigure(int index)
    {
        int count = pool[index].Count;

        if(count == 0)
        {
            SpawnAdditionalFigures(index);
            count = ADDITIONAL_ITEMS;
        }

        int selectedIndex = count - 1;

        Figure f = pool[index][selectedIndex];
        pool[index].RemoveAt(selectedIndex);

        f.gameObject.SetActive(true);
        f.transform.parent = null;
        return f;
    }

    public void AddFigure(int index, Figure f)
    {
        f.transform.parent = transform;
        f.transform.position = Vector3.zero;
        f.gameObject.SetActive(false);
        pool[index].Add(f);
    }

    private void SpawnAdditionalFigures(int index)
    {
        for (int i = 0; i < ADDITIONAL_ITEMS; i++)
        {
            Figure temp = Instantiate(figuresPrefab[index], transform.position, Quaternion.identity, transform);
            temp.InitComponent(index);
            temp.gameObject.SetActive(false);
            pool[index].Add(temp);
        }
    }


    public ParticleController GetParticles()
    {
        int count = particles.Count;

        if (count == 0)
        {
            SpawnAdditionalParticles();
            count = ADDITIONAL_ITEMS;
        }

        int selectedIndex = count - 1;

        ParticleController p = particles[selectedIndex];
        particles.RemoveAt(selectedIndex);

        p.gameObject.SetActive(true);
        p.transform.parent = null;
        p.StartCounting();
        return p;
    }

    public void AddParticles(ParticleController c)
    {
        c.transform.position = transform.position;
        c.transform.parent = transform;
        c.gameObject.SetActive(false);
        particles.Add(c);
    }


    private void SpawnAdditionalParticles()
    {
        for (int i = 0; i < ADDITIONAL_ITEMS; i++)
        {
            ParticleController temp = Instantiate(particleObj, transform.position, Quaternion.identity, transform);
            temp.InitParticles();
            temp.gameObject.SetActive(false);
            particles.Add(temp);
        }
    }
}
