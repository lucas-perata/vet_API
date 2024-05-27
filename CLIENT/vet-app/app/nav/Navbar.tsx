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

  return (
    <header
      className="sticky top-0 z-50 flex justify-between
    bg-white p-5 items-center text-gray-800 shadow-md"
    >
      <Link
        href={role != null ? "/dashboard" : "/"}
        className="gap-2 text-2xl flex items-center font-semibold text-orange-700"
      >
        <MdPets size={34} />
        <div>Vet MVP</div>
      </Link>
      <div className="flex gap-4">
        {kookies == null ? (
          <>
            <Button>
              <Link href="/sessions/sign-up"> Crear cuenta </Link>
            </Button>
            <Button>
              <Link href="/sessions/login">Iniciar sesión</Link>
            </Button>
          </>
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
