using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CountryCollection
{
    private List<Country> countries;

    public CountryCollection()
    {
        countries = new List<Country>();
        if (File.Exists("Гражданства.txt"))
        {
            String[] lines = File.ReadAllLines("Гражданства.txt");
            foreach (String line in lines)
            {
                countries.Add(new Country(line));
            }
        }
    }

    // Возвращаем список строк для UI
    public List<String> getAllCountry()
    {
        List<String> result = new List<String>();
        foreach (Country c in countries)
        {
            result.Add(c.getName());
        }
        return result;
    }

    public Country FindCitizenship(String citizenshipStr)
    {
        foreach (Country c in countries)
        {
            if (c.getName() == citizenshipStr)
            {
                return c;
            }
        }
        return null;
    }
}