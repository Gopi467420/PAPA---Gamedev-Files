using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    
    //Declaration
    protected GameManager _gamemanager;
    protected Animator Animator;
    protected bool _playerinsidebounds, _interacted ;
     [SerializeField] protected bool _caninteract;
    protected TextMeshPro _interacttext;
    protected bool _cluefound;
    


    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Find the PlayerController 
        _gamemanager = FindObjectOfType<GameManager>();
        Animator = GetComponentInChildren<Animator>();
        _interacttext = GetComponentInChildren<TextMeshPro>();
        _playerinsidebounds = false;
        _interacted = false;
        _cluefound = false;       
        _interacttext.enabled = false;
       

    }

    // Update is called once per frame
    protected virtual void Update()
    {
       //if player is inside the trigger box and press E
       //and the the object can be interactable then the go ahead 
       //and deal interaction 
       //here the _caninteract is true when the condition
       //is met that is set through the code, like reaching
       //a checkpoint       
        if (_playerinsidebounds && Input.GetKeyDown(KeyCode.E) && _caninteract)
        {
            DealInteraction();
            _interacted = true;
                           

        }

    }

    //Accesors

    public virtual bool Getinteracted() { return _interacted; }
    public virtual bool GetClueFound() { return _cluefound; }

    //Mutators
    //Player can't interact
    public virtual void SetCanInteract(bool value) { _caninteract = value ; }
    //The clue is found
    public virtual void SetClueFound(bool value) { _cluefound = value; }
   

    //Perform task , defined in the child class 
    public virtual void DealInteraction() { }
   
    

    //When player enter the trigger show the intereact text and se playerinbounds bool true
    protected virtual void OnTriggerEnter(Collider collider)
    {
        if(collider == _gamemanager.GetPlayer().GetCollider())
        {
            _playerinsidebounds = true;
            _interacttext.enabled = true;
            
            
             
        }
    }


    //When Player exits the trigger box dont show the text and set playerinbounds bool false
    protected virtual void OnTriggerExit(Collider collider)
    {
        if (collider == _gamemanager.GetPlayer().GetCollider())
        {
            _playerinsidebounds = false;
            _interacttext.enabled = false;
        }
    }
}
