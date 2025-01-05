using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CrushingGateScript : MonoBehaviour
{
    public float closeSpeed = 10f; // The speed the Spikes close at
    public float openSpeed = 2f; // The speed the Spikes go back to base position
    public float waitTime = 1f; // The wait time before the Spikes close or open

    // Spike Bottom Positions
    public Vector2 spikeBottom_initialPos;
    public Vector2 spikeBottom_closedPos;
    public float spikeBottom_yOffset = 2f;
    private GameObject spikeBottom;

    // Spike Top Positions
    public Vector2 spikeTop_initialPos;
    public Vector2 spikeTop_closedPos;
    public float spikeTop_yOffset = -2f;
    public GameObject spikeTop;

    private bool gateAtStartingPosition = true;


    // Start is called before the first frame update
    void Start()
    {
        spikeBottom = transform.GetChild(0).gameObject;
        spikeTop = transform.GetChild(1).gameObject;

        // Save starting position
        spikeBottom_initialPos = spikeBottom.transform.position;
        spikeTop_initialPos = spikeTop.transform.position;

        // Set closed position based on offset
        spikeBottom_closedPos = new Vector2(spikeBottom_initialPos.x, spikeBottom_initialPos.y + spikeBottom_yOffset);
        spikeTop_closedPos = new Vector2(spikeTop_initialPos.x, spikeTop_initialPos.y + spikeTop_yOffset);

        // Start Crushing Loop
        StartCoroutine(Crush());
    }

    private IEnumerator Crush()
    {
        while (true)
        {
            if (gateAtStartingPosition == true)
            {
                //yield return MoveSpikeToPosition(spikeBottom, spikeBottom_closedPos, closeSpeed);
                //yield return MoveSpikeToPosition(spikeTop, spikeTop_closedPos, closeSpeed);

                yield return MoveSpikesTogether(spikeBottom, spikeBottom_closedPos, spikeTop, spikeTop_closedPos, closeSpeed);

                yield return new WaitForSeconds(waitTime);
                gateAtStartingPosition = false;
            }
            else 
            {
                //yield return MoveSpikeToPosition(spikeBottom, spikeBottom_initialPos, openSpeed);
                //yield return MoveSpikeToPosition(spikeTop, spikeTop_initialPos, openSpeed);

                yield return MoveSpikesTogether(spikeBottom, spikeBottom_initialPos, spikeTop, spikeTop_initialPos, closeSpeed);

                yield return new WaitForSeconds(waitTime);
                gateAtStartingPosition = true;
            }
        }
    }
     private IEnumerator MoveSpikeToPosition(GameObject spikeRef, Vector2 targetPos, float speed)
    {
        while ((Vector2)spikeRef.gameObject.transform.position != targetPos)
        {
            spikeRef.gameObject.transform.position = Vector2.MoveTowards(spikeRef.gameObject.transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator MoveSpikesTogether(GameObject spikeRefBot, Vector2 targetPosBottom, GameObject spikeRefTop, Vector2 targetPosTop, float speed)
    {
        while ((Vector2)spikeRefBot.gameObject.transform.position != targetPosBottom)
        {
            spikeRefBot.gameObject.transform.position = Vector2.MoveTowards(spikeRefBot.gameObject.transform.position, targetPosBottom, speed * Time.deltaTime);
            spikeRefTop.gameObject.transform.position = Vector2.MoveTowards(spikeRefTop.gameObject.transform.position, targetPosTop, speed * Time.deltaTime);
            yield return null;
        }
    }

}
