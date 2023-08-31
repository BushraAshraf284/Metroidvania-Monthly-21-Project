using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : MonoBehaviour
{
    public string weaponName;
    public Sprite icon;
    public GameObject weaponPrefab;

    public void Activate(Vector3 position, Quaternion rotation)
    {
        Instantiate(weaponPrefab, position, rotation);
    }
}
