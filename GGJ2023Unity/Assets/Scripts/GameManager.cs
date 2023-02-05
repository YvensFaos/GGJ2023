using System.Collections.Generic;
using Game;
using Newtonsoft.Json;
using UnityEngine;
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
        FindAndRemoveOldLevelEntry(levelFinished);
        LevelRecords.Add(new LevelRecord()
        {
            levelName = levelFinished.LevelName,
            maxGrowth = growth,
            maxRootPowerCollected = rootPower, 
            unlocked = true
        });

        if (!levelFinished.LastLevel)
        {
            var nextLevel = levelFinished.NextLevel;
            FindAndRemoveOldLevelEntry(nextLevel);
            LevelRecords.Add(new LevelRecord()
            {
                levelName = nextLevel.LevelName,
                maxGrowth =  0,
                maxRootPowerCollected = 0, 
                unlocked = true
            });
        }
        
        StoreLevelsToPlayerPrefs();
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

    private void LoadLevelRecords(string jsonData)
    {
        Debug.Log($"Load data json: {jsonData}");
        levelRecords = JsonConvert.DeserializeObject<List<LevelRecord>>(jsonData);
    }
}
