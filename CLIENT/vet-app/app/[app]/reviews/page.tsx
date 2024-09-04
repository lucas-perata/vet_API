"use client"
import { createInstance } from '@/utils/axiosConfig';
import { cookies } from 'next/headers'
import React, { useState } from 'react'
import { redirect } from "next/navigation";
import { AxiosInstance } from 'axios';
import { useFetchReviews } from '@/app/hooks/useReviews';
import ReviewsList from './ReviewsList';
import useStore from '@/store/store';
import { Pagination, Review } from '@/types';

function page() {
  const cookie: string | undefined = useStore((state) => state.token());
  // if (cookie == undefined) redirect("/app/sessions/login");

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
    <div className="flex flex-col xl:flex-row gap-4">
      <div>
        <h2>Cantidad total de reseñas</h2>
        <p>{pagination.totalItems}</p>
        <span>
          <ul>
            <li>Positivas {lengthPositive}</li>
            <li>Negativas {lengthNegative}</li>
          </ul>
        </span>
      </div>
      <ReviewsList data={data} />
    </div>
  )
}

export default page;
