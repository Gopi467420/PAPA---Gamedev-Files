using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerShowInstruction : MonoBehaviour
{

    //Declaration
    private GameManager _gameManager;
    private bool _instructionshown, _canshow;
    [SerializeField] protected bool _Showonce;
    [SerializeField] protected Image _imagetoshow;
    
   
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _instructionshown = false;        
        _canshow = true;
       
        
    }

    // Update is called once per frame
    void Update()
    {

        if( _instructionshown && Input.GetKeyDown(KeyCode.F))
        {
            Continue();
        }
        
    }
    public bool GetInstructionShown() { return _instructionshown; }
    public void SetCanShow(bool value) { _canshow = value; }

    private void Continue()
    {
        _gameManager.GetLevelManager().HideImage(_imagetoshow);
        _gameManager.GetLevelManager().HideInstructions(_gameManager.GetLevelManager().GetSkipTMPComponent());
        _gameManager.ResumeGame();
        


    }

   
    //If player enters trigger then show instruction
    private void OnTriggerEnter(Collider other)
    {

        if (other == _gameManager.GetPlayer().GetCollider())
        {
            if (_canshow)
            {
                if (_Showonce)
                {
                    if (!_instructionshown)
                    {
                        _gameManager.GetLevelManager().ShowImage(_imagetoshow);
                        _gameManager.GetLevelManager().ShowInstructions(_gameManager.GetLevelManager().GetSkipTMPComponent());
                        _gameManager.SetGamePaused();
                        _instructionshown = true;
                    }
                }
                else
                {
                    _gameManager.GetLevelManager().ShowImage(_imagetoshow);
                    _gameManager.GetLevelManager().ShowInstructions(_gameManager.GetLevelManager().GetSkipTMPComponent());
                    _gameManager.SetGamePaused();
                    _instructionshown = true;
                }

            }
            

        }
            
            

            
          
        

    }

    private void OnTriggerStay(Collider other)
    {

        if (_gameManager.GetPoissonDrawer().Getinteracted())
        {

            if (other == _gameManager.GetPlayer().GetCollider())
            {
                if (_canshow)
                {
                    if (_Showonce)
                    {
                        if (!_instructionshown)
                        {
                            _gameManager.GetLevelManager().ShowImage(_imagetoshow);
                            _gameManager.GetLevelManager().ShowInstructions(_gameManager.GetLevelManager().GetSkipTMPComponent());
                            _gameManager.SetGamePaused();
                            _instructionshown = true;
                        }
                    }
                    else
                    {
                        _gameManager.GetLevelManager().ShowImage(_imagetoshow);
                        _gameManager.GetLevelManager().ShowInstructions(_gameManager.GetLevelManager().GetSkipTMPComponent());
                        _gameManager.SetGamePaused();
                        _instructionshown = true;
                    }

                }


            }

        }
        
    }





}
