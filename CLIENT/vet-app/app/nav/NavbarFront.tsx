import Link from 'next/link'
import React from 'react'
import { MdPets } from 'react-icons/md'

export default function NavbarFront() {
  return (
    <header
      className="sticky top-0 z-50 flex justify-between
     p-10 items-center text-gray-800  bg-green-600"
    >
      <Link
        href="/"
        className="gap-2 text-2xl flex items-center font-semibold text-white"
      >
        <MdPets size={34} />
        VET MVP      </Link>
      <span className="text-xs text-gray-300">Una nueva plataforma para el cuidado de mascotas</span>
      <div className="flex gap-4">
        <div className="text-gray-200 flex text-bold justify-between w-80 text-ls">
          <Link href="/sobre-nosotros">Sobre nosotros</Link>
          <Link href="app/sessions/sign-up"> Crear cuenta </Link>
          <Link href="app/sessions/login" >Iniciar sesión</Link>
        </div>
      </div>

    </header>

  )
}

