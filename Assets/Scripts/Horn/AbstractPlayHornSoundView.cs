using UnityEngine;

namespace Train.Horn
{
    public class AbstractPlayHornSoundView : MonoBehaviour
    {
        protected IHornSound _hornSound;

        public virtual void Construct(IHornSound hornSound) => _hornSound = hornSound;

        public void PlayHornSound() => _hornSound.PlaySound();
    }
}
