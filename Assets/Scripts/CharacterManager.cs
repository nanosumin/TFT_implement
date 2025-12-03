using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class CharacterManager : MonoBehaviour
{
    public string jsonFilePath = "Data/character.json";
    public Transform spawnParent;
    public GameObject[] characterPrefabs;

    private CharacterStats[] charactersData;

    void Awake()
    {
        LoadCharacterData();
        SpawnCharacters();
    }

    void LoadCharacterData()
    {
        string path = Path.Combine(Application.dataPath, jsonFilePath);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            charactersData = JsonConvert.DeserializeObject<CharacterStats[]>(json);
            Debug.Log("Loaded " + charactersData.Length + " characters.");
        }
        else
        {
            Debug.LogError("JSON file not found at " + path);
        }
    }

    void SpawnCharacters()
    {
        foreach (var data in charactersData)
        {
            GameObject prefab = FindPrefabByName(data.characterName);
            if(prefab != null)
            {
                GameObject obj = Instantiate(prefab, spawnParent);
                CharacterStats stats = obj.GetComponent<CharacterStats>();
                stats.id = data.id;
                stats.characterName = data.characterName;
                stats.hp = data.hp;
                stats.attack = data.attack;
                stats.cost = data.cost;
                stats.ability = data.ability;
            }
            else
            {
                Debug.LogWarning("Prefab not found for " + data.characterName);
            }
        }
    }

    GameObject FindPrefabByName(string name)
    {
        foreach (var prefab in characterPrefabs)
        {
            if (prefab.name == name)
                return prefab;
        }
        return null;
    }
}
