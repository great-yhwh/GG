import { useFieldArray } from "react-hook-form";
import { BlockEditor } from "../BlockEditor/BlockEditor.jsx";
import styles from "./PageEditor.module.css";

export const PageEditor = ({ pageIndex, control, register }) => {
    const { fields, append, remove } = useFieldArray({
        control,
        name: `pages.${pageIndex}.blocks`,
    });

    return (
        <div className={styles.pageEditor}>
            <div className={styles.pageHeader}>
                <input
                    className={styles.pageNameInput}
                    {...register(`pages.${pageIndex}.name`)}
                    placeholder="Название профиля"
                />
            </div>
            <div className={styles.blocksContainer}>
                {fields.map((block, blockIdx) => (
                    <BlockEditor
                        key={block.id}
                        pageIndex={pageIndex}
                        blockIndex={blockIdx}
                        control={control}
                        register={register}
                        onRemove={() => remove(blockIdx)}
                    />
                ))}
                <div className={styles.addBlockMenu}>
                    <button
                        type="button"
                        onClick={() => append({ type: "shorttext", text: "", required: false, description: "", imageUrl: "", options: [] })}
                    >
                        + Короткий текст
                    </button>
                    <button
                        type="button"
                        onClick={() => append({ type: "longtext", text: "", required: false, description: "", imageUrl: "", options: [] })}
                    >
                        + Длинный текст
                    </button>
                    <button
                        type="button"
                        onClick={() => append({ type: "single", text: "", required: false, description: "", imageUrl: "", options: [{ label: "", points: 0 }] })}
                    >
                        + Один вариант
                    </button>
                    <button
                        type="button"
                        onClick={() => append({ type: "multiple", text: "", required: false, description: "", imageUrl: "", options: [{ label: "", points: 0 }] })}
                    >
                        + Несколько вариантов
                    </button>
                </div>
            </div>
        </div>
    );
};