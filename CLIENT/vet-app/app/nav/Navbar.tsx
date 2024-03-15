import React from "react";
import { MdPets } from "react-icons/md";
import { Button } from "@/components/ui/button";
import { cookies } from "next/headers";
import LogoutButton from "../sessions/LogoutButton";
import Link from "next/link";

export default function Navbar() {
  const kookies = cookies().get("token")?.value;

  return (
    <header
      className="sticky top-0 z-50 flex justify-between
    bg-white p-5 items-center text-gray-800 shadow-md"
    >
      <Link
        href="/"
        className="gap-2 text-2xl flex items-center font-semibold text-orange-700"
      >
        <MdPets size={34} />
        <div>Vet MVP</div>
        <div></div>
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
        ) : (
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
