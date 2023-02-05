using System;
using System.Collections.Generic;
using Game;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<GameLevel> gameLevels;
    [SerializeField] private List<LevelRecord> levelRecords;

    public List<LevelRecord> LevelRecords => levelRecords;

    protected override void Awake()
    {
        base.Awake();
        
        var jsonData = PlayerPrefs.GetString("Levels");
        if (jsonData is null or "")
        {
            CreateLevelRecords();
        }
        else
        {
            LoadLevelRecords(jsonData);
        }
    }

    public void UnlockLevel(GameLevel levelFinished, int rootPower, int growth)
    {
        CheckAndUpdateLevelEntry(levelFinished, rootPower, growth);

        if (!levelFinished.LastLevel)
        {
            var nextLevel = levelFinished.NextLevel;
           CheckAndUpdateLevelEntry(nextLevel, 0, 0);
        }
        
        StoreLevelsToPlayerPrefs();
    }

    private void CheckAndUpdateLevelEntry(GameLevel level, int rootPower, int growth)
    {
        if (FindLevelEntry(level, out var record))
        {
            if (record.maxRootPowerCollected >= rootPower) return;
            FindAndRemoveOldLevelEntry(level);
        }
        //No record was found
        LevelRecords.Add(new LevelRecord()
        {
            levelName = level.LevelName,
            maxGrowth = growth,
            maxRootPowerCollected = rootPower,
            unlocked = true
        });
    }

    private void StoreLevelsToPlayerPrefs()
    {
        var json = JsonConvert.SerializeObject(LevelRecords);
        Debug.Log($"Save data json: {json}");
        PlayerPrefs.SetString("Levels", json);
    }

    private void FindAndRemoveOldLevelEntry(GameLevel levelFinished)
    {
        var index = LevelRecords.FindIndex(record => record.levelName.Equals(levelFinished.LevelName));
        if (index != -1)
        {
            LevelRecords.RemoveAt(index);
        }
    }

    private bool FindLevelEntry(GameLevel level, out LevelRecord record)
    {
        var index = LevelRecords.FindIndex(record => record.levelName.Equals(level.LevelName));
        try
        {
            record = levelRecords[index];
        }
        catch (Exception exception)
        {
            record = new LevelRecord();
        }
        
        return index != -1;
    }

    public GameLevel GetLevelFromRecord(LevelRecord record)
    {
        return gameLevels.Find(level => level.LevelName.Equals(record.levelName));
    }
    
    private void CreateLevelRecords()
    {
        gameLevels.ForEach(level =>
        {
            LevelRecords.Add(new LevelRecord()
            {
                levelName = level.LevelName,
                maxGrowth =  0,
                maxRootPowerCollected = 0, 
                unlocked = level.FirstLevel
            });
        });
        StoreLevelsToPlayerPrefs();
    }

    public void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
        CreateLevelRecords();
        //Hardcoded load main menu
        SceneManager.LoadScene("MainMenu");
    }

    private void LoadLevelRecords(string jsonData)
    {
        Debug.Log($"Load data json: {jsonData}");
        levelRecords = JsonConvert.DeserializeObject<List<LevelRecord>>(jsonData);
    }
}
