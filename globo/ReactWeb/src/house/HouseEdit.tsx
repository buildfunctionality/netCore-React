import { useParams } from "react-router-dom";
import { useFetchHouse, useUpdateHouse } from "../hooks/HouseHooks";
import ApiStatus from "../../utils/apiStatus";
import HouseForm from "./HouseForm";
import ValidationSummary from "../../utils/ValidationSummary";


const HouseEdit = () => {
    const { id } = useParams();
    if(!id) throw Error("Need a house");

    const houseId = parseInt(id);

    const { data,status,isSuccess } = useFetchHouse(houseId);
    const updateHouseMutation = useUpdateHouse();

    if(!isSuccess) return <ApiStatus status={status} />

    return (
        <>
        {   updateHouseMutation.isError && (<ValidationSummary error={updateHouseMutation.error} /> )}
        <HouseForm 
            house={data}
            submitted= {h => updateHouseMutation.mutate(h)}
        />
        </>
    )
};

export default HouseEdit;