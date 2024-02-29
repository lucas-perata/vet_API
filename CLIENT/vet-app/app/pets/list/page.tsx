import React from 'react';
import {createInstance} from '../../../utils/axiosConfig';
import { cookies } from 'next/headers';
import withAuthSSR from '@/utils/withAuthSSR';
import PetCard from './PetCard';

async function getData(){
  const kookies = cookies().get("token")?.value;

  withAuthSSR(kookies);

  const instance = createInstance(kookies); 

  const res = await instance.get("api/pet/pets");

  return res.data;
}

async function page() {

const data = await getData();
 
  return (
      <div className='flex justify-center gap-10' style={{minHeight:"83vh"}}>
          {data && data.map((pet) => (
          <PetCard photo={pet.petPhoto.map((ph) => (ph))} 
          pet={pet} key={pet.id}></PetCard>
          ))}
      </div>
  )
}

export default page;