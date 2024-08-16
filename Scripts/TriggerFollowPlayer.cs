using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFollowPlayer : MonoBehaviour
{

    //This script checks if the player is inside the trigger bounds and set ghost follow player true  else followplayer is false


   //Deaclaration
    private GameManager _gamemanager;


    // Start is called before the first frame update
    void Start()
    {

        _gamemanager = FindObjectOfType<GameManager>();
    }



    
    //When player enter in the trigger ghost will start folowing the player
    private void OnTriggerEnter(Collider other)
    {
        if (other == _gamemanager.GetPlayer().GetCollider())
        {

            _gamemanager.GetGhost().SetFollowPlayer(true);


        }
    }

    //    //When player enter in the trigger ghost will sstop folowing the player

    private void OnTriggerExit(Collider other)
    {
        if (other == _gamemanager.GetPlayer().GetCollider())
        {

            _gamemanager.GetGhost().SetFollowPlayer(false);


        }

    }
}
