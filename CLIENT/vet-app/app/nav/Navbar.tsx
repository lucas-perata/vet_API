"use client"
import React from "react";
import { MdPets } from "react-icons/md";
import { Button } from "@/components/ui/button";
import LogoutButton from "../sessions/LogoutButton";
import Link from "next/link";
import { getRole } from "../actions/getRole";
import useStore from "@/store/store";

export default function Navbar() {

  const token = useStore((state) => state.token);
  const kookies = token();
  let role: string;

  if (kookies) {
    role = getRole(kookies);
  }

  // TODO: diferent navbar for front end and APP side

  return (
    <header
      className="sticky top-0 z-50 flex justify-between
     p-10 items-center text-gray-800  bg-green-600"
    >
      <Link
        href={role != null ? "/dashboard" : "/"}
        className="gap-2 text-2xl flex items-center font-semibold text-white"
      >
        <MdPets size={34} />
        <div>Vet MVP</div>
        <p className="text-xs text-gray-300">Una nueva plataforma para el cuidado de mascotas</p>
      </Link>
      <div className="flex gap-4">
        {kookies == null ? (
          <div className="text-gray-200 flex text-bold justify-between w-80 text-ls">
            <Link href="/sobre-nosotros">Sobre nosotros</Link>
            <Link href="/sessions/sign-up"> Crear cuenta </Link>
            <Link href="/sessions/login" >Iniciar sesión</Link>
          </div>
        ) : role == "Vet" ? (<>
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
  );
}
