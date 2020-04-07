using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MouseOver : MonoBehaviour
{

    private Animator anim;
    private MeshRenderer angry;
    public GameObject emojiAngry;
    public GameObject emojiLoved;
    public GameObject emojiLike;
    public GameObject emojiClean;
    public GameObject poopModel;
    public GameObject loveMeter;
    public GameObject hungerMeter;
    public GameObject tidynessMeter;    
    public List<GameObject> emotions = new List<GameObject>();   
    private Dictionary<string, GameObject> emotions2 = new Dictionary<string, GameObject>(); 
    public SaveData save = new SaveData();
    void Awake() {
        
    }
     
    // Start is called before the first frame update
    void Start ()
    {
        save = SaveGameManager.Load();
        loveMeter.GetComponent<ProgressBar> ().current = save.love;
        hungerMeter.GetComponent<ProgressBar>().current = save.hunger;
        tidynessMeter.GetComponent<ProgressBar>().current = save.tidyNess;
        
        anim = gameObject.GetComponent<Animator> ();
        angry = gameObject.GetComponent<MeshRenderer> ();       
        
        foreach(GameObject emotion in emotions)
        {
            emotions2.Add(emotion.name, emotion);
        }


        InvokeRepeating ("heightenNeeds", 1, 1);



    }

    void OnApplicationQuit()
    {
        //Debug.Log("Application ending after " + Time.time + " seconds");
        SaveGameManager.Save(save);

    }

    private void Update ()
    {
        save.love = loveMeter.GetComponent<ProgressBar> ().current;
        save.hunger = hungerMeter.GetComponent<ProgressBar> ().current;
        save.tidyNess = tidynessMeter.GetComponent<ProgressBar> ().current;
    }

    void OnMouseDown ()
    {
        //Debug.Log ("Pet clicked !");  
        anim.Play ("stun");
        //emotion ("emojiAngry");
        showEmotion("emojiAngry");

    }

    public void showEmotion(string emojiType)
    {        
        Vector3 pos = new Vector3 (0f, 3f, 0f);
        GameObject emoji = Instantiate (emotions2[emojiType], transform.position + pos, Quaternion.Euler (0, 120, 20));

        Rigidbody emojiRigid = emoji.GetComponent<Rigidbody> ();
        emojiRigid.AddExplosionForce (100, transform.position + pos, 1);

        loveMeter.GetComponent<ProgressBar> ().current -= 10;
    }

    /*public void feelingAngry ()
    {
        Vector3 pos = new Vector3 (0f, 3f, 0f);
        GameObject emoji = Instantiate (emojiAngry, transform.position + pos, Quaternion.Euler (0, 120, 20));

        Rigidbody emojiRigid = emoji.GetComponent<Rigidbody> ();
        emojiRigid.AddExplosionForce (100, transform.position + pos, 1);

        loveMeter.GetComponent<ProgressBar> ().current -= 10;

    }

    public void feelingLoved ()
    {
        Vector3 pos = new Vector3 (0f, 3f, 0f);
        GameObject emoji = Instantiate (emojiLoved, transform.position + pos, Quaternion.Euler (0, 120, 20));

        Rigidbody emojiRigid = emoji.GetComponent<Rigidbody> ();
        emojiRigid.AddExplosionForce (100, transform.position + pos, 1);

        loveMeter.GetComponent<ProgressBar> ().current += 10;
    }

    public void feelingSatisfied ()
    {
        Vector3 pos = new Vector3 (0f, 3f, 0f);
        GameObject emoji = Instantiate (emojiLike, transform.position + pos, Quaternion.Euler (0, 120, 20));

        Rigidbody emojiRigid = emoji.GetComponent<Rigidbody> ();
        emojiRigid.AddExplosionForce (100, transform.position + pos, 1);

        hungerMeter.GetComponent<ProgressBar> ().current += 10;
    }

    public void feelingClean ()
    {
        Vector3 pos = new Vector3 (0f, 3f, 0f);
        GameObject emoji = Instantiate (emojiClean, transform.position + pos, Quaternion.Euler (0, 120, 20));

        Rigidbody emojiRigid = emoji.GetComponent<Rigidbody> ();
        emojiRigid.AddExplosionForce (100, transform.position + pos, 1);

        tidynessMeter.GetComponent<ProgressBar> ().current += 10;
    }*/

    public void poop ()
    {
        if (Random.value < 0.5)
        {
            Instantiate (poopModel, transform.position + new Vector3 (0f, 0.5f, 0f), Quaternion.Euler (0, 0, 0));
            tidynessMeter.GetComponent<ProgressBar> ().current -= 10;

        }

    }

    private void heightenNeeds ()
    {
        //loveMeter.GetComponent<ProgressBar> ().current -= 1;
        //hungerMeter.GetComponent<ProgressBar> ().current += 1;
    }

}