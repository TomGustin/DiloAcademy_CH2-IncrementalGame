public interface IObserver
{
    void OnNotify(string sender, string messagge);
    void OnNotify(string messagge);
}
