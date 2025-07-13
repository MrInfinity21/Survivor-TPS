using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] Transform _recoilFollowPos;
    [SerializeField] float _kickBackAmount = -1f;
    [SerializeField] float _kickBackSpeed = 10f, _returnSpeed = 20f;
    float _currentRecoilPosition, _finalRecoilPosition;

    void Update()
    {
        _currentRecoilPosition = Mathf.Lerp(_currentRecoilPosition, 0, _returnSpeed *  Time.deltaTime);
        _finalRecoilPosition = Mathf.Lerp(_finalRecoilPosition, _currentRecoilPosition, _kickBackSpeed * Time.deltaTime);
        _recoilFollowPos.localPosition = new Vector3(0, 0, _finalRecoilPosition);
    }

    public void TriggerRecoil() => _currentRecoilPosition += _kickBackAmount;

}
