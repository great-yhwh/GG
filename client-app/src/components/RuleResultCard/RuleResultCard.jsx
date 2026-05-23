import styles from "./RuleResultCard.module.css";

export const RuleResultCard = ({ rule }) => {
    if (!rule) return null;

    const {
        id,
        name,
        targetDocuments = [],
        guidance,
        profiles = [],
    } = rule;

    return (
        <div className={styles.card}>
            <div className={styles.header}>
                <div className={styles.headerIcon}>🤩</div>
                <div>
                    <h3 className={styles.title}>Правило успешно создано</h3>
                    <p className={styles.subtitle}>ID: {id}</p>
                </div>
            </div>

            <div className={styles.content}>
                {/* Название правила */}
                <div className={styles.field}>
                    <div className={styles.fieldLabel}>Название правила</div>
                    <div className={styles.fieldValue}>{name}</div>
                </div>

                {/* Целевые документы */}
                {targetDocuments.length > 0 && (
                    <div className={styles.field}>
                        <div className={styles.fieldLabel}>Целевые документы</div>
                        <div className={styles.fieldValue}>
                            {targetDocuments.map((doc, idx) => (
                                <span key={idx} className={styles.badge}>{doc.name}</span>
                            ))}
                        </div>
                    </div>
                )}

                {/* Профили */}
                {profiles.length > 0 && (
                    <div className={styles.field}>
                        <div className={styles.fieldLabel}>Профили</div>
                        <div className={styles.profilesList}>
                            {profiles.map((profile, idx) => (
                                <div key={idx} className={styles.profileCard}>
                                    <div className={styles.profileHeader}>Профиль #{idx + 1}</div>
                                    <div className={styles.profileRow}>
                                        <span>Дней:</span> <strong>{profile.days}</strong>
                                    </div>
                                    {profile.purposes?.length > 0 && (
                                        <div className={styles.profileRow}>
                                            <span>Цели:</span> {profile.purposes.join(", ")}
                                        </div>
                                    )}
                                    {profile.citizenships?.length > 0 && (
                                        <div className={styles.profileRow}>
                                            <span>Гражданства:</span> {profile.citizenships.join(", ")}
                                        </div>
                                    )}
                                    {profile.properties?.length > 0 && (
                                        <div className={styles.profileRow}>
                                            <span>Свойства:</span>
                                            <ul className={styles.propertyList}>
                                                {profile.properties.map((prop, i) => (
                                                    <li key={i}>{prop.name}: {prop.value}</li>
                                                ))}
                                            </ul>
                                        </div>
                                    )}
                                </div>
                            ))}
                        </div>
                    </div>
                )}

                {/* Guidance */}
                {guidance && (
                    <div className={styles.field}>
                        <div className={styles.fieldLabel}>Руководство</div>
                        <div className={styles.guidanceBlock}>
                            {guidance.description && (
                                <div className={styles.guidanceItem}>
                                    <span>Описание:</span> {guidance.description}
                                </div>
                            )}
                            {guidance.refusal && (
                                <div className={styles.guidanceItem}>
                                    <span>Отказ:</span> {guidance.refusal}
                                </div>
                            )}
                            {guidance.organizations?.length > 0 && (
                                <div className={styles.guidanceItem}>
                                    <span>Организации:</span>
                                    <ul>
                                        {guidance.organizations.map((org, i) => (
                                            <li key={i}>{org.name} — {org.address}</li>
                                        ))}
                                    </ul>
                                </div>
                            )}
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
};