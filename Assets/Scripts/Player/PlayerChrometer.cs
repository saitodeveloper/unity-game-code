using System.Collections.Generic;

public class PlayerChronometer
{
    public class ChronometerConstraints
    {
        public float Timer;
        public float RelaxConstant;

        public ChronometerConstraints(float timer, float relaxConstant)
        {
            Timer = timer;
            RelaxConstant = relaxConstant;
        }
    }

    public enum ChronometerType
    {
        AttackTimer,
        HealthRecoveryTimer
    }

    private Dictionary<string, ChronometerConstraints> _chronometerSet;

    public PlayerChronometer()
    {
        _chronometerSet = new Dictionary<string, ChronometerConstraints>();
    }

    public void RegisterChroometer(ChronometerType chronometerType, float timer)
    {
        var key = GetStatusName(chronometerType);

        if (!_chronometerSet.ContainsKey(key))
        {
            _chronometerSet.Add(
                key,
                new ChronometerConstraints(timer, timer)
            );
        }
    }

    public float GetInstantTimer(ChronometerType chronometerType)
    {
        var key = GetStatusName(chronometerType);
        if (_chronometerSet.ContainsKey(key)) return _chronometerSet[key].Timer;
        else return 0f;
    }

    private string GetStatusName(ChronometerType chronometerType)
    {
        return chronometerType.ToString();
    }

    public void CalculateTimers(float deltaTime)
    {
        List<string> keys = new List<string>(_chronometerSet.Keys);

        foreach (var key in keys)
        {
            if (_chronometerSet[key].Timer <= 0f)
            {
                _chronometerSet[key].Timer = _chronometerSet[key].RelaxConstant;
            }
            else
            {
                _chronometerSet[key].Timer -= deltaTime;
            }
        }
    }
}