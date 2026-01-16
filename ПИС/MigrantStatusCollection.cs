using System;
using System.Collections.Generic;
using System.IO;

public class MigrantStatusCollection
{
    private List<MigrantStatus> statuses;

    public MigrantStatusCollection()
    {
        statuses = new List<MigrantStatus>();
        if (File.Exists("Статус.txt"))
        {
            String[] lines = File.ReadAllLines("Статус.txt");
            foreach (String line in lines)
            {
                statuses.Add(new MigrantStatus(line));
            }
        }
    }

    public List<String> getAllStatusCollection()
    {
        List<String> result = new List<String>();
        foreach (MigrantStatus s in statuses)
        {
            result.Add(s.getName());
        }
        return result;
    }

    public MigrantStatus FindStatus(string statusStr)
    {
        return null;
    }
}