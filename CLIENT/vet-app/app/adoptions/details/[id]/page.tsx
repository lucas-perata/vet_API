"use client"
import { photoMainB } from '@/app/actions/petActionsCS';
import { useFetchAdoption } from '@/app/hooks/useAdoptions'
import useStore from '@/store/store';
import { Adoption } from '@/types';
import { createInstance } from '@/utils/axiosConfig';
import React from 'react'

function AdoptionDetails({ params }: { params: { id: number } }) {

  const token = useStore((state) => state.token);
  const axiosI = createInstance(token());
  const { data, isLoading, isError } = useFetchAdoption({ instance: axiosI, id: params.id });

  const adoption: Adoption = data;

  console.log(adoption);

  if (isLoading) return <div>Loading</div>
  // TODO: envío de mensaje autom cuando se presione quiero que me contacten
  return (
    <div>
      {adoption.id}
      {adoption.area}
      {}
      <div>
        <img src={photoMainB(adoption)} alt={`foto adopción ${adoption.name}`} />

      </div>
      <p>Quiero que me contacten</p>
    </div>
  )
}

export default AdoptionDetails
