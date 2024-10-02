"use client"
import { useFetchVets } from '@/app/hooks/useVets';
import useStore from '@/store/store'
import { createInstance } from '@/utils/axiosConfig';
import { AxiosInstance } from 'axios';
import React from 'react'
import VetCard from './VetCard';
import { Vet } from '@/types';

function page() {

  const cookie: string | undefined = useStore((state) => state.token());
  const axiosI: AxiosInstance = createInstance(cookie);
  const { data, isLoading, isError } = useFetchVets({ instance: axiosI });

  if (isLoading) return <p>Loading...</p>
  if (isError) return <p>Error...</p>

  return (
    <div className="flex  justify-center">
      <p>Veterinarios cerca de { }</p>
      <div className="flex w-[80%] p-10 justify-center gap-10">
        {data.data.map((vet: Vet) => <VetCard displayName={vet.displayName} area={vet.area} province={vet.province} id={vet.id}
          email={vet.email} phoneNumber={vet.phoneNumber} />)}
      </div>
    </div>
  )
}

export default page
