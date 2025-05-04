namespace Interfaces
{
    public interface IPassivable
    {
        public void ApplyPassiveEffect();
        public void PowerUpPassiveEffect(int value);
        public void PowerUpPassiveEffect(float value);
    }
}