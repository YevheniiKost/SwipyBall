namespace YevheniiKostenko.SwipyBall.Data.Config
{
    public class PlayerConfig
    {
        public float MaxAngleDeviation { get; }
        public float MaxAngle { get; }
        public float TimeBetweenJumps { get; }
        public float TimeBetweenHits { get; }
        
        public float JumpForce { get; }
        public float PushForce { get; }
        public float HitPushForce { get; }
        public float GroundCheckDistance { get; }
        
        public PlayerConfig(
            float maxAngleDeviation,
            float maxAngle,
            float timeBetweenJumps,
            float timeBetweenHits,
            float jumpForce,
            float pushForce,
            float hitPushForce,
            float groundCheckDistance)
        {
            MaxAngleDeviation = maxAngleDeviation;
            MaxAngle = maxAngle;
            TimeBetweenJumps = timeBetweenJumps;
            JumpForce = jumpForce;
            PushForce = pushForce;
            HitPushForce = hitPushForce;
            GroundCheckDistance = groundCheckDistance;
            TimeBetweenHits = timeBetweenHits;
        }
    }
}