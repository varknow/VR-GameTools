using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Authorization : MonoBehaviour
{
    public InputField UsernameField;
    public InputField PasswordField;
    public InputField EmailField;
    public UnityEvent AuthorizationEvent;
    public UnityEvent PlayerExistsEvent;
    public UnityEvent IncorrectEvent;
    public UnityEvent RequiredFields;

    public void SignUpButton()
    {
        if(UsernameField.text != "" & PasswordField.text != "" & EmailField.text != "")
        {
            SignUp(UsernameField.text, PasswordField.text, EmailField.text);
        } else
        {
            RequiredFields.Invoke();
        }
    }

    public void SignInButton()
    {
        if (UsernameField.text != "" & PasswordField.text != "")
        {
            SignIn(UsernameField.text, PasswordField.text);
        }
        else
        {
            RequiredFields.Invoke();
        }
    }

    private void SignUp(string username,string password, string email)
    {
        PlayerData data = GetPlayerData(username);
        if (data == null)
        {
            SaveNewPlayer(username,password,email);
            AuthorizationEvent.Invoke();
        }
        else
        {
            PlayerExistsEvent.Invoke();
        }  
    }

    public void SignIn(string username, string password)
    {
        PlayerData data = GetPlayerData(username);
        if (data != null)
        {
            if (SaveSystem.LoadPlayer(username).Password == password)
            {
                PlayerManager.instance.SetCurrentCredentials(data.Username,data.Password,data.Email);
                AuthorizationEvent.Invoke();
            }
            else
            {
                IncorrectEvent.Invoke();
            }
        }
        else
        {
            IncorrectEvent.Invoke();
        }
    }

    public PlayerData GetPlayerData(string username)
    {
        PlayerData data = SaveSystem.LoadPlayer(username) ?? null;
        return data;
    }


    private void SaveNewPlayer(string username,string password, string email)
    {
        PlayerManager.instance.SetCurrentCredentials(username,password,email);
        PlayerManager.instance.SaveCurrentPlayer();
    }

}
