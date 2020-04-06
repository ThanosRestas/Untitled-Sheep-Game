using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode ()]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem ("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar ()
    {
        GameObject obj = Instantiate (Resources.Load<GameObject> ("UI/Linear Progress Bar"));
        obj.transform.SetParent (Selection.activeGameObject.transform, false);
    }

#endif

    public int minimum;
    public int maximum;
    public int current;
    public Image mask;
    public Image fill;
    public Color color;
    private const int ammount = 1;
    // Start is called before the first frame update
    void Start ()
    {
        //Test
    }

    // Update is called once per frame
    void Update ()
    {
        GetCurrentFill ();

    }

    void GetCurrentFill ()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;

        float fillAmmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmmount;

        fill.color = color;
    }

}