using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "qualitea";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load(string profileID)
    {
        //use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                //Load the seralized data from the file
                string dataToLoad = "";
                
                using (FileStream stream = new FileStream(fullPath,FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //optionally decrypt the data
                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }
                
                //deseralize the data from Json back into C# obkect
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occursed when trying to load data to file: " + fullPath + "\n" + e);
            }

        }
        return loadedData;
    }
    
    public void Delete(GameData data, string profileID)
    {
        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            Debug.Log($"save slot {profileID} deleted ");
        }
        else Debug.LogWarning($"Save slot {profileID} does not exist.");

    }

    public void Save(GameData data, string profileID)
    {
        //use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
        try
        {
            //create directory path if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize C# game data into Json
            string dataToStore = JsonUtility.ToJson(data, true);

            //optionally encrypt the data
            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            //write the serialized data to thef ile
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occursed when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    public Dictionary <string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        //Loop over all dictionary names in the data directory path
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileID = dirInfo.Name;

            //check if the data file exists
            //if it doesn't, then this folder isn't a profile and should be skipped.
            string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data: "
                    + profileID);
                continue;
            }

            //Load the game data for this profule and put it in the dictionary
            GameData profileData = Load(profileID);

            //ensure profule data isn't null
            if (profileData != null) profileDictionary.Add(profileID, profileData);
            else Debug.LogError("Tried to load profile but something went wrong. ProfileID: " + profileID);
        }

        return profileDictionary;
    }

    //simple implementation of XOR encryption
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
