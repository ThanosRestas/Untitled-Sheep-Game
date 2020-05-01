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
    public GameObject[] poopInScene;
    public Vector3 poopPosition;

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

        // Instantiate a poop prefab for every poop position
        foreach (PoopPosition poop in save.poopPositions)
        {

            Instantiate (poopModel, poop.returnVector (), Quaternion.Euler (0, 0, 0));
        }
        save.poopPositions.Clear ();

        anim = gameObject.GetComponent<Animator> ();
        angry = gameObject.GetComponent<MeshRenderer> ();

        foreach (GameObject emotion in emojiMeshes)
        {
            emotionType.Add (emotion.name, emotion);
        }

        //private int timePassedSinceQuit = (int)SaveGameManager.loadMinusSave.TotalSeconds ;
        //loveMeter.GetComponent<ProgressBar> ().current -= (int)SaveGameManager.loadMinusSave.TotalSeconds * 1;
        //hungerMeter.GetComponent<ProgressBar> ().current += (int)SaveGameManager.loadMinusSave.TotalSeconds * 1;

        //loveMeter.GetComponent<ProgressBar> ().current -= (int)SaveGameManager.loadMinusSave.TotalSeconds * 1;

        InvokeRepeating ("heightenNeeds", 1, 1);
    }

    void OnApplicationQuit ()
    {
        //Debug.Log("Application ending after " + Time.time + " seconds");
        scanforPoop ();
        SaveGameManager.Save (save);
    }

    private void scanforPoop ()
    {
        poopInScene = GameObject.FindGameObjectsWithTag ("Poop");

        foreach (GameObject poop in poopInScene)
        {

            save.poopPositions.Add (new PoopPosition (poop.transform.position.x, poop.transform.position.y, poop.transform.position.z));
        }
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
        poop ();

    }

    public void showEmotion (string emojiType)
    {
        Vector3 pos = new Vector3 (0f, 3f, 0f);
        GameObject emoji = Instantiate (emotionType[emojiType], transform.position + pos, Quaternion.Euler (0, 120, 20));

        Rigidbody emojiRigid = emoji.GetComponent<Rigidbody> ();
        emojiRigid.AddExplosionForce (100, transform.position + pos, 1);

        if (emojiType == "emojiAngry")
        {
            loveMeter.GetComponent<ProgressBar> ().current -= 10;
        }
        else if (emojiType == "emojiLove")
        {
            loveMeter.GetComponent<ProgressBar> ().current += 10;
        }
        else if (emojiType == "emojiLike")
        {
            hungerMeter.GetComponent<ProgressBar> ().current -= 10;
        }
        else if (emojiType == "emojiClean")
        {
            tidynessMeter.GetComponent<ProgressBar> ().current += 10;
        }

    }

    public void poop ()
    {

        if (Random.value < 0.5)
        {

            GameObject poopInstance = Instantiate (poopModel, transform.position + transform.forward * -2, Quaternion.Euler (0, -150, 0));

            tidynessMeter.GetComponent<ProgressBar> ().current -= 10;

        }

    }

    private void heightenNeeds ()
    {
        //loveMeter.GetComponent<ProgressBar> ().current -= 1;
        //hungerMeter.GetComponent<ProgressBar> ().current += 1;
    }

}