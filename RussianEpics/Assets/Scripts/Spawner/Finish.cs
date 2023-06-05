using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }
}
