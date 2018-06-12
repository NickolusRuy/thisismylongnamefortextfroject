using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLAdapter : MonoBehaviour {

    private const string SOURCE_FILE = "Xml/figures";

    private const string PREFAB_TAG = "prefab";
    private const string IMG_TAG = "img";
    private const string DESCR_TAG = "descr";
    private const string POINTS_TAG = "points";


    void Start () {
        ParseXML();
    }
	
    private void ParseXML()
    {
        TextAsset ta = (TextAsset)Resources.Load(SOURCE_FILE);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(ta.text);

        XmlNode root = xmlDoc.DocumentElement;
        XmlNodeList nodes = root.SelectNodes("figure");

        int key = 0;

        Dictionary<int, XmlFigure> readedFigures = new Dictionary<int, XmlFigure>();

        foreach (XmlNode node in nodes)
        {
            XmlFigure temp;
            temp.prefab = node[PREFAB_TAG].InnerText;
            temp.img    = node[IMG_TAG].InnerText;
            temp.descr  = node[DESCR_TAG].InnerText;
            temp.points = int.Parse(node[POINTS_TAG].InnerText);

            readedFigures.Add(key, temp);
            key++;
        }

        GameManager.Instance.InitAll(readedFigures);


    }
}

public struct XmlFigure
{
    public string prefab;
    public string img;
    public string descr;
    public int points;

    public bool CheckData()
    {
        return (prefab == string.Empty ||
                   img == string.Empty ||
                 descr == string.Empty ||
                points == 0);    
    }
}
