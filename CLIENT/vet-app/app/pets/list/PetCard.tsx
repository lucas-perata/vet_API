import React from "react";
import { FiTrash2 } from "react-icons/fi";
import { FaEdit } from "react-icons/fa";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import Link from "next/link";

type Props = {
  pet: any;
  photo?: any;
};

export default function PetCard({ pet, photo }: Props) {
  return (
    <>
      <Card className="w-[700px] h-[395px]  hover:shadow-xl">
        <Link href={`details/${pet.id}`}>
          <CardHeader className="flex flex-row">
            <Avatar className="w-[300px] h-[300px] shadow-lg">
              <AvatarImage
                src={photo.find((main) => main.isMain == true)?.url || ""}
                alt="foto-mascota"
              />
              <AvatarFallback>Subí una foto</AvatarFallback>
            </Avatar>
            <div className="flex flex-col gap-4 p-4">
              <CardTitle>{pet.name}</CardTitle>
              <CardDescription>Raza: {pet.breed}</CardDescription>
              <CardDescription>
                Fecha de nacimiento {pet.dateOfBirth}
              </CardDescription>
              <CardDescription>Color {pet.color}</CardDescription>
              <CardDescription>Género {pet.gender}</CardDescription>
              <CardDescription>
                {pet.isNeutered == true ? "Castrado" : null}
              </CardDescription>
            </div>
          </CardHeader>
        </Link>
        <CardFooter className="flex justify-end gap-2">
          <Button>Historial médico</Button>
          <Button>Vacunas</Button>
          <Link href={`update/${pet.id}`}>
            <Button>
              <FaEdit />
            </Button>
          </Link>
          <Button className="bg-red-600">
            <FiTrash2 />
          </Button>
        </CardFooter>
      </Card>
    </>
  );
}
