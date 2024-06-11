using System.Collections;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager _instance;
    public static ResourceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("ResourceManager").AddComponent<ResourceManager>();
            }
            return _instance;
        }
    }

    private GameObject resourceRock;
    private GameObject resourceTree;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        resourceRock = Resources.Load<GameObject>("Resource_Rock");
        resourceTree = Resources.Load<GameObject>("Resource_Tree");

        Instantiate(resourceRock);
        Instantiate(resourceTree);
    }

    public void RespawnResource(string resourceName, float delayTime)
    {
        if (resourceName == "rock")
        {
            StartCoroutine(WaitandRespawn(resourceRock, delayTime));
        }
        else if (resourceName == "tree")
        {
            StartCoroutine(WaitandRespawn(resourceTree, delayTime));
        }
    }

    private IEnumerator WaitandRespawn(GameObject respawnResource, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Instantiate(respawnResource);
    }
}
