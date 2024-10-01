"use client"
import { useMutation, useQuery } from "@tanstack/react-query";
import { AxiosInstance } from "axios";

interface Params {
  id?: number;
  pageParam?: number;
  instance?: AxiosInstance;
}

// VET SIDE 
export function useFetchReviews({ pageParam, instance }: Params) {
  if (pageParam == null) {
    pageParam = 1;
  }

  if (instance == undefined) {
    return;
  }

  const { data, isLoading, isError } = useQuery({
    queryKey: ["reviews", pageParam],
    queryFn: async () => {
      const response = await instance.get(
        `api/review/vet?PageNumber=${pageParam}`,
        {
          responseType: "json",
          headers: { "Content-Type": "application/json" },
        },
      );
      return { data: response, headers: response.headers };
    },
  })
  return { data, isLoading, isError };
}


// OWNER SIDE
