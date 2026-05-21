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
            // Шаг 9: создаем Profile
            var profile = new Profile();

            // Шаг 10-12: устанавливаем свойства
            profile.SetDays(days);
            profile.SetPurpose(purpose);
            profile.SetCitizenship(citizenship);

            // Шаг 13-16: создаем и добавляем свойства профиля
            for (int i = 0; i < propertyNames.Count; i++)
            {
                var property = new ProfileProperty();
                property.SetProperty(propertyNames[i], propertyValues[i]);
                profile.AddProperty(property);
            }

            // Шаг 17-18: возвращаем готовый профиль
            return profile;
        }
    }
}