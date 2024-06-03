import React from 'react'
import { Button } from '@/components/ui/button'
import Link from 'next/link'
import { MdPets } from 'react-icons/md'
import LogoutButton from '../[app]/sessions/LogoutButton';

type Props = {
  kookies: string;
  role: string;
};

export default function NavbarApp({ kookies, role }: Props) {
  return (
    <header
      className="sticky top-0 z-50 flex justify-between
     p-7 items-center text-gray-800 shadow-lg bg-gray-100"
    >
      <Link
        href={"/dashboard"}
        className="gap-2 text-2xl flex items-center font-semibold"
      >
        <MdPets size={34} />
        <div>Vet MVP</div>
      </Link>
      <div className="flex gap-4">
        {role == "Vet" ? (<>
          <Link href="/adoptions">
            <Button disabled>
              Adopciones
            </Button> </Link>
          <Link href="/"> <Button disabled >Reseñas</Button></Link>
          <Link href="/"> <Button disabled>Citas</Button></Link>
          <Link href="/"> <Button disabled>Perfil</Button></Link>
        </>) : (
          <div className="flex gap-5">
            <Button>
              <Link href="/adoptions">Adopciones</Link>
            </Button>
            <Button>
              <Link href="/expenses">Gastos</Link>
            </Button>
            <Button>
              <Link href="/pets/list">Mis mascotas</Link>
            </Button>
            <LogoutButton></LogoutButton>
          </div>
        )}
      </div>

    </header>

  )

}

