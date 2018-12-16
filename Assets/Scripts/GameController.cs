using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{

    public static GameObject player, khattabHips ,boss;
    public static GameObject[] minions;
    public static Animator khattabAnimator;
    // Use this for initialization

    //  Ayman : I updated this to be on Awake rather than on start

    // void Start()
    // {
    //     player = GameObject.FindGameObjectsWithTag("Khattab")[0];
    //     minions = GameObject.FindGameObjectsWithTag("Minion");
    //     khattabHips = GameObject.FindGameObjectsWithTag("KhattabHips")[0];
    //     khattabAnimator = player.GetComponent<Animator>();
    // }

    void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Khattab")[0];
        minions = GameObject.FindGameObjectsWithTag("Minion");
        khattabHips = GameObject.FindGameObjectsWithTag("KhattabHips")[0];
        khattabAnimator = player.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static GameObject findChildWithTag(GameObject g, string tag)
    {
        foreach (Transform child in g.transform)
        {
            if (child.gameObject.tag == tag)
            {
                return child.gameObject;
            }
            else
            {
                GameObject go = findChildWithTag(child.gameObject, tag);
                if (go != null) return go;
            }
        }
        return null;
    }

    public static GameObject findParentWithTag(GameObject g, string tag)
    {
        GameObject parent = g.transform.parent.gameObject;
        if (parent.tag == tag)
        {
            return parent;
        }
        else
        {
            GameObject go = findParentWithTag(parent, tag);
            if (go != null) return go;
        }
        return null;
    }

}
