using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

// fsfullserializer: https://github.com/jacobdufault/fullserializer
using FullSerializer;

public class LaserGameConfig: MonoBehaviour{

	//public LaserCanvasManager canvasManager = new LaserCanvasManager();
	public LaserGameConfigJson Config = new LaserGameConfigJson();
	public LaserGameConfigJson ConfigToSave = new LaserGameConfigJson();
	private fsSerializer serializer = new fsSerializer();

	public void SaveGameConfigSetup(){
		ConfigToSave.properties = new List<LaserGameProperties>();
		ConfigToSave.levels = new List<LaserLevel>();

		LaserGameProperties propertyToSave = new LaserGameProperties();
		propertyToSave.maxBounces = 222;
		propertyToSave.defaultMode = 0;
		propertyToSave.sandboxModeBoardSize = new int[2] { 5, 9 };

		ConfigToSave.properties.Add(propertyToSave);
	}

	public void SaveGameConfiguration(LaserLevel newLevel)
	{
		
		ConfigToSave.levels.Add(newLevel);

		string playerToJason = JsonUtility.ToJson(ConfigToSave, true);
		Debug.Log(playerToJason);

		string jsonSaveFilePath = Application.dataPath + "/Laser/Scripts/LaserGameConfigsSavedJSONv2.json";
		System.IO.File.WriteAllText(jsonSaveFilePath, playerToJason);
	
	}
		

	public void LoadGameConfiguration(string path)
	{

		SaveGameConfigSetup();

		Debug.Log(path + "/Laser/Scripts/LaserGameConfigsJSON.json");
		var gameConfig = LoadJsonFile<LaserGameConfigJson>(path + "/Laser/Scripts/LaserGameConfigsJSON.json");
		if (gameConfig != null){
			Debug.Log("GAME CONFIG LOADED!");
			Config = gameConfig;
		}

	}


	private T LoadJsonFile<T> (string path) where T: class
	{
		if (File.Exists(path)) 
		{
			var file = new StreamReader(path);
			var fileContents = file.ReadToEnd();
			var data = fsJsonParser.Parse(fileContents);
			object deserialized = null;
			serializer.TryDeserialize(data, typeof(T), ref deserialized).AssertSuccessWithoutWarnings();
			file.Close();
			return deserialized as T;
		}
		return null;
	}

	private T LoadJSONString<T>(string json) where T : class
	{
		var data = fsJsonParser.Parse(json);
		object deserialized = null;
		serializer.TryDeserialize(data, typeof(T), ref deserialized).AssertSuccessWithoutWarnings();
		return deserialized as T;
	}

}
