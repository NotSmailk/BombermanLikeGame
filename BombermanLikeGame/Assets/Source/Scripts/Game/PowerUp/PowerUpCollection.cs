using System.Collections.Generic;

public class PowerUpCollection
{
    public List<MonoLink> links = new List<MonoLink>();

    public void ClearAll()
    {
        foreach (var link in links)
        {
            link?.DestroyLink();
        }

        links.Clear();
    }

    public void Remove(MonoLink link)
    {
        if (links.Contains(link))
        {
            links.Remove(link);
            link?.DestroyLink();
        }
    }
}
