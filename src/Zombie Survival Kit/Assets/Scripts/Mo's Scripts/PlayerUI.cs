using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    RectTransform hpBarFill;

    [SerializeField]
    Text ammoLeft;

    [SerializeField]
    GameObject gunObject;

    private PlayerStats stats;
    private Gun gun;

    public void SetStats(PlayerStats playerStats)
    {
        stats = playerStats;
        gun = gunObject.GetComponent<Gun>();
    }

    private void Update()
    {
        SetHPFill(stats.curHealth);
        SetAmmoAmount(gun.getAmmoInClip());
    }

    void SetHPFill(float amount)
    {
        hpBarFill.localScale = new Vector3(1f, amount/100f, 1f);
    }

    void SetAmmoAmount(int amount)
    {
        ammoLeft.text = amount.ToString();
    }
}
