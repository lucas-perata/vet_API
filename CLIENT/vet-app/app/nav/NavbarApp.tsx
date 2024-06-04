"use client"
import React from 'react'
import { Button } from '@/components/ui/button'
import Link from 'next/link'
import { MdPets } from 'react-icons/md'
import LogoutButton from '../[app]/sessions/LogoutButton';
import { getRole } from '../actions/getRole'

type Props = {
  kookies: string;
};



export default function NavbarApp({ kookies }: Props) {
  let role = getRole(kookies);
  return (
    <header
      className="sticky top-0 z-50 flex justify-between
     p-7 items-center text-gray-800 shadow-lg bg-gray-100"
    >
      <Link
        href={kookies == undefined ? "/" : "/app/dashboard"}
        className="gap-2 text-2xl flex items-center font-semibold"
      >
        <MdPets size={34} />
        <div>Vet MVP</div>
      </Link>
      {
        !role ? "loading"
          :
          kookies == undefined ? <div className="flex gap-4">
            <div className="flex gap-5">
              <Button>
                <Link href="/app/sessions/login">Ingresar</Link>
              </Button>
              <Button>
                <Link href="/app/sessions/sign-up">Crear cuenta</Link>
              </Button>
            </div>
          </div>
            :
            <div className="flex gap-4">
              {role == "Vet" ? (<>
                <Link href="/app/adoptions">
                  <Button disabled>
                    Adopciones
                  </Button> </Link>
                <Link href="/app/reviews/"> <Button disabled >Reseñas</Button></Link>
                <Link href="/app/appointments"> <Button disabled>Citas</Button></Link>
                <Link href="/app/profile"> <Button disabled>Perfil</Button></Link>
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

      }
    </header>

  )

}

