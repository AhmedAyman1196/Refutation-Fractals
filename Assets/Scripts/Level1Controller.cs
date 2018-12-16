using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : MonoBehaviour {

    float time = 0;
    float timeLastMinionInstantiated = 0, instantiationTimeDifference = 5;
    int[] cntMinionsInstantiatedInFloor;
    GameObject [][] references = new GameObject[3][];
    public static int maxFloorReached = 0, kills = 0;
    GameObject finish;
    Light finishLight;
    public static  int TARGET_KILLS = 10;
    // Use this for initialization
    void Start () {
        references[0] = new GameObject[] { GameObject.FindGameObjectsWithTag("Floor0Reference0")[0],
                                            GameObject.FindGameObjectsWithTag("Floor0Reference1")[0],
                                              GameObject.FindGameObjectsWithTag("Floor0Reference2")[0]  };

        references[1] = new GameObject[] { GameObject.FindGameObjectsWithTag("Floor1Reference0")[0],
                                            GameObject.FindGameObjectsWithTag("Floor1Reference1")[0],
                                              GameObject.FindGameObjectsWithTag("Floor1Reference2")[0]  };

        references[2] = new GameObject[] { GameObject.FindGameObjectsWithTag("Floor2Reference0")[0],
                                            GameObject.FindGameObjectsWithTag("Floor2Reference1")[0],
                                              GameObject.FindGameObjectsWithTag("Floor2Reference2")[0]  };
        finish = GameObject.FindGameObjectsWithTag("Finish")[0];
        finishLight = GameObject.FindGameObjectsWithTag("FinishLight")[0].GetComponent<Light>();
        cntMinionsInstantiatedInFloor = new int[3];
    }

	// Update is called once per frame
	void Update () {
        finish.SetActive(kills >= TARGET_KILLS);
        finishLight.intensity = (kills >= TARGET_KILLS ? 7f : 0);
        time += Time.deltaTime;
        if(time - timeLastMinionInstantiated >= instantiationTimeDifference) {
            timeLastMinionInstantiated = time;
            for(int floor = 0; floor <= maxFloorReached; floor++) {
                if (cntMinionsInstantiatedInFloor[floor] == 4) continue;
                cntMinionsInstantiatedInFloor[floor]++;
                int randomPlace = Random.Range(0, references[floor].Length);
                Debug.Log("Instantiated a Minion in floor : " + floor + " with reference position : " + references[floor][randomPlace].name);
                GameObject instance = Instantiate(Resources.Load("Mutant minion", typeof(GameObject)), references[floor][randomPlace].transform) as GameObject;
            }
        }
	}
}
