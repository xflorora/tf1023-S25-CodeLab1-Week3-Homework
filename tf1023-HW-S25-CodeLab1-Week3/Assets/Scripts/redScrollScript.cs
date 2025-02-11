using UnityEngine;

public class redScrollScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //if player collides with scroll object, increase scrollScore
            GameManager.Instance.RedScroll++;
            GameManager.Instance.totalScroll++;
            Destroy(this.gameObject);
        }
    }
}
