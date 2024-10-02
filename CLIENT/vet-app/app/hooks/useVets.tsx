import { useQuery } from "@tanstack/react-query";
import { AxiosInstance } from "axios";

interface Params {
  pageParam?: number;
  instance?: AxiosInstance;
}

export function useFetchVets({ pageParam, instance }: Params) {

  if (pageParam == null) {
    pageParam = 1;
  }

  if (instance == undefined) {
    return;
  }

  const { data, isLoading, isError } = useQuery({
    queryKey: ["vetList", pageParam],
    queryFn: async () => {
      const response = await instance.get(
        `all-vets?PageNumber=${pageParam}`,
        {
          responseType: "json",
          headers: { "Content-Type": "application/json" },
        },
      );
      return { data: response.data, headers: response.headers }
    },
  })
  return { data, isLoading, isError };
}
