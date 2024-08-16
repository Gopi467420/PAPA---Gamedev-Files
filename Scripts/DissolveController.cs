using System.Collections;

using UnityEngine;



public class DissolveController : MonoBehaviour
{

    //Declaration 
    public SkinnedMeshRenderer skinnedmesh;
    private Material[] skinnedmaterials;
    public float dissolveRate = 0.0000025f;
    public float refreshRate = 0.025f;
    private bool _dissolveeffectcompleted;



    // Start is called before the first frame update
    void Start()
    {

        if(skinnedmesh != null) { skinnedmaterials = skinnedmesh.sharedMaterials; }
        _dissolveeffectcompleted = false;


        //Apply the effect to all the materials
        for (int i = 0; i < skinnedmaterials.Length; i++)
        {
            skinnedmaterials[i].SetFloat("_DissolveAmount", 0);

        }
    }

 

    public bool GetDissolveEffectCompleted() { return _dissolveeffectcompleted; }

    //Dissolve effect for the ghost 
   public IEnumerator DissolveCo()
      
    {

        if(skinnedmaterials.Length > 0)
        {
            float counter = 0;

            while (skinnedmaterials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for(int i = 0; i <skinnedmaterials.Length;i++)
                {
                    skinnedmaterials[i].SetFloat("_DissolveAmount", counter);

                }
                yield return new WaitForSeconds(refreshRate);
            }

            _dissolveeffectcompleted = true;
        }
    }
}
