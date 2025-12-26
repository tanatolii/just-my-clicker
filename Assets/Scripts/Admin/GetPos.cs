using NaughtyAttributes;
using UnityEngine;

public class GetPos : MonoBehaviour
{
    [Button]
    public void GetPosition()
    {
        Debug.Log(transform.position);
    }
}
