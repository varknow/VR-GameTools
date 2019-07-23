using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData {

    public string Username;
	public string Password; //TODO: Encryption
    public string Email;
	
	public PlayerData (){

        string[] credentials = new string[3];
        credentials = PlayerManager.instance.GetCurrentCredentials();
        this.Username = credentials[0];
        this.Password = credentials[1];
        this.Email = credentials[2];
	} 
}
