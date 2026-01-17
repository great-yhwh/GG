using System;
using System.Collections.Generic;
using System.IO;

public class GovernmentOrganizationCollection
{
    // По ассоциации
    private List<GovernmentOrganization> organizations;

    public GovernmentOrganizationCollection()
    {
        organizations = new List<GovernmentOrganization>();

        String[] lines = File.ReadAllLines("Организации.txt");
        foreach (String line in lines)
        {
            if (!String.IsNullOrWhiteSpace(line))
            {
                organizations.Add(new GovernmentOrganization(line));
            }
        }
    }

    public List<GovernmentOrganization> getAll()
    {
        return this.organizations;
    }
}