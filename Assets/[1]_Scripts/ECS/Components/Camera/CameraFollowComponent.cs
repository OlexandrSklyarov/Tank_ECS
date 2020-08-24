using Cinemachine;

namespace SA.Tanks
{
    public struct CameraFollowComponent
    {
        public CinemachineVirtualCamera VirtualCamera { get; set; }
        public bool IsTargetExist { get; set; }
    }

}
