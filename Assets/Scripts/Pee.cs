public class Pee : Water
{
    public int monsterIndex;
    public int peeAmount = 20;
    
    public override bool OnCapture()
    {
        if (base.OnCapture()) {
            return true;
        }

        print("Water captured");
        ResourceManager.Instance.GainPee(monsterIndex, peeAmount);
        Animate();
        return false;

    }

    protected override void PlayAudio() {
        audioSource.Play();
        LeanTween.value(gameObject, audioSource.volume, 0, 1f).setOnComplete(() => {
            audioSource.Stop();
        }).setOnUpdate((float value) => {
            audioSource.volume = value;
        });
    }

}