"use client";

import { useMutation, useQuery } from "@tanstack/react-query";
import { AxiosInstance } from "axios";
import { useQueryClient } from "@tanstack/react-query";

interface Params {
  id?: number;
  pageParam?: number;
  instance?: AxiosInstance;
  area?: string;
  province?: string;
  gender?: string;
}

export function useFetchAdoptions({ instance, pageParam }: Params) {
  if (pageParam == null) {
    pageParam = 1;
  }
  const { data, isLoading, isError } = useQuery({
    queryKey: ["adoptions", pageParam],
    queryFn: async () => {
      const response = await instance.get(
        "api/Adoption/all-adoptions?pagesize=6&PageNumber=" + pageParam,
        {
          responseType: "json",
          headers: { "Content-Type": "application/json" },
        },
      );
      return { data: response, headers: response.headers };
    },
  });
  return { data, isLoading, isError };
}

export function useFetchAdoption({ instance, id }: Params) {
  const { data, isLoading, isError } = useQuery({
    queryKey: ["adoption"],
    queryFn: async () => {
      const { data } = await instance.get("api/adoption/" + id);
      return data;
    },
  });
  return { data, isLoading, isError };
}

export function useFilterAdoptions(instance) {
  const queryClient = useQueryClient();
  const { mutate: filterData } = useMutation({
    mutationFn: async (area: string) => {
      const response = await instance.get(`api/Adoption/search?pagesize=6&area=${area}&gender=string`,
        {
          responseType: "json",
          headers: { "Content-Type": "application/json" },
        },
      );
      return { data: response, headers: response.headers };
    },
    onSuccess: (response) => {
      queryClient.setQueryData(["adoptions", 1], response)
    },
    onError: () => {
      console.log("error");
    }
  })
  return { filterData };
}


