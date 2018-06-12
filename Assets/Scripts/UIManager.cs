using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using AmconNS;

public class UIManager : MonoBehaviour, ISubManager
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private RectTransform content;

    [SerializeField]
    private GameObject menuItem;

    private Animator anim;

    private const string SCORE_WORD = "Score: ";
    private const string SPRITES_FOLDER = "Sprites/";
    private const string SCORE_STR = "Score";

    private int score = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        score = PlayerPrefs.GetInt(SCORE_STR);
        ShowScore();
    }

    public void Init(Dictionary<int, XmlFigure> _figuresData)
    {
        // init ui
        int length = _figuresData.Count;

        for (int i = 0; i < length; i++)
        {
            GameObject temp = Instantiate(menuItem);
            temp.transform.SetParent(content, false);

            XmlFigure f = _figuresData[i];

            Button btn = temp.transform.GetChild(0).GetComponent<Button>();
            int index = i;
            btn.onClick.AddListener(delegate { OnSpawnObjectClick(index); });
             

            Text txt = temp.transform.GetChild(1).GetComponent<Text>();
            txt.text = f.descr;

            Image img = temp.transform.GetChild(2).GetComponent<Image>();

            string path = SPRITES_FOLDER + f.img;

            Sprite s = Resources.Load<Sprite>(SPRITES_FOLDER + f.img);
            img.sprite = s;

        }
    }

    public void CountPoints(int points)
    {
        score += points;
        PlayerPrefs.SetInt(SCORE_STR, score);
        ShowScore();
    }

    public void ShowAnim()
    {
        anim.Play("Show");
    }

    private void ShowScore()
    {
        scoreText.text = SCORE_WORD + score;
    }

    public void OnSpawnObjectClick(int index)
    {
        GameManager.Instance.SpawnRequest(index);
    }


}
