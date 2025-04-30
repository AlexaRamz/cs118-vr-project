using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid3D<T>
{
    private Vector3Int gridSize; // Dimensions of the grid in terms of number of cells
    private Vector3 worldPosition = Vector3.zero; // World space position of the grid's origin
    private Quaternion worldRotation = Quaternion.identity; // World space rotation of the grid
    private Vector3 voxelSize = new Vector3(1f, 1f, 1f); // Dimensions of a single voxel (cube) in world units

    Dictionary<Vector3Int, T> gridObjects;

    public Grid3D(Vector3Int gridSize, Vector3 worldPosition, Quaternion worldRotation, Vector3 voxelSize)
    {
        this.gridSize = gridSize;
        this.worldPosition = worldPosition;
        this.worldRotation = worldRotation;
        this.voxelSize = voxelSize;
    }

    public T GetObject(Vector3Int gridPos)
    {
        if (IsWithinGrid(gridPos) && gridObjects.ContainsKey(gridPos)) {
            return gridObjects[gridPos];
        }
        return default(T);
    }

    public bool SetObject(Vector3Int gridPos, T obj)
    {
        if (IsWithinGrid(gridPos))
        {
            gridObjects[gridPos] = obj;
            return true;
        }
        return false;
    }

    public bool RemoveObject(Vector3Int gridPos)
    {
        if (gridObjects.ContainsKey(gridPos))
        {
            gridObjects.Remove(gridPos);
            return true;
        }
        return false;
    }

    public T GetObjectAtWorldPosition(Vector3 worldPos)
    {
        Vector3Int gridPos = WorldToGridPosition(worldPos);
        return GetObject(gridPos);
    }

    public bool SetObjectAtWorldPosition(Vector3 worldPos, T obj)
    {
        Vector3Int gridPos = WorldToGridPosition(worldPos);
        return SetObject(gridPos, obj);
    }

    public bool RemoveObjectAtWorldPosition(Vector3 worldPos)
    {
        Vector3Int gridPos = WorldToGridPosition(worldPos);
        return RemoveObject(gridPos);
    }

    public Vector3 GridToWorldPosition(Vector3Int gridPos)
    {
        return worldRotation * Vector3.Scale(gridPos, voxelSize) + worldPosition;
    }

    public Vector3Int WorldToGridPosition(Vector3 worldPos)
    {
        Vector3 floatGridPos = (Quaternion.Inverse(worldRotation) * (worldPos - worldPosition));
        Vector3 scaledGridPos = new Vector3(floatGridPos.x / voxelSize.x, floatGridPos.y / voxelSize.y, floatGridPos.z / voxelSize.z);
        return new Vector3Int(Mathf.FloorToInt(scaledGridPos.x), 
                              Mathf.FloorToInt(scaledGridPos.y), 
                              Mathf.FloorToInt(scaledGridPos.z));
    }

    public bool IsWithinGrid(Vector3Int gridPos)
    {
        return gridPos.x >= 0 && gridPos.x < gridSize.x && gridPos.y >= 0 && gridPos.y < gridSize.y && gridPos.z >= 0 && gridPos.z < gridSize.z;
    }
}
