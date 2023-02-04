using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderborad : MonoBehaviour
{
     private Transform entryContainer;
     private Transform entryTemplate;
     private List<HighscoreEntry> highscoreEntryList;
     private List<Transform> highscoreEntryTransformList;


     private void Awake()
    {
     entryContainer = transform.Find("highscoreEntryContainer");
     entryContainer = transform.Find("highscoreEntryTemplate");

     entryTemplate.gameObject.SetActive(false);
    //  highscoreEntryList = new List<HighscoreEntry>(){
    //     new HighscoreEntry {score = 12462, name = "ABC"},
    //     new HighscoreEntry {score = 10462, name = "AC"},
    //     new HighscoreEntry {score = 7462, name = "lC"},
    //     new HighscoreEntry {score = 5462, name = "JC"},
    //     new HighscoreEntry {score = 4462, name = "AB"},
    //     new HighscoreEntry {score = 3462, name = "BC"},
    //     new HighscoreEntry {score = 2462, name = "JABC"},
    //     new HighscoreEntry {score = 250, name = "KABC"},
    //     new HighscoreEntry {score = 190, name = "AnBC"},
    //     new HighscoreEntry {score = 124, name = "AklBC"},
    //     };
    

     string jsonString = PlayerPrefs.GetString("highscoreTable");
       Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
       
       
            //sorting
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
    {
        for (int j = i + 1; i < highscores.highscoreEntryList.Count; j++)
        {
            if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
            {
                HighscoreEntry tmp=highscores.highscoreEntryList[i];
               highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                highscores.highscoreEntryList[j] = tmp;
            }
        }
    }
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry,entryContainer,highscoreEntryTransformList);
        }
    }    

private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry,Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;

        Transform entryTransform = Instantiate(entryTemplate,container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0,-templateHeight*transformList.Count);
        entryTransform.gameObject.SetActive(true); 

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            
            default:
                   rankString = rank +"TH";break;
        case 1: rankString = "1st";break;
        case 2: rankString = "2nd";break;
        case 3: rankString = "3rd";break;

        }
        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = highscoreEntry.score;
       
       entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();
       string name =highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name ;
    
        transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry (int score,string name){
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry{score = score , name=name};
       //Load saved Highscores 
       string jsonString = PlayerPrefs.GetString("highscoreTable");
       Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
       //Add new enry to highscore
       highscores.highscoreEntryList.Add(highscoreEntry);
       //Save Updated Highscore
           string json =JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable",json);
        PlayerPrefs.Save();
    }

    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    // Represents single High score entry
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
    
}