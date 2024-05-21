"use client"
import { photoMainB } from '@/app/actions/petActionsCS';
import { useFetchAdoption } from '@/app/hooks/useAdoptions'
import { Button } from '@/components/ui/button';
import useStore from '@/store/store';
import { Adoption } from '@/types';
import { createInstance } from '@/utils/axiosConfig';
import React from 'react'
import { TiMessages } from "react-icons/ti";
import { TiLocationOutline } from "react-icons/ti";

function AdoptionDetails({ params }: { params: { id: number } }) {

  const token = useStore((state) => state.token);
  const axiosI = createInstance(token());
  const { data, isLoading, isError } = useFetchAdoption({ instance: axiosI, id: params.id });

  const adoption: Adoption = data;

  console.log(adoption);

  if (isLoading) return <div>Loading</div>
  // TODO: envío de mensaje autom cuando se presione quiero que me contacten
  // TODO: adopción publicada por... nombre de usuario y link a su perfil
  return (
    <div className='flex  p-10 justify-center'>
      <div className='shadow-2xl rounded-2xl p-2  min-w-96 max-w-max'>
        <img className="rounded-2xl " src={photoMainB(adoption)} alt={`foto adopción ${adoption.name}`} />
        <div className='flex justify-evenly gap-1 p-1'>
          {/* <div className='bg-blue-600 p-1 rounded-2xl text-white'> */}
          {/*   {adoption.area} */}
          {/* </div> */}
          {/* <div className='bg-blue-500 p-1 rounded-2xl text-white'> */}
          {/*   <p>{adoption.province} </p> */}
          {/* </div> */}
          <div className='bg-sky-200 p-1 rounded-xl'>
            <p>{adoption.gender}</p>
          </div>
          <div className='bg-green-500 p-1 rounded-xl text-white'>
            <p>{adoption.isVaccinated == true ? "Vacunado" : ""}</p>
          </div>
          <div className='p-1 bg-green-400 rounded-xl'>
            {adoption.isDeworm == true ? "Desparasitado" : ""}
          </div>
          <div className='p-1 bg-green-300 rounded-xl'>
            {adoption.isNeutered == true ? "Castrado/a" : ""}
          </div>
        </div>
      </div>
      <div className="p-5 max-w-xl gap-5 flex flex-col">
        <div className="flex">
          <TiLocationOutline size={30} />
          <p className='text-2xl font-bold'>{adoption.area}, {adoption.province}</p>
        </div>

        <div>
          <h2 className='text-2xl font-bold'>Detalles</h2>
          <p>{adoption.description}</p>                           </div>
        <div className='border-2 ounded-xl shadow-xl'>
          <p className='flex justify-between p-4 '>{adoption.appUserId} <Button><TiMessages size={20} /></Button> </p>

        </div>
      </div>

    </div>
  )
}

export default AdoptionDetails
