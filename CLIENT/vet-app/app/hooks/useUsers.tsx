import { useQuery } from "@tanstack/react-query";
import { AxiosInstance } from "axios";

interface Params {
  instance?: AxiosInstance;
}

export function useFetchUserData({ instance }: Params) {
  if (instance == undefined) {
    return;
  }

  const { data, isLoading, isError } = useQuery({
    queryKey: ["userData"],
    queryFn: async () => {
      const response = await instance.get(`currentuser`,
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
