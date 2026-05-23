import { useFieldArray } from "react-hook-form";
import styles from "./PropertyList.module.css";

export const PropertyList = ({ control, nestIndex, register }) => {
    const { fields, append, remove } = useFieldArray({
        control,
        name: `profiles.${nestIndex}.properties`,
    });

    return (
        <div className={styles.propertyList}>
            <div className={styles.propertyLabel}>
                Дополнительная информация
                {fields.length > 0 && (
                    <span className={styles.propertyCount}>
                        {fields.length}
                    </span>
                )}
            </div>

            {fields.length === 0 && (
                <div className={styles.emptyProps}>
                    Нет свойств. Нажмите кнопку ниже, чтобы добавить.
                </div>
            )}

            {fields.map((field, idx) => (
                <div
                    key={field.id}
                    className={styles.propertyRow}
                    style={{ animationDelay: `${idx * 0.05}s` }}
                >
                    <input
                        className={styles.propertyInput}
                        {...register(
                            `profiles.${nestIndex}.properties.${idx}.name`
                        )}
                        placeholder="Название свойства"
                    />
                    <input
                        className={styles.propertyInput}
                        {...register(
                            `profiles.${nestIndex}.properties.${idx}.value`
                        )}
                        placeholder="Значение"
                    />
                    <button
                        type="button"
                        className={styles.btnRemoveProp}
                        onClick={() => remove(idx)}
                    >
                        ✕
                    </button>
                </div>
            ))}

            <button
                type="button"
                className={styles.btnAddProp}
                onClick={() => append({ name: "", value: "" })}
            >
                ➕ Добавить свойство
            </button>
        </div>
    );
};