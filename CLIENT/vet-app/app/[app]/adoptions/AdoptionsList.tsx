"use client";
import React, { useState } from "react";
import useStore from "@/store/store";
import { createInstance } from "@/utils/axiosConfig";
import AdoptionsCard from "./AdoptionsCard";
import { Pagination } from "@/types";
import { Button } from "@/components/ui/button";
import { useFetchAdoptions } from "@/app/hooks/useAdoptions";

function PaginationButtons(params, totalPages: number, currentPage: number) {
  if (totalPages > 1) {
    return (
      <div className="flex gap-4">
        <Button
          disabled={currentPage == 1}
          onClick={() =>
            params((page: number) => (page > 1 ? page - 1 : page))
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
  if (pagination.totalItems < 1) {
    return <div>No hay mascotas para adoptar</div>
  } else {
    return (
      <div className="flex flex-col">
        <div className="flex flex-col w-full items-center align-top">
          <div className="flex flex-row w-10/12 justify-center p-5 gap-5 flex-wrap">
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
}
