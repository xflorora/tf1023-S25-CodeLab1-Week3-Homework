using Unity.VisualScripting;
using UnityEngine;

public class ShowText : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         //if player collides with scroll object, increase scrollScore
         GameManager.Instance.scrollScore++;

         Destroy(this.gameObject);
      }
   }
}
