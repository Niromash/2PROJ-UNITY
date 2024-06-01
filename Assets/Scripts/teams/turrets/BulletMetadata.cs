using UnityEngine;

// This class is used to store the source turret of the bullet
public class BulletMetadata : MonoBehaviour
{
    private Turret sourceTurret;
    
    public Turret GetSourceTurret()
    {
        return sourceTurret;
    }

    public void SetSourceTurret(Turret sourceTurret)
    {
        this.sourceTurret = sourceTurret;
    }
}