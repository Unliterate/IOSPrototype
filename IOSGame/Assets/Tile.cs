using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public Material GreenMat = null;
    public Material YellowMat = null;

    public enum ETileType
    {
        TT_Green,
        TT_Yellow
    };

    public ETileType TileType = ETileType.TT_Green;

    private void Start()
    {
        if(Random.Range(0, 5) > 0)
        {
            TileType = ETileType.TT_Green;
            GetComponent<Renderer>().material = GreenMat;
        }
        else
        {
            TileType = ETileType.TT_Yellow;
            GetComponent<Renderer>().material = YellowMat;
        }
    }

    void OnMouseDown()
    {
        TileType = ETileType.TT_Green;
        GetComponent<Renderer>().material = GreenMat;
    }
}
