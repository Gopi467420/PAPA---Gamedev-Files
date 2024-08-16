using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : Interactable
{
   //Declaration
    public bool open;
    protected AudioSource _audioSource;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Animator.SetBool("Opendoor", false);        
        open = false;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.enabled = true;


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
            _audioSource.Play();

        }
        else if (!Animator.GetBool("Opendoor"))
        {
            open = true;
            Animator.SetBool("Opendoor", open);
            _audioSource.Play();
        }
    }

}
