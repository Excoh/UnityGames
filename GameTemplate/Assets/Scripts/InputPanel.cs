using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPanel : MonoBehaviour
{
    public GameObject playerControlPrefab;
    public GameObject controlPrefab;

    void Start()
    {
        Load();
    }

    void Update()
    {
       
    }

    public void Load()
    {
        for(int i = 0; i < 1; i++)
        {
            GameObject playerControl = Instantiate(playerControlPrefab, transform);
            InputManager.InputMethod type = InputManager.InputMethod.Keyboard;
            switch(type)
            //switch (InputManager.Players[i].GetComponent<InputPlayer>().InputMethod)
            {
                case InputManager.InputMethod.Keyboard:
                    foreach(var dictionary in InputManager.GetKeyboardDictionary(0))
                    {
                        LoadDictionary(playerControl, dictionary);
                    }
                    break;

                case InputManager.InputMethod.XboxController:
                    LoadDictionary(playerControl, InputManager.GetXboxAxisDictionary(0));
                    LoadDictionary(playerControl, InputManager.GetXboxButtonDictionary(0));
                    break;

                default :
                    break;
            }

        }
    }

    private void LoadDictionary<T>(GameObject player, Dictionary<string,T> dictionary)
    {
        foreach(var pair in dictionary)
        {
            GameObject control = Instantiate(controlPrefab, player.transform);
            var text = control.GetComponentsInChildren<Text>();
            text[0].text = pair.Key.ToString();
            text[1].text = pair.Value.ToString();
        }
    }
}
