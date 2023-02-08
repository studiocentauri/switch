using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField] Transform[] entryContainer;
    [SerializeField] Transform entryTemplate;
    private List<List<HighscoreEntry>> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    private int level;
    [SerializeField] Image[] levelImages;
    [SerializeField] Color[] colors;
    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);

        Highscores highscores = new Highscores();
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        highscoreEntryList = new List<List<HighscoreEntry>>()
        {
            new List<HighscoreEntry>(),
            new List<HighscoreEntry>(),
            new List<HighscoreEntry>()
        };

        level = 0;
        AddHighscoreEntry(3414, "Acha");
        AddHighscoreEntry(3414, "Acha");
        AddHighscoreEntry(3414, "Acha");
        level = 1;
        AddHighscoreEntry(3414, "eoydwq");
        AddHighscoreEntry(3414, "ryjrh");
        AddHighscoreEntry(3414, "etq");
        AddHighscoreEntry(3414, "qtte");
        level = 2;
        AddHighscoreEntry(3414, "qeoir");
        AddHighscoreEntry(3414, "awdaw");
        AddHighscoreEntry(3414, "rwehh");
        level = 0;

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // sorting the list
        for (int i=0;i<highscores.highscoreEntryList.Count;i++)
        {
            for(int j=i+1;j< highscores.highscoreEntryList.Count;j++)
            {
                if(highscores.highscoreEntryList[i].score> highscores.highscoreEntryList[j].score)
                {
                    HighscoreEntry temp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = temp;
                }
            }
        }

        for(int i=0;i< highscoreEntryList.Count;i++)
        {
            highscoreEntryTransformList = new List<Transform>();
            foreach (HighscoreEntry highScoreEntry in highscoreEntryList[i])
            {
                CreateHighscoreEntryTransform(highScoreEntry, entryContainer[i], highscoreEntryTransformList);
            }
        }
    }
    private void CreateHighscoreEntryTransform(HighscoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        entryTransform.Find("Image").Find("Rank").GetComponent<TMP_Text>().text = rankString;
        entryTransform.Find("Image").Find("Name").GetComponent<TMP_Text>().text = highScoreEntry.name;
        entryTransform.Find("Image").Find("Score").GetComponent<TMP_Text>().text = highScoreEntry.score.ToString();
        transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score, string name)
    {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
        highscoreEntryList[level].Add(highscoreEntry);
        // Load saved Highscores 
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        // Add new entry to highscore
        highscores.highscoreEntryList.Add(highscoreEntry);
        // Save Updated Highscore
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
    public void SetLevel(int val)
    {
        entryContainer[level].gameObject.SetActive(false);
        levelImages[level].color = colors[1];
        level = val - 1;
        entryContainer[level].gameObject.SetActive(true);
        levelImages[level].color = colors[0];
    }
}
