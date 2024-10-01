"use client"

import { useQuery } from "@tanstack/react-query";
import { AxiosInstance } from "axios"

interface Params {
  instance?: AxiosInstance;
}

// VET SIDE 
export function useFetchDataVet(instance: AxiosInstance) {
  const { data, isLoading, isError } = useQuery({
    queryKey: ["dashVet"],
    queryFn: async () => {
      const { data } = await instance.get("vet-dash");
      return data;
    },
  });

  return { data, isLoading, isError }
}

// OWNER SIDE 
