using System.Collections.Generic;

public class FactoryMonoLink : MonoLink
{
    public List<MonoLink> Links = new List<MonoLink>();

    public void ClearAll()
    {
        foreach (var i in Links)
        {
            i.DestroyLink();
        }

        Links.Clear();
    }

    public void Remove(MonoLink link)
    {
        if (Links.Contains(link))
        {
            Links.Remove(link);
            link.DestroyLink();
        }
    }
}
