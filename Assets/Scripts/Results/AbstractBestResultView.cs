using UnityEngine;

public abstract class AbstractBestResultView<T> : MonoBehaviour
{
    protected T _bestResult;

    protected virtual void OnEnable() => UpdateView();

    protected abstract void LoadResult();

    protected abstract void SetToView();

    public virtual void UpdateView()
    {
        LoadResult();
        SetToView();
    }
}
