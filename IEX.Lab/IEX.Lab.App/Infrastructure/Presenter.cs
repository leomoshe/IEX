namespace IEX.Lab.App
{
    public class Presenter<T>
    {
        public T View { get; set; }
        public virtual void OnViewReady() { }
    }
}
