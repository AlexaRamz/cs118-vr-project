using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockBuilding : MonoBehaviour
{
    public bool canEdit;
    public GameObject newBlock;
    bool canRepeat;

    void Start()
    {
        canRepeat = true;
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
        canRepeat = true;
    }
    void Combine(GameObject block)
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        Destroy(this.gameObject.GetComponent<MeshCollider>());

        int i = 0;
        Debug.Log(meshFilters.Length);
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine, true);
        transform.GetComponent<MeshFilter>().mesh.RecalculateBounds();
        transform.GetComponent<MeshFilter>().mesh.RecalculateNormals();
        transform.GetComponent<MeshFilter>().mesh.Optimize();

        this.gameObject.AddComponent<MeshCollider>();
        transform.gameObject.SetActive(true);

        Destroy(block);

        canRepeat = false;
        StartCoroutine(Delay());
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && canEdit && canRepeat)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 1000.0f))
            {
                //generate new block
                Vector3 blockPos = hit.point + hit.normal / 2.0f;

                blockPos.x = (float)Math.Round(blockPos.x, MidpointRounding.AwayFromZero);
                blockPos.y = (float)Math.Round(blockPos.y, MidpointRounding.AwayFromZero);
                blockPos.z = (float)Math.Round(blockPos.z, MidpointRounding.AwayFromZero);

                GameObject block = (GameObject)Instantiate(newBlock, blockPos, Quaternion.identity);
                block.transform.parent = this.transform;
                Combine(block);
            }
        }
    }
}
