"use client";
import React, { Dispatch, useState } from "react";
import { useFetchAdoptions } from "../hooks/useAdoptions";
import useStore from "@/store/store";
import { createInstance } from "@/utils/axiosConfig";
import AdoptionsCard from "./AdoptionsCard";
import { Pagination } from "@/types";
import { Button } from "@/components/ui/button";

function PaginationButtons(params, totalPages: number, currentPage: number) {
  return (
    <div className="flex gap-4">
      <Button
        disabled={currentPage >= 1}
        onClick={() =>
          params((page: number) => (totalPages == page ? page - 1 : page))
        }
      >
        Anterior
      </Button>
      <p className="text-red-500 text-xl">{currentPage}</p>
      <Button
        disabled={currentPage == totalPages}
        onClick={() =>
          params((page: number) => (page < totalPages ? page + 1 : page))
        }
      >
        Siguiente
      </Button>
    </div>
  );
}

export default function AdoptionsList() {
  const [pageNumber, setPageNumber] = useState<number>(1);
  const token = useStore((state) => state.token);
  const axiosI = createInstance(token());
  const { data, isLoading, isError } = useFetchAdoptions({
    instance: axiosI,
    pageParam: pageNumber,
  });
  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error</div>;

  const pagination: Pagination = JSON.parse(data?.headers.pagination);
  return (
    <div className="flex flex-col">
      <div className="flex flex-col w-full items-center align-top">
        <div className="flex flex-row w-10/12  items-center p-5 gap-5 flex-wrap">
          <AdoptionsCard adoptions={data?.data.data} />
        </div>
        <div >
          <div >
            {PaginationButtons(
              setPageNumber,
              pagination.totalPages,
              pagination.currentPage,
            )}
          </div>
        </div>

      </div>
    </div>

  );
}
