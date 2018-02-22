using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaserGameConfigJson {
	
	public List<LaserGameProperties> properties;
	public List<LaserLevel> levels;

}

[System.Serializable]
public class LaserGameProperties{

	public int maxBounces; 
	public int defaultMode;
	public int[] sandboxModeBoardSize;
}


[System.Serializable]
public class LaserLevel  {

	public int level;
	public int[] boardSize; 
	public List<LaserGameBoardItems> gameBoardItems; 
	public List<LaserAvailableItems> availableItems;
}

[System.Serializable]
public class LaserGameBoardItems{

	public string type;
	public List<LaserGameBoardItemParameters> parameters; 

}

public class LaserGameBoardItemParameters{
	public int[] rgb;
	public int[] location;
	public string direction;
}

[System.Serializable]
public class LaserAvailableItems{

	public string type; 
	public int quantity; 

}


[System.Serializable]
public class Player
{
	public string playerId;
	public string playerLoc;
	public string playerNick;
	public List<LaserAvailableItems> ss;
}


// Using JsonHelper to serialize/deserialize the game and level configurations
// Code found from the following website: https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

	[System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}