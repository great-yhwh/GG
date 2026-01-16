
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

    public String messagePatentNeed;

    public String messagePatentNoNeed;

    public int criteriaDate;

    /// <summary>
    /// @param foreignCitizen 
    /// @return
    /// </summary>
    public String PatentMessage(ForeignCitizen foreignCitizen) {
        // TODO implement here
        return null;
    }

}