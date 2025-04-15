using UnityEngine;

public class pOcPosition : MonoBehaviour
{
    public GameObject player;
    public Material dissolve;
    // Update is called once per frame
    void Update()
    {
        dissolve.SetVector("_PlayerPosition", player.transform.position);
    }
}
