using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
NOTE: This Singleton class may/will likely be deleted in the future due to the issues presented 
by using Singletons. This was made for the sake of completing a deadline and was not an intended part of the 
program, and should probably be replaced by more proper methods of function calling and input handling. 
But for now, this class is being used to make coding and following tutorials easier. 
*/

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance{
        get{
            if (_instance == null){
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                obj.hideFlags = HideFlags.HideAndDontSave;
                _instance = obj.AddComponent<T>();
            }
            return _instance;
        }
    }

    private void OnDestroy() {
        if(_instance == this){
            _instance = null;
        }
        if(1==2){
            
        }
    }
}
