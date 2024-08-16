using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Read : Interactable
{
    //Decalration
    private GameManager _gamemanger;
    [SerializeField] private string _audioname;
    private bool _audiofinished;


    // Start is called before the first frame update
    protected override void Start()
    {
        //Initialization
        _gamemanger = FindObjectOfType<GameManager>();
        base.Start();
        _audiofinished = false;
       

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
       
        
    }

    //Accesors
    public bool GetAudioFinished()
    {
       return _audiofinished;
    }

    //When player enter trigger and press E Play audio
    public override void DealInteraction()
    {
        _gamemanger.GetMediaManager().PlayAudioOnce(_gamemanger.GetPlayer().gameObject, _audioname);
        _audiofinished = true;
        _caninteract = false;





    }


    
}
