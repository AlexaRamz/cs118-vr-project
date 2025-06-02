using UnityEngine;

public class WaterGun : MonoBehaviour
{
    [SerializeField] private ParticleSystem waterStreamEffect;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            waterStreamEffect.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            waterStreamEffect.Stop();
        }
    }

    public void OnWaterCollision(GameObject hitObject)
    {
        IShootable shootable = hitObject.GetComponent<IShootable>();
        if (shootable != null)
        {
            shootable.OnHit();
            Debug.Log("Did Hit");
        }
    }
}
