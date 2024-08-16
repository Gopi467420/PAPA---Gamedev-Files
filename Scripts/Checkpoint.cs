using UnityEngine;


public class Checkpoint : MonoBehaviour
{

    //Declaration
    private GameManager _gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        _gamemanager = FindObjectOfType<GameManager>();
    }


    //If the players enters this trigger then save this gameobject transform as a checkpoint
    private void OnTriggerEnter(Collider other)
    {
        if (other == _gamemanager.GetPlayer().GetCollider())
        {
            _gamemanager.SaveCheckpoint(transform.position, gameObject.tag);
        }

    }


  


    
}
