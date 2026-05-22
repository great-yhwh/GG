using PIS_6sem.Entities;

namespace PIS_6sem.Services
{
    public class ProfileFactory
    {
        public Profile CreateProfile(
            int days,
            string purpose,
            string citizenship,
            List<string> propertyNames,
            List<string> propertyValues)
        {
            if (propertyNames.Count != propertyValues.Count)
            {
                throw new ArgumentException("Количество имён свойств и значений свойств не совпадает.");
            }

            var profile = new Profile();

            profile.SetDays(days);
            profile.SetPurpose(purpose);
            profile.SetCitizenship(citizenship);

            for (int i = 0; i < propertyNames.Count; i++)
            {
                var property = new ProfileProperty();
                property.SetProperty(propertyNames[i], propertyValues[i]);
                profile.AddProperty(property);
            }

            return profile;
        }
    }
}