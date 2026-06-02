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

    const targetDocumentNames = targetDocuments
        .map((doc) => (typeof doc === "string" ? doc : doc?.name))
        .filter(Boolean);

    return (
        <div className={styles.card}>
            <div className={styles.header}>
                <div className={styles.headerIcon}>✓</div>

                <div className={styles.headerContent}>
                    <span className={styles.status}>Успешно</span>
                    <h3 className={styles.title}>Правило успешно создано</h3>
                    <p className={styles.subtitle}>
                        ID правила: <span className={styles.idBadge}>{id}</span>
                    </p>
                </div>
            </div>

            <div className={styles.section}>
                <div className={styles.sectionLabel}>Название правила</div>
                <div className={styles.sectionValue}>{name || "—"}</div>
            </div>

            {targetDocumentNames.length > 0 && (
                <div className={styles.section}>
                    <div className={styles.sectionLabel}>Целевые документы</div>
                    <div className={styles.chips}>
                        {targetDocumentNames.map((doc, idx) => (
                            <span key={idx} className={styles.chip}>
                                {doc}
                            </span>
                        ))}
                    </div>
                </div>
            )}

            {profiles.length > 0 && (
                <div className={styles.section}>
                    <div className={styles.sectionHead}>
                        <div className={styles.sectionLabel}>Профили</div>
                        <span className={styles.countBadge}>{profiles.length}</span>
                    </div>

                    <div className={styles.profileList}>
                        {profiles.map((profile, idx) => {
                            const purposes = profile.purposes || [];
                            const citizenships = profile.citizenships || [];
                            const properties = profile.properties || [];

                            return (
                                <div key={idx} className={styles.profileCard}>
                                    <div className={styles.profileHeader}>
                                        <div className={styles.profileTitle}>
                                            Профиль #{idx + 1}
                                        </div>
                                        <span className={styles.daysBadge}>
                                            {profile.days} дн.
                                        </span>
                                    </div>

                                    <div className={styles.detailList}>
                                        <div className={styles.detailRow}>
                                            <div className={styles.detailLabel}>Дней</div>
                                            <div className={styles.detailValue}>
                                                {profile.days}
                                            </div>
                                        </div>

                                        {purposes.length > 0 && (
                                            <div className={styles.detailRow}>
                                                <div className={styles.detailLabel}>Цели</div>
                                                <div className={styles.chips}>
                                                    {purposes.map((purpose, i) => (
                                                        <span key={i} className={styles.chip}>
                                                            {purpose}
                                                        </span>
                                                    ))}
                                                </div>
                                            </div>
                                        )}

                                        {citizenships.length > 0 && (
                                            <div className={styles.detailRow}>
                                                <div className={styles.detailLabel}>
                                                    Гражданства
                                                </div>
                                                <div className={styles.chips}>
                                                    {citizenships.map((citizenship, i) => (
                                                        <span key={i} className={styles.chip}>
                                                            {citizenship}
                                                        </span>
                                                    ))}
                                                </div>
                                            </div>
                                        )}

                                        {properties.length > 0 && (
                                            <div className={styles.detailBlock}>
                                                <div className={styles.detailLabel}>Свойства</div>
                                                <div className={styles.properties}>
                                                    {properties.map((prop, i) => (
                                                        <div key={i} className={styles.propertyItem}>
                                                            <span className={styles.propertyName}>
                                                                {prop.name}
                                                            </span>
                                                            <span className={styles.propertySeparator}>
                                                                —
                                                            </span>
                                                            <span className={styles.propertyValue}>
                                                                {prop.value}
                                                            </span>
                                                        </div>
                                                    ))}
                                                </div>
                                            </div>
                                        )}
                                    </div>
                                </div>
                            );
                        })}
                    </div>
                </div>
            )}

            {guidance && (
                <div className={styles.section}>
                    <div className={styles.sectionLabel}>Руководство</div>

                    <div className={styles.guidanceGrid}>
                        {guidance.description && (
                            <div className={styles.guidanceCard}>
                                <div className={styles.guidanceLabel}>Описание</div>
                                <div className={styles.guidanceText}>
                                    {guidance.description}
                                </div>
                            </div>
                        )}

                        {guidance.refusal && (
                            <div className={styles.guidanceCard}>
                                <div className={styles.guidanceLabel}>Отказ</div>
                                <div className={styles.guidanceText}>
                                    {guidance.refusal}
                                </div>
                            </div>
                        )}

                        {guidance.organizations?.length > 0 && (
                            <div className={styles.guidanceCard}>
                                <div className={styles.guidanceLabel}>Организации</div>
                                <div className={styles.orgList}>
                                    {guidance.organizations.map((org, i) => (
                                        <div key={i} className={styles.orgItem}>
                                            <div className={styles.orgName}>{org.name}</div>
                                            <div className={styles.orgAddress}>
                                                {org.address}
                                            </div>
                                        </div>
                                    ))}
                                </div>
                            </div>
                        )}
                    </div>
                </div>
            )}
        </div>
    );
};