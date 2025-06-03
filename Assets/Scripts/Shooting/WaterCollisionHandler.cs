using UnityEngine;

public class WaterCollisionHandler : MonoBehaviour
{
    [SerializeField] WaterGun waterGun;
    public void OnParticleCollision(GameObject other)
    {
        waterGun.OnWaterCollision(other);
    }
}
