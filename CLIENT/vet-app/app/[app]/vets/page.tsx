"use client"
import { useFetchVets } from '@/app/hooks/useVets';
import useStore from '@/store/store'
import { createInstance } from '@/utils/axiosConfig';
import { AxiosInstance } from 'axios';
import React, { useState } from 'react'
import VetCard from './VetCard';
import { Pagination, Vet } from '@/types';
import { useFetchUserData } from '@/app/hooks/useUsers';
import { Button } from '@/components/ui/button';

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
function page() {

  const cookie: string | undefined = useStore((state) => state.token());
  const axiosI: AxiosInstance = createInstance(cookie);

  const [pageNumber, setPageNumber] = useState<number>(1);
  const { data, isLoading, isError } = useFetchVets({ instance: axiosI, pageParam: 1 });
  const userData = useFetchUserData({ instance: axiosI });

  if (isLoading) return <p>Loading...</p>
  if (isError) return <p>Error...</p>

  const pagination: Pagination = JSON.parse(data?.headers.pagination);
  if (!isLoading && !isError) {
    console.log(pagination);
  }


  return (
    <div className="flex flex-col items-center justify-center">
      <div>
        <p>Veterinarios cerca de <span>{userData?.data?.data.email}</span></p>
      </div>
      <div className="flex w-[80%] p-10 justify-center gap-10">
        {data.data.map((vet: Vet) => <VetCard displayName={vet.displayName} area={vet.area} province={vet.province} id={vet.id}
          email={vet.email} phoneNumber={vet.phoneNumber} />)}
        <div>
          {PaginationButtons(setPageNumber, pagination.totalPages, pagination.currentPage)}
        </div>
      </div>
    </div>
  )
}

export default page
