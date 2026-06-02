import { useFieldArray } from "react-hook-form";
import styles from "./BlockEditor.module.css";

export const BlockEditor = ({ pageIndex, blockIndex, control, register, onRemove }) => {
    const { fields: optionFields, append, remove: removeOption } = useFieldArray({
        control,
        name: `pages.${pageIndex}.blocks.${blockIndex}.options`,
    });

    const blockType = control._formValues?.pages?.[pageIndex]?.blocks?.[blockIndex]?.type || "shorttext";
    const isChoiceType = blockType === "single" || blockType === "multiple";

    return (
        <div className={styles.blockCard}>
            <div className={styles.blockHeader}>
                <select {...register(`pages.${pageIndex}.blocks.${blockIndex}.type`)}>
                    <option value="shorttext">Короткий текст</option>
                    <option value="longtext">Длинный текст</option>
                    <option value="single">Один вариант</option>
                    <option value="multiple">Несколько вариантов</option>
                </select>
                <button type="button" onClick={onRemove} className={styles.removeBtn}>Удалить</button>
            </div>
            <div className={styles.field}>
                <label>Вопрос</label>
                <input {...register(`pages.${pageIndex}.blocks.${blockIndex}.text`)} placeholder="Введите вопрос" />
            </div>
            <div className={styles.row}>
                <label className={styles.checkbox}>
                    <input type="checkbox" {...register(`pages.${pageIndex}.blocks.${blockIndex}.required`)} />
                    Обязательный
                </label>
                <span className={styles.optionalLabel}>Добавить пояснение</span>
                <span className={styles.optionalLabel}>Добавить изображение</span>
            </div>
            <div className={styles.field}>
                <label>Пояснение</label>
                <input {...register(`pages.${pageIndex}.blocks.${blockIndex}.description`)} placeholder="Дополнительная подсказка" />
            </div>
            <div className={styles.field}>
                <label>Изображение (URL)</label>
                <input {...register(`pages.${pageIndex}.blocks.${blockIndex}.imageUrl`)} placeholder="https://..." />
            </div>
            {isChoiceType && (
                <div className={styles.optionsSection}>
                    <div className={styles.optionsHeader}>Ответы / Баллы</div>
                    {optionFields.map((opt, optIdx) => (
                        <div key={opt.id} className={styles.optionRow}>
                            <input
                                placeholder="Вариант"
                                {...register(`pages.${pageIndex}.blocks.${blockIndex}.options.${optIdx}.label`)}
                            />
                            <input
                                type="number"
                                placeholder="Баллы"
                                {...register(`pages.${pageIndex}.blocks.${blockIndex}.options.${optIdx}.points`, { valueAsNumber: true })}
                            />
                            <button type="button" onClick={() => removeOption(optIdx)}>✕</button>
                        </div>
                    ))}
                    <button type="button" onClick={() => append({ label: "", points: 0 })}>+ Добавить вариант</button>
                </div>
            )}
        </div>
    );
};