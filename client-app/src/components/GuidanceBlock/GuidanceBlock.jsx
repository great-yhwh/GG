import styles from "./GuidanceBlock.module.css";

export const GuidanceBlock = ({ register }) => {
    return (
        <div className={styles.guidanceBlock}>
            <h2 className={styles.sectionTitle}>
                <span className={styles.sectionIcon}>📖</span>
                Руководство
            </h2>

            <div className={styles.fieldGroup}>
                <label className={styles.label}>
                    Описание руководства
                    <span className={styles.labelOptional}>(опционально)</span>
                </label>
                <input
                    className={styles.input}
                    placeholder="Опишите руководство для данного правила..."
                    {...register("guidanceDescription")}
                />
            </div>

            <div className={styles.fieldGroup}>
                <label className={styles.label}>
                    Отказ
                    <span className={styles.labelOptional}>(опционально)</span>
                </label>
                <input
                    className={styles.input}
                    placeholder="Текст отказа при несоблюдении правила..."
                    {...register("refusal")}
                />
            </div>
        </div>
    );
};