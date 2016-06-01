using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoostAnimation : MonoBehaviour
{
    public Square square;

    public void ShowEffect()
    {
        GameObject partcl = Instantiate(Resources.Load("Prefabs/Effects/Firework"), transform.position, Quaternion.identity) as GameObject;
        partcl.GetComponent<ParticleSystem>().startColor = LevelManager.THIS.scoresColors[square.item.color];
        Destroy(partcl, 1f);

    }
    public void OnFinished(BoostType boostType)
    {
        if (boostType == BoostType.Random_color)
        {

            List<Item> itemsList = LevelManager.THIS.GetItemsAround(square);
            foreach (Item item in itemsList)
            {
                if (item != null)
                {
                    if (item.currentType == ItemsTypes.NONE)
                        item.GenColor(-1, true);
                }
            }

        }
        if (boostType == BoostType.Bomb)
        {
            square.item.DestroyItem();
        }
        LevelManager.THIS.StartCoroutine(LevelManager.THIS.FindMatchDelay());
        SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.explosion);

        Destroy(gameObject);
    }
}
