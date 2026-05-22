import { useState } from 'react';

function App() {
  const [ruleName, setRuleName] = useState('');
  const [targetDocs, setTargetDocs] = useState('');
  const [guidanceDescription, setGuidanceDescription] = useState('');
  const [refusal, setRefusal] = useState('');
  const [orgNames, setOrgNames] = useState('');
  const [orgAddresses, setOrgAddresses] = useState('');

  // Профили: массив объектов
  const [profiles, setProfiles] = useState([
    {
      days: 90,
      purposes: '',   // строка, разделённая запятыми
      citizenships: '',
      properties: [], // массив {name, value}
    },
  ]);

  const [result, setResult] = useState(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  // Добавить/удалить профиль
  const addProfile = () => {
    setProfiles([
      ...profiles,
      { days: 0, purposes: '', citizenships: '', properties: [] },
    ]);
  };

  const removeProfile = (index) => {
    setProfiles(profiles.filter((_, i) => i !== index));
  };

  const updateProfile = (index, field, value) => {
    const updated = [...profiles];
    updated[index][field] = value;
    setProfiles(updated);
  };

  // Добавить/удалить свойство в профиле
  const addProperty = (profileIndex) => {
    const updated = [...profiles];
    updated[profileIndex].properties.push({ name: '', value: '' });
    setProfiles(updated);
  };

  const removeProperty = (profileIndex, propIndex) => {
    const updated = [...profiles];
    updated[profileIndex].properties.splice(propIndex, 1);
    setProfiles(updated);
  };

  const updateProperty = (profileIndex, propIndex, field, value) => {
    const updated = [...profiles];
    updated[profileIndex].properties[propIndex][field] = value;
    setProfiles(updated);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    setResult(null);

    // Преобразуем строки в списки
    const targetDocsList = targetDocs
        .split(';')
        .map((s) => s.trim())
        .filter(Boolean);
    const orgNamesList = orgNames
        .split(';')
        .map((s) => s.trim())
        .filter(Boolean);
    const orgAddressesList = orgAddresses
        .split(';')
        .map((s) => s.trim())
        .filter(Boolean);

    const daysList = profiles.map((p) => p.days);
    const purposeNamesList = profiles.map((p) =>
        p.purposes
            .split(',')
            .map((s) => s.trim())
            .filter(Boolean)
    );
    const citizenshipNamesList = profiles.map((p) =>
        p.citizenships
            .split(',')
            .map((s) => s.trim())
            .filter(Boolean)
    );
    const propertyNames = profiles.map((p) =>
        p.properties.map((prop) => prop.name)
    );
    const propertyValues = profiles.map((p) =>
        p.properties.map((prop) => prop.value)
    );

    const requestData = {
      ruleName,
      targetDocs: targetDocsList,
      guidanceDescription,
      refusal,
      orgNames: orgNamesList,
      orgAddresses: orgAddressesList,
      daysList,
      purposeNamesList,
      citizenshipNamesList,
      propertyNames,
      propertyValues,
    };

    try {
      const response = await fetch('api/rules', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(requestData),
      });
      if (!response.ok) {
        const errText = await response.text();
        throw new Error(`${response.status}: ${errText}`);
      }
      const data = await response.json();
      setResult(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
      <div style={{ padding: '2rem', fontFamily: 'Arial', maxWidth: '800px', margin: '0 auto' }}>
        <h1>Создание правила</h1>
        <form onSubmit={handleSubmit}>
          <div>
            <label>Название правила:</label>
            <input
                type="text"
                value={ruleName}
                onChange={(e) => setRuleName(e.target.value)}
                required
                style={{ display: 'block', width: '100%', marginBottom: '1rem' }}
            />
          </div>

          <div>
            <label>Целевые документы (через ;):</label>
            <input
                type="text"
                value={targetDocs}
                onChange={(e) => setTargetDocs(e.target.value)}
                style={{ display: 'block', width: '100%', marginBottom: '1rem' }}
            />
          </div>

          <div>
            <label>Описание руководства:</label>
            <input
                type="text"
                value={guidanceDescription}
                onChange={(e) => setGuidanceDescription(e.target.value)}
                style={{ display: 'block', width: '100%', marginBottom: '1rem' }}
            />
          </div>

          <div>
            <label>Отказ (опционально):</label>
            <input
                type="text"
                value={refusal}
                onChange={(e) => setRefusal(e.target.value)}
                style={{ display: 'block', width: '100%', marginBottom: '1rem' }}
            />
          </div>

          <div>
            <label>Названия организаций (через ;):</label>
            <input
                type="text"
                value={orgNames}
                onChange={(e) => setOrgNames(e.target.value)}
                style={{ display: 'block', width: '100%', marginBottom: '1rem' }}
            />
          </div>

          <div>
            <label>Адреса организаций (через ;):</label>
            <input
                type="text"
                value={orgAddresses}
                onChange={(e) => setOrgAddresses(e.target.value)}
                style={{ display: 'block', width: '100%', marginBottom: '1rem' }}
            />
          </div>

          <h2>Профили</h2>
          {profiles.map((profile, idx) => (
              <div key={idx} style={{ border: '1px solid #ccc', padding: '1rem', marginBottom: '1rem' }}>
                <h3>Профиль #{idx + 1}</h3>
                <button type="button" onClick={() => removeProfile(idx)}>Удалить профиль</button>
                <div>
                  <label>Количество дней:</label>
                  <input
                      type="number"
                      value={profile.days}
                      onChange={(e) => updateProfile(idx, 'days', parseInt(e.target.value))}
                      min="0"
                      required
                      style={{ display: 'block', width: '100%', marginBottom: '0.5rem' }}
                  />
                </div>
                <div>
                  <label>Цели въезда (через запятую):</label>
                  <input
                      type="text"
                      value={profile.purposes}
                      onChange={(e) => updateProfile(idx, 'purposes', e.target.value)}
                      style={{ display: 'block', width: '100%', marginBottom: '0.5rem' }}
                  />
                </div>
                <div>
                  <label>Гражданства (через запятую):</label>
                  <input
                      type="text"
                      value={profile.citizenships}
                      onChange={(e) => updateProfile(idx, 'citizenships', e.target.value)}
                      style={{ display: 'block', width: '100%', marginBottom: '0.5rem' }}
                  />
                </div>
                <div>
                  <h4>Дополнительные свойства</h4>
                  {profile.properties.map((prop, propIdx) => (
                      <div key={propIdx} style={{ display: 'flex', gap: '0.5rem', marginBottom: '0.5rem' }}>
                        <input
                            placeholder="Название"
                            value={prop.name}
                            onChange={(e) => updateProperty(idx, propIdx, 'name', e.target.value)}
                        />
                        <input
                            placeholder="Значение"
                            value={prop.value}
                            onChange={(e) => updateProperty(idx, propIdx, 'value', e.target.value)}
                        />
                        <button type="button" onClick={() => removeProperty(idx, propIdx)}>×</button>
                      </div>
                  ))}
                  <button type="button" onClick={() => addProperty(idx)}>+ Добавить свойство</button>
                </div>
              </div>
          ))}
          <button type="button" onClick={addProfile}>+ Добавить профиль</button>

          <div style={{ marginTop: '2rem' }}>
            <button type="submit" disabled={loading}>
              {loading ? 'Отправка...' : 'Создать правило'}
            </button>
          </div>
        </form>

        {error && (
            <div style={{ color: 'red', marginTop: '1rem' }}>Ошибка: {error}</div>
        )}
        {result && (
            <div style={{ marginTop: '2rem' }}>
              <h2>Созданное правило:</h2>
              <pre>{JSON.stringify(result, null, 2)}</pre>
            </div>
        )}
      </div>
  );
}

export default App;