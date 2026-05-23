import { PropertyList } from "../PropertyList/PropertyList"; // если PropertyList тоже .jsx

export const ProfileItem = ({ index, register, control, errors, onRemove }) => {
    return (
        <div className="profileCard">
            <h3>Профиль #{index + 1}</h3>
            <button type="button" onClick={onRemove}>Удалить профиль</button>

            <div>
                <label>Количество дней:</label>
                <input
                    type="number"
                    {...register(`profiles.${index}.days`, { valueAsNumber: true })}
                />
                {errors.profiles?.[index]?.days && <span className="error">{errors.profiles[index].days.message}</span>}
            </div>

            <div>
                <label>Цели въезда (через запятую):</label>
                <input {...register(`profiles.${index}.purposes`)} />
                {errors.profiles?.[index]?.purposes && <span className="error">{errors.profiles[index].purposes.message}</span>}
            </div>

            <div>
                <label>Гражданства (через запятую):</label>
                <input {...register(`profiles.${index}.citizenships`)} />
                {errors.profiles?.[index]?.citizenships && <span className="error">{errors.profiles[index].citizenships.message}</span>}
            </div>

            <PropertyList control={control} nestIndex={index} register={register} errors={errors} />
        </div>
    );
};