using System;
using System.Collections.Generic;
using System.IO;

public class EntryPurposeCollection
{
    private List<EntryPurpose> purposes;

    public EntryPurposeCollection()
    {
        purposes = new List<EntryPurpose>();
        if (File.Exists("ײוכüֲתוחהא.txt"))
        {
            String[] lines = File.ReadAllLines("ײוכüֲתוחהא.txt");
            foreach (String line in lines)
            {
                purposes.Add(new EntryPurpose(line));
            }
        }
    }

    public List<String> getAllPurpose()
    {
        List<String> result = new List<String>();
        foreach (EntryPurpose p in purposes)
        {
            result.Add(p.getName());
        }
        return result;
    }

    public EntryPurpose FindPurpose(String purposeStr)
    {
        foreach (EntryPurpose p in purposes)
        {
            if (p.getName() == purposeStr)
            {
                return p;
            }
        }
        return null;
    }
}