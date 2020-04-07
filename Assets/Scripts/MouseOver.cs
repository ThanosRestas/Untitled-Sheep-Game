using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MouseOver : MonoBehaviour
{
    private Animator anim;
    private MeshRenderer angry; 
    public GameObject poopModel;
    public GameObject loveMeter;
    public GameObject hungerMeter;
    public GameObject tidynessMeter;
    public List<GameObject> emojiMeshes = new List<GameObject> ();
    private Dictionary<string, GameObject> emotionType = new Dictionary<string, GameObject> ();
    public SaveData save = new SaveData ();
    
    void Awake ()
    {

    }

    // Start is called before the first frame update
    void Start ()
    {
        save = SaveGameManager.Load ();
        loveMeter.GetComponent<ProgressBar> ().current = save.love;
        hungerMeter.GetComponent<ProgressBar> ().current = save.hunger;
        tidynessMeter.GetComponent<ProgressBar> ().current = save.tidyNess;

        anim = gameObject.GetComponent<Animator> ();
        angry = gameObject.GetComponent<MeshRenderer> ();

        foreach (GameObject emotion in emojiMeshes)
        {
           emotionType.Add (emotion.name, emotion);
        }

        InvokeRepeating ("heightenNeeds", 1, 1);
    }

    void OnApplicationQuit ()
    {
        //Debug.Log("Application ending after " + Time.time + " seconds");
        SaveGameManager.Save (save);
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
        showEmotion ("emojiAngry");

    }

    public void showEmotion (string emojiType)
    {
        Vector3 pos = new Vector3 (0f, 3f, 0f);
        GameObject emoji = Instantiate (emotionType[emojiType], transform.position + pos, Quaternion.Euler (0, 120, 20));

        Rigidbody emojiRigid = emoji.GetComponent<Rigidbody> ();
        emojiRigid.AddExplosionForce (100, transform.position + pos, 1);

        

        if(emojiType == "emojiAngry")
        {
            loveMeter.GetComponent<ProgressBar> ().current -= 10;
        }
        else if(emojiType == "emojiLove")
        {
            loveMeter.GetComponent<ProgressBar> ().current += 10;
        }
        else if(emojiType == "emojiLike")
        {
            hungerMeter.GetComponent<ProgressBar> ().current -= 10;
        }
        else if(emojiType == "emojiClean")
        {
            tidynessMeter.GetComponent<ProgressBar> ().current += 10;
        }


    }

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