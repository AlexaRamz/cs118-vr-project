//public class ShootableTarget : MonoBehaviour
//{
//    public GameObject hitDecalPrefab;  
//    public AudioClip hitSound;

//    private AudioSource audioSource;

//    void Start()
//    {
//        audioSource = GetComponent<AudioSource>();
//    }

//    public void OnShot(Vector3 hitPoint, Vector3 hitNormal)
//    {
      
//        if (hitDecalPrefab)
//        {
//            Quaternion decalRotation = Quaternion.LookRotation(hitNormal);
//            GameObject decal = Instantiate(hitDecalPrefab, hitPoint + hitNormal * 0.01f, decalRotation);
//            Destroy(decal, 2f); 
//        }

//        if (hitSound && audioSource)
//        {
//            audioSource.PlayOneShot(hitSound);
//        }
//    }
//}
