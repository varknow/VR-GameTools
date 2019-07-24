using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem  {


    public static void SaveVoiceClip(AudioClip clip, int number)
    {
        string username = PlayerManager.instance.GetCurrentCredentials()[0];
        string path = Application.persistentDataPath + "/" + username;
        string filename = username + " Clip " + number;
        SavWav.Save(path,filename, clip);
    }

	public static void SavePlayer(string username){

		BinaryFormatter formatter = new BinaryFormatter();

        Directory.CreateDirectory(Application.persistentDataPath + "/" + username);
        string path = Application.persistentDataPath +"/"+ username + "/" + "player.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

		PlayerData data = new PlayerData();
		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static PlayerData LoadPlayer(string username)
    {

		string path = Application.persistentDataPath +"/"+ username +"/"+ "player.txt";
		if (File.Exists(path)){

			BinaryFormatter formatter = new BinaryFormatter();

			FileStream stream = new FileStream(path, FileMode.Open);
			PlayerData data = formatter.Deserialize(stream) as PlayerData;
			stream.Close();

			return data ;
		}
		else
		{
			Debug.LogError(" Save File Not Found In" + path);
			return null;
		}
	}
}
