"use client"
import { createInstance } from '@/utils/axiosConfig';
import React, { useState } from 'react'
import { AxiosInstance } from 'axios';
import { useFetchReviews } from '@/app/hooks/useReviews';
import ReviewsList from './ReviewsList';
import useStore from '@/store/store';
import { Pagination, Review } from '@/types';
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

  const { data, isLoading, isError } = useFetchReviews({ instance: axiosI, pageParam: pageNumber });

  if (isLoading) return <p>Loading...</p>;
  if (isError) return <p>Error...</p>;

  let lengthNegative = data.data.data.filter(function (item: Review) {
    return item.stars == "AwfulService" || item.stars == "BadService";
  }).length;

  let lengthPositive = data.data.data.filter(function (item: Review) {
    return item.stars == "ExcellentService" || item.stars == "GoodService";
  }).length;


  const pagination: Pagination = JSON.parse(data?.headers.pagination);

  return (
    <div className="flex justify-center flex-col xl:flex-row gap-4">
      <div className="border shadow-xl rounded-xl p-10">
        <div><span className="text-3xl">Reseñas {pagination.totalItems} </span>
        </div>
        <span>
          <ul>
            <li><span className="font-bold">Positivas</span> {lengthPositive}</li>
            <li><span className="font-bold">Negativas</span> {lengthNegative}</li>
            <li><span className="font-bold">Mes actual: </span></li>
            <li><span className="font-bold">Mes pasado: </span></li>
          </ul>
        </span>
      </div>
      <ReviewsList data={data} />
      <div>
        {PaginationButtons(setPageNumber, data.headers.pagination.totalPages, data.headers.pagination.currentPage)}
      </div>
    </div>
  )
}

export default page;
