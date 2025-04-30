using UnityEngine;

public class Block : MonoBehaviour, IGridBuildable
{
    public Vector3Int gridPosition { get; set; }

    public void SetColor(Color32 newColor)
    {
        GetComponent<MeshRenderer>().material.color = newColor;
    }
}
