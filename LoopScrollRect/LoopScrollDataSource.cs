namespace UnityEngine.UI
{
    public abstract class LoopScrollDataSource
    {
        public abstract void ProvideData(Transform transform, int idx);
    }

    public class LoopScrollSendIndexSource : LoopScrollDataSource
    {
        public static readonly LoopScrollSendIndexSource Instance = new LoopScrollSendIndexSource();

        private LoopScrollSendIndexSource()
        { }

        public override void ProvideData(Transform transform, int idx)
        {
            transform.SendMessage("ScrollCellIndex", idx);
        }
    }

    public class LoopScrollArraySource<T> : LoopScrollDataSource
    {
        private T[] objectsToFill;

        public LoopScrollArraySource(T[] objectsToFill)
        {
            this.objectsToFill = objectsToFill;
        }

        public override void ProvideData(Transform transform, int idx)
        {
            transform.SendMessage("ScrollCellContent", objectsToFill[idx]);
        }
    }
}