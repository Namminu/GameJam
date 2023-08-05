using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoatType { FISH, NET, BOTH }
public class Boat : MonoBehaviour
{
    public BoatType type;
    [Range(0f, 100f)]
    public float fishBoatProbability;

    [Range(0f, 100f)]
    public float netBoatProbability;

    [Range(0f, 100f)]
    public float bothBoatProbability;

    [Header("그물망 보트 설정")]
    [SerializeField] private GameObject net;
    [SerializeField] private float netSpreadTime;
    [SerializeField] private float netFallSpeed;

    [Header("낚시 보트 설정")]
    [SerializeField] private GameObject fishing;
    [SerializeField] private GameObject body;
    [SerializeField] private Transform hookPoint;
    [SerializeField] private GameObject hook;
    [SerializeField] private float fishingLineSpreadTime;

    private bool patternOn = false;

    private void Awake()
    {
        net.SetActive(false);
        fishing.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        float allProb = fishBoatProbability + netBoatProbability + bothBoatProbability;
        float rand = Random.Range(0f, allProb);
        if (rand < allProb - netBoatProbability - bothBoatProbability)
            type = BoatType.FISH;
        else if (rand >= fishBoatProbability && rand < allProb - bothBoatProbability)
            type = BoatType.NET;
        else if(rand >= fishBoatProbability + netBoatProbability)
            type = BoatType.BOTH;
    }

    private void StartPattern()
    {
        switch (type)
        {
            case BoatType.FISH:
                fishing.SetActive(true);
                StartCoroutine(FishBoat());
                break;
            case BoatType.NET:
                net.SetActive(true);
                StartCoroutine(NetBoat());
                break;
            default:
                fishing.SetActive(true);
                net.SetActive(true);
                StartCoroutine(FishBoat());
                StartCoroutine(NetBoat());
                break;
        }
    }

    IEnumerator FishBoat()
    {
        yield return new WaitForSeconds(0.5f);
        float timer = 0f;
        Vector3 lineScale = body.transform.localScale;
        float lineScaleY = lineScale.y;
        float lineScaleYMax = Random.Range(lineScaleY, 1f);
        while (timer <= fishingLineSpreadTime)
        {
            hook.transform.SetParent(null);
            lineScale.y = Mathf.Lerp(lineScaleY, lineScaleYMax, timer / fishingLineSpreadTime);
            body.transform.localScale = lineScale;
            hook.transform.position = hookPoint.transform.position;
            timer += Time.deltaTime;
            yield return null;
        }
        hook.transform.SetParent(hookPoint);
    }

    IEnumerator NetBoat()
    {
        float timer = 0f;
        Vector3 netSpreadVec = net.transform.localScale;
        while (timer <= netSpreadTime)
        {
            netSpreadVec.y = Mathf.Lerp(0f, 1f, timer / netSpreadTime);
            net.transform.localScale = netSpreadVec;
            timer += Time.deltaTime;
            yield return null;
        }

        net.GetComponent<AutoScroll>().enabled = true;
        net.transform.SetParent(null);
        timer = 0f;

        while (timer < 5f)
        {
            net.transform.position -= new Vector3(0, Time.deltaTime * netFallSpeed * timer, 0);
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(net);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BoatTrigger"))
        {
            if (!patternOn)
            {
                StartPattern();
                patternOn = true;
            }
        }
    }
}
