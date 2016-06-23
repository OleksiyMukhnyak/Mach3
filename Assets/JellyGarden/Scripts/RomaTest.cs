using UnityEngine; 
using System.Collections;

public class RomaTest : MonoBehaviour
{
     
    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5);

        Debug.Log("Start");
        for (int i = 0; i < 7; i++)
        {
            InitScript.Instance.BuyBoost((BoostType)i, 0, 10);
        }
        InitScript.Instance.BuyBoost(BoostType.Random_color, 100, 10);
        // GameObject.FindObjectsOfType<InitScript>().ReloadBoosts();
        // PlayerPrefs.DeleteAll();
        //foreach (var item in GameObject.FindObjectsOfType<BoostIcon>())
        //{
        //    item.ActivateBoost();
        //    Debug.Log("GameObject.FindObjectsOfType<BoostIcon>()");
        //}
        //  ActivateBoost();
    }
     
    // Update is called once per frame
    void Update()
    {

    }
}
