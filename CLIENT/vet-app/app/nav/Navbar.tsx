import React from 'react'
import { MdPets } from "react-icons/md";


export default function Navbar() {
  return (
    <header className='sticky top-0 z-50 flex justify-between 
    bg-white p-5 items-center text-gray-800 shadow-md'>
        <div className='gap-2 text-2xl flex items-center font-semibold text-orange-700'>
            <MdPets size={34}/> 
            <div>Vet MVP</div>
        </div>
        <div>Pending</div>
        <div>Login</div>
    </header>
  )
}
