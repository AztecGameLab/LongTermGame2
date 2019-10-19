using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{

    //We need to keep track of our total score (num minigames completed sucessfully) (int)
    //We need to keep track of our lives (int)
    public int lives;
    public int completedMiniGames;

    //We need a static singleton
    private static MiniGameManager _instance;
    public static MiniGameManager instance{
        get{
            if(_instance == null){
                _instance = GameObject.FindObjectOfType<MiniGameManager>();

                if(_instance == null){
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<MiniGameManager>();
                }
            }
            return _instance;
        }
    }

    public void Awake(){
        //We want this to stay within the scene so that the minigames can call it
        GameObject.DontDestroyOnLoad(this.gameObject);     
    }

    public void GameComplete(bool win){
        if(win){
            completedMiniGames++;
        } else {
            lives--;
        }
    }

    

}
