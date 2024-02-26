import React from 'react'
import { MdPets } from "react-icons/md";
import { Button } from "@/components/ui/button"
import { cookies } from 'next/headers';
import { logout } from '@/utils/auth';
import LogoutButton from '../sessions/LogoutButton';



export default function Navbar() {
  const cookiesList = cookies().get("token")?.value
  
  return (
    <header className='sticky top-0 z-50 flex justify-between 
    bg-white p-5 items-center text-gray-800 shadow-md'>
        <div className='gap-2 text-2xl flex items-center font-semibold text-orange-700'>
            <MdPets size={34}/> 
            <div>Vet MVP</div>
            <div></div>
        </div>
        <div>Pending</div>
        <div className='flex gap-4'>
          {cookiesList == null ? (<><Button href="">Crear cuenta</Button>
          <Button href="">Iniciar sesión</Button></>) : <LogoutButton></LogoutButton> }
        </div>
    </header>
  )
}
