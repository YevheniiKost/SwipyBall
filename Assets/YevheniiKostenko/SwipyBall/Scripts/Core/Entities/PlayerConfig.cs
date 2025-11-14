namespace YevheniiKostenko.SwipyBall.Core.Entities
{
    public class PlayerConfig
    {
        public float MaxAngleDeviation { get; }
        public float MaxAngle { get; }
        public float TimeBetweenJumps { get; }
        
        public float JumpForce { get; }
        public float PushForce { get; }
        public float GroundCheckDistance { get; }
        public int MaxJumpCount { get; }
        public float NextJumpDecreaseFactor { get; }
        
        public PlayerConfig(
            float maxAngleDeviation,
            float maxAngle,
            float timeBetweenJumps,
            float jumpForce,
            float pushForce,
            float groundCheckDistance,
            int maxJumpCount,
            float nextJumpDecreaseFactor)
        {
            MaxAngleDeviation = maxAngleDeviation;
            MaxAngle = maxAngle;
            TimeBetweenJumps = timeBetweenJumps;
            JumpForce = jumpForce;
            PushForce = pushForce;
            GroundCheckDistance = groundCheckDistance;
            MaxJumpCount = maxJumpCount;
            NextJumpDecreaseFactor = nextJumpDecreaseFactor;
        }
    }
}