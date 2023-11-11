using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> vfxList;
    [SerializeField] private List<Vector3> setScale;

    private Dictionary<VFXTag, GameObject> vfxDictionary = new Dictionary<VFXTag, GameObject>();

    private void Awake()
    {
        for(int i = 0; i < vfxList.Count; i++)
        {
            VFXTag tag = (VFXTag)i;
            vfxDictionary[tag] = vfxList[i];
        }
    }

    public void PlayAttackVFX(VFXTag vfxTag, DamageType damageType, bool isCrit, Lane owner)
    {
        GameObject selectedVFX = vfxDictionary[vfxTag];
        Debug.Log(selectedVFX);
        selectedVFX.SetActive(true);
        //SpriteRenderer vfxSprite = selectedVFX.GetComponent<SpriteRenderer>();
        ParticleSystem vfxParticle = selectedVFX.GetComponent<ParticleSystem>();
        

        // Bigger if Crit
        if(isCrit)
        {
            selectedVFX.transform.localScale = setScale[1];
        }
        else
        {
            selectedVFX.transform.localScale = setScale[0];
        }

        // Set Color According to Element
        switch(damageType)
        {
            case DamageType.Fire:
                //vfxSprite.color = Color.red; 
                vfxParticle.startColor = Color.red;
                break;

            case DamageType.Water:
                //vfxSprite.color = Color.blue; 
                vfxParticle.startColor = Color.blue;
                break;

            case DamageType.Wind:
                //vfxSprite.color = Color.green;
                vfxParticle.startColor = Color.green;
                break;

            default:
                //vfxSprite.color = Color.white;
                vfxParticle.startColor = Color.white;
                break;
        }
        
        selectedVFX.GetComponent<ParticleAnimator>().SetOwner(owner);
    }
}
