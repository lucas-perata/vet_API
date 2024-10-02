import { useQuery } from "@tanstack/react-query";
import { AxiosInstance } from "axios";

interface Params {
  pageParam?: number;
  instance?: AxiosInstance;
}


export function useFetchVets({ instance, pageParam }: Params) {
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
        `all-vets?PageSize=1&PageNumber=${pageParam}`,
        {
          responseType: "json",
          headers: { "Content-Type": "application/json" },
        },
      );

      if (!response || !response.headers) {
        console.log("Error: Response or headers are undefined.");
        return;
      }

      return { data: response.data, headers: response.headers };
    },
  });
  return { data, isLoading, isError };
}

