
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PatentRule {

    public PatentRule() {
    }

    public List<Country> criteriaCountries;

    public EntryPurpose criteriaPurpose;

    public MigrantStatus criteriaStatus;

    /// <summary>
    /// @param citizenship 
    /// @param purpose 
    /// @param status 
    /// @param entryDate 
    /// @return
    /// </summary>
    public String PatentMessage(Country citizenship, EntryPurpose purpose, MigrantStatus status, void entryDate) {
        // TODO implement here
        return null;
    }

}