
using TMPro;

using UnityEngine;


public class InteractableDrawer : Interactable
{
    private  GameManager gameManager;
    private TextMeshProUGUI _Texttoshowoninteraction;
    public GameObject _texttroshowoninteractiongameobject;
    
    private Animator _animator;
    private bool open;
    private Vector3 _currentposition;
    protected AudioSource _audioSource;




    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        Animator.SetBool("Opendoor", false);
        open = false;

        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        
        _animator = GetComponentInChildren<Animator>();
        _Texttoshowoninteraction = _texttroshowoninteractiongameobject.GetComponent<TextMeshProUGUI>();
        _currentposition = _texttroshowoninteractiongameobject.transform.position;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.enabled =true;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

       
    }



    //Play Drawer Animation
    public override void DealInteraction()
    {

        if (Animator.GetBool("Opendoor"))
        {
            open = false;
            Animator.SetBool("Opendoor", open);
            _texttroshowoninteractiongameobject.SetActive(false);         
           
            _audioSource.Play();
            



        }
        else if (!Animator.GetBool("Opendoor"))
        {
            open = true;
            Animator.SetBool("Opendoor", open);
            _texttroshowoninteractiongameobject.SetActive(true);
            _audioSource.Play();


            
        }
    }
}
