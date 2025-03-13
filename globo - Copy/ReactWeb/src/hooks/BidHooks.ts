import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query"
import axios, { AxiosError, AxiosResponse } from "axios"
import config from "../../config"
import Problem from "../types/problems";
import { Bid } from "../types/bid";

const useFetchBids = (houseId: number) => {
    return useQuery<Bid[],AxiosError<Problem>>({
        queryKey: ["bids", houseId],
        queryFn: () => axios .get(`${config.baseApiUrl}/house/${houseId}/bids`)
                            .then((resp) => resp.data),
    });
}


const useAddBids = () => {
    const queryClient = useQueryClient();
    return useMutation<AxiosResponse,AxiosError<Problem>, Bid>({
        mutationFn: (b) => axios.post(`${config.baseApiUrl}/house/${b.houseId}/bids`,b),
        onSuccess: (resp,bid) => {
            console.log(resp);
            queryClient.invalidateQueries({
                queryKey: ["bids", bid.houseId]
            });
        },
    });
    
}

export { useFetchBids, useAddBids };