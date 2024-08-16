using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TriggerPlayAudio : MonoBehaviour
{

    //Decalaration
    private GameManager _gameManager;    
    [SerializeField] private string AudioFileName;
    private bool _mediaplayed, _canplay;

    // to be set in the inscpector if the audio is to played once or always play on trigger
    public bool _playonce;
    

   
    

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _mediaplayed = false;
        
        _canplay = true;
        
        
    }
   


    public string GetAudioFileName() {  return AudioFileName; }
    public bool GetMediaFinished() { return _mediaplayed;}
    public void SetCanPLay(bool value) { _canplay = value; }
    

    //If PLayer enters the trigger play the audio

    private void OnTriggerEnter(Collider other)
    {
        if (_canplay)
        {
            if (_playonce) { PlayAudioOnce(); }
            else if (!_playonce) { PlayAudio(); }

        }
       
        
    }

    
    //THis Method plays the audio everytime the player enters the triiger
    public void PlayAudio() { _gameManager.GetMediaManager().PlayAudioOnce(_gameManager.GetPlayer().gameObject, AudioFileName); }
 
    //This methods only plays the audio
    public void PlayAudioOnce() 
    {
        if (!_mediaplayed)
        {
            _gameManager.GetMediaManager().PlayAudioOnce(_gameManager.GetPlayer().gameObject, AudioFileName);
            _mediaplayed = true;
        }
    }
    



}
