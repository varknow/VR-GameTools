using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    //public GameObject player;
    private string Username;
    private string Password;
    private string Email;

    public void SetCurrentCredentials(string username, string password,string email)
    {
        this.Username = username;
        this.Password = password;
        this.Email = email;
    }


    public string[] GetCurrentCredentials()
    {
        string[] credentials = new string[3];
        credentials[0] = this.Username;
        credentials[1] = this.Password;
        credentials[2] = this.Email;

        return credentials;
    }


    public void SaveCurrentPlayer()
    {
        SaveSystem.SavePlayer(this.Username);
        Debug.Log("Save Successful");
    }


    public void LoadCurrentPLayer()
    {

        PlayerData data = SaveSystem.LoadPlayer(this.Username);

        this.Username = data.Username;
        this.Password = data.Password;
        this.Email = data.Email;

        Debug.Log("Load Successful");
    }
}
